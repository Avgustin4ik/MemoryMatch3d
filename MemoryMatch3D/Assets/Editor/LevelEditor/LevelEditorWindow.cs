using System;
using System.Collections.Generic;
using System.Linq;
using Complicators;
using UnityEditor;
using UnityEngine;
public enum CellContent
{
    EMPTY = 0,
    ANIMAL = 1
    // PROPS = 2
}

public class LevelEditorWindow : EditorWindow
    {
        
        private Rect _headerRect;
        private Rect _constructorRect;
        private Rect _footerRect;
        private Rect _complicatorsRect;
        private static LevelEditorWindow _levelEditorWindow;
        private static Vector2Int _gridSize = new Vector2Int(3,3);
        private static int _propsCount;
        private static int _animalsCount;
        private static Dictionary<int, ComplicatorData[]> _complicatorsDict;
        [SerializeField] [HideInInspector]
        private ComplicatorData[] _complicators = Array.Empty<ComplicatorData>();
        private static SerializedProperty serComplicatorsArr;
        
        private const int WindowWidth = 600;
        private const int WindowHeight = 600;
        private const int MaxTurnCount = 20;
        [MenuItem("Tools/LevelEditor/LevelEditor Window")]
        static void OpenWindow()
        {
            OpenDataFile();
            _complicatorsDict = new Dictionary<int, ComplicatorData[]>();
            _selectedTurn = 0;
            _turnsCount = 4;
            _levelEditorWindow = GetWindow<LevelEditorWindow>();
            _levelEditorWindow.minSize = new Vector2(WindowWidth, WindowHeight);
            _levelEditorWindow.Show();
            obj = new SerializedObject(_levelEditorWindow);
            serComplicatorsArr = obj.FindProperty("_complicators");
            if(serComplicatorsArr == null) Debug.LogWarning("Dictionary not find");
        }

        private static int[] _cellsContent;
        private static void OpenDataFile()
        {
            _gridSize = new Vector2Int(3,4);
            _cellsContent = new int[_gridSize.x * _gridSize.y];
            Array.Fill(_cellsContent,0);
        }

        private void OnGUI()
        {
            InitializeTextures();
            InitializeRects();
            DrawTextures();
            DrawGridSizeArea();
            DrawConstructorArea();
            DrawControlButtons();
            DrawComplicatorsArea();
        }

       private static int _selectedTurn = -1;
        private static bool _createNewComplicatorBtn = false;
        public uint RepeatFrequency { get; set; }
        public bool Repeatable {get; set;}
        public ComplicatorData LastCreatedComplicator { get; set; }

        public bool EndlessTurnsToggle => _endlessTurnsToggle;

        private void DrawComplicatorsArea()
        {
            obj.Update();
            GUILayout.BeginArea(_complicatorsRect);
            GUILayout.BeginHorizontal();
            {
                GUILayout.Space(25f);
                GUILayout.BeginVertical();
                {
                    string[] labelArr = new string[EndlessTurnsToggle ? 10 : _turnsCount];
                    var btnStyle = new GUIStyle(GUI.skin.button);
                    btnStyle.fixedHeight = EditorGUIUtility.singleLineHeight;
                    btnStyle.margin = new RectOffset(10, 5, 5, 5);
                    for (int i = 0; i < (EndlessTurnsToggle ? 10 : _turnsCount); i++)
                    {
                        labelArr[i] = $"TURN #{i}";
                    }
                    _selectedTurn = GUILayout.SelectionGrid(_selectedTurn, labelArr, 1, btnStyle);
                    if (_selectedTurn >= 0)
                    {
                        if(_complicatorsDict[_selectedTurn] != null) _complicators = _complicatorsDict[_selectedTurn];   
                        if (LastCreatedComplicator != null)
                        {
                            var complicatorDatas = _complicatorsDict[_selectedTurn];
                            Array.Resize(ref _complicators, complicatorDatas.Length + 1);
                            _complicators[^1] = LastCreatedComplicator;
                            //hande repeatable
                            if (Repeatable && EndlessTurnsToggle == false)
                            {
                                var startIndex = _selectedTurn;
                                for (int i = 0; i < _complicatorsDict.Count; i++)
                                {
                                    var turnNumber = i;
                                    if(turnNumber < startIndex) continue;
                                    if((turnNumber - startIndex)%RepeatFrequency != 0) continue;
                                    var complicators = _complicatorsDict[turnNumber];
                                    Array.Resize(ref complicators, complicators.Length + 1);
                                    complicators[^1] = LastCreatedComplicator;
                                    _complicatorsDict[turnNumber] = complicators;
                                }
                                Repeatable = false;
                                RepeatFrequency = 0;
                            }
                            LastCreatedComplicator = null;
                        }
                    } 
                }
                if (GUILayout.Button("RESET"))
                {
                    _selectedTurn = -1;
                    _complicatorsDict.Clear();
                }
                GUILayout.EndVertical();
                GUILayout.BeginVertical();
                var complicatorsArray = EditorGUILayout.PropertyField(serComplicatorsArr);
                obj.ApplyModifiedProperties();
                _complicatorsDict[_selectedTurn] = _complicators;
                _createNewComplicatorBtn = GUILayout.Button("Create New Complicator");
                if (_createNewComplicatorBtn)
                {
                    GetWindow<CreateNewComplicatorWindow>().Show();
                }
                GUILayout.EndVertical();
            }
            GUILayout.Space(25f);
            GUILayout.EndHorizontal();
            
            GUILayout.EndArea();
        }

        private void DrawControlButtons()
        {
            GUILayout.BeginArea(_footerRect);
            GUILayout.BeginVertical();
            GUILayout.Space(EditorGUIUtility.singleLineHeight);
            var button = GUI.skin.button;
            // button = default;
            button.fixedWidth = _buttonSize.x*2;
            button.fixedHeight = _buttonSize.y;
            button.margin = _buttonOffset;
            button.normal.textColor = Color.white;
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            var gridSize = _gridSize.x * _gridSize.y;
            _setAnimalsToggle = GUILayout.Toggle(_setAnimalsToggle, $"Set Animals Count");
            if (_setAnimalsToggle == false) GUI.enabled = false;
                _animalsCount = Mathf.Clamp(EditorGUILayout.IntField($"count", _animalsCount), 0, gridSize);
            GUI.enabled = true;
            if (_animalsCount % 2 != 0) _animalsCount += 1; //check odd
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            var availablePropsCount = gridSize - _animalsCount;
            GUI.enabled = false; 
            _setPropsToggle = GUILayout.Toggle(_setPropsToggle, $"Set Props Count");
            // if (_setPropsToggle == false) GUI.enabled = false;
            _propsCount = Mathf.Clamp(EditorGUILayout.IntField($"count", _propsCount), 0, availablePropsCount);
            GUI.enabled = true;
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            var randomizeBtn = GUILayout.Button($"Randomize", button);
            GUILayout.BeginHorizontal();
            var saveBtn = GUILayout.Button($"Save Scheme", button);
            GUILayout.Space(EditorGUIUtility.singleLineHeight);
            var exportBtn = GUILayout.Button($"Export to Json", button);
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            GUILayout.EndArea();
            if (saveBtn)
            {
                SaveBlueprint($"Assets/Editor/LevelEditor/LevelBlueprints/Level.asset");
            }
            if (randomizeBtn)
                RandomizeCells(_setAnimalsToggle, _setPropsToggle);
            if(exportBtn)
                LevelExporterToJsonWindow.OpenWindow();
        }
        

        private void RandomizeCells(bool setAnimalsToggle, bool setPropsToggle)
        {
            var ratio = 0.6f; //maximum possible animals //todo varialble in inspector
            
            var size = _gridSize.x * _gridSize.y;
            var animalsMax = Convert.ToInt32(size * ratio);
            if (_animalsCount % 2 != 0) _animalsCount += 1;
            int animalsCount = setAnimalsToggle ? _animalsCount : UnityEngine.Random.Range(0, animalsMax);
            if (animalsCount % 2 != 0) animalsCount += 1;
            // var propsMax = size - animalsMax;
            // var propsCount = setPropsToggle ? _propsCount : UnityEngine.Random.Range(0, propsMax);
            var propsCount = 0;
            var rnd = new System.Random();
            var list = new List<int>();
            for (var i = 0; i < size; i++) {
                if (i < animalsCount) {
                    list.Add((int)CellContent.ANIMAL);
                    continue;
                }
                // if (i < animalsCount + propsCount) {
                //     list.Add((int)CellContent.PROPS);
                //     continue;
                // }
                list.Add((int)CellContent.EMPTY);
            }

            _cellsContent = list.OrderBy(x => rnd.Next()).ToArray();
        }

        private static void SaveBlueprint(string baseFilename)
        {
            var newFilename = AssetDatabase.GenerateUniqueAssetPath(baseFilename);
            var levelBlueprintData = ScriptableObject.CreateInstance<LevelBlueprintData>();
            levelBlueprintData.SetData(_gridSize,_cellsContent);
            AssetDatabase.CreateAsset(levelBlueprintData, newFilename);
            AssetDatabase.SaveAssets();
            var size = _gridSize.x*_gridSize.y;
            var newArray = new int[size];
            Array.Copy(_cellsContent,newArray,size);
            _cellsContent = newArray;
            levelBlueprintData.IsTurnEndless = _endlessTurnsToggle;
            levelBlueprintData.TurnsCount =  (uint)_turnsCount;
            levelBlueprintData.IsOpenAllBoosterFree = _freeOpenAllToggle;
            levelBlueprintData.ComplicatorsSchema = _complicatorsDict;
            // levelBlueprintData.Reward = null;
#if UNITY_EDITOR
            Debug.LogWarning($"LevelBlueprint was saved at {newFilename}");
#endif
        }

        private void DrawConstructorArea()
        {
            GUILayout.BeginArea(_constructorRect);
            var clickedButton = -1;
            DrawLevelScheme(ref clickedButton);
            GUILayout.EndArea();
            if(clickedButton < 0 ) return;
            // Debug.Log($"clickedButton = {clickedButton}");
            var newValue = _cellsContent[clickedButton] + 1;
            var enumContentCount = Enum.GetNames(typeof(CellContent)).Length - 1;
            _cellsContent[clickedButton] = newValue > enumContentCount ? 0 : newValue;
        }

        private void DrawTextures()
        {
            GUI.DrawTexture(_headerRect,_headerTexture);
            GUI.DrawTexture(_constructorRect, _constructorTexture);
            GUI.DrawTexture(_footerRect,_headerTexture);
            GUI.DrawTexture(_complicatorsRect, _constructorTexture);
        }

        private Vector2 _buttonSize; 
        private RectOffset _buttonOffset; 

        private void InitializeRects()
        {
            const int rightWidth = 200;
            var leftWidth = WindowWidth - rightWidth;
            _complicatorsRect.x = leftWidth;
            _complicatorsRect.y = 0;
            _complicatorsRect.width = 400;
            _complicatorsRect.height = WindowHeight;
            
            _buttonSize = new Vector2(50,50);
            _buttonOffset = new RectOffset(10, 10, 10, 10);
            _headerRect.x = _headerRect.y = 0;
            _headerRect.width = leftWidth;
            _headerRect.height = EditorGUIUtility.singleLineHeight*5;
            
            _constructorRect.x = 0;
            _constructorRect.y = _headerRect.height;
            _constructorRect.width = leftWidth;
            _constructorRect.height = _gridSize.y * (_buttonSize.y + _buttonOffset.bottom) - _buttonOffset.bottom + 2*EditorGUIUtility.singleLineHeight;

            _footerRect.x = 0;
            _footerRect.y = _constructorRect.y + _constructorRect.height;
            _footerRect.width = leftWidth;
            _footerRect.height = WindowHeight - _footerRect.y;
        }

        

        private Texture2D _headerTexture;
        private Texture2D _constructorTexture;
        private Color _headerColor = Color.gray;
        private Color _constructorColor = Color.black;

        private static bool _setAnimalsToggle;
        private static bool _setPropsToggle;

        private const int MaxXSize = 18;
        private const int MaxYSize = 18;
        // private static LevelBlueprintData _data;

        private void InitializeTextures()
        {
            void Init(out Texture2D texture, Color color)
            {
                texture = new Texture2D(1, 1);
                texture.SetPixel(0,0,color);
                texture.Apply();

            }
            ColorUtility.TryParseHtmlString("#4C4C47", out var color);
            Init(out _headerTexture, color);
            ColorUtility.TryParseHtmlString("#2D2D2A", out color);
            Init(out _constructorTexture, color);
        }

        private void DrawGridSizeArea()
        {
            GUILayout.BeginArea(_headerRect);
            DrawGridSizeProperties(out var newGridSize);
            GUILayout.EndArea();
            if (newGridSize != _gridSize)
            {
                _gridSize = newGridSize;
                _cellsContent = new int[_gridSize.x * _gridSize.y];
            }
        }

        private void DrawLevelScheme(ref int clickedButton)
        {   
            var labelArray = _cellsContent.Select(cell => ((CellContent)cell).ToString()).ToArray();

            var button = GUI.skin.button;
            button.fixedWidth = _buttonSize.x;
            button.fixedHeight = _buttonSize.y;
            button.margin = _buttonOffset;
            
            GUILayout.BeginVertical();
            GUILayout.Space(EditorGUIUtility.singleLineHeight);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            clickedButton = GUILayout.SelectionGrid(-1, labelArray, xCount: _gridSize.x, button);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal(); 
            GUILayout.Space(EditorGUIUtility.singleLineHeight);
            GUILayout.EndVertical();
        }

        private static int _turnsCount = 0;
        private static bool _endlessTurnsToggle;
        private static bool _freeOpenAllToggle;
        private static bool[] turnsArr;
        private static SerializedObject obj;
        

        private void DrawGridSizeProperties(out Vector2Int newGridSize)
        {
            GUILayout.BeginVertical();
            EditorGUILayout.LabelField($"Grid Size");
            GUILayout.BeginHorizontal();
            newGridSize = _gridSize;//todo костыль
            newGridSize.x = Mathf.Clamp(EditorGUILayout.IntField("x", _gridSize.x, GUI.skin.textField),2,MaxXSize);
            newGridSize.y = Mathf.Clamp(EditorGUILayout.IntField("y", _gridSize.y, GUI.skin.textField),2,MaxYSize);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            {
                _endlessTurnsToggle = GUILayout.Toggle(EndlessTurnsToggle, "Endless Turns");
                GUI.enabled = !EndlessTurnsToggle;
                _turnsCount = Mathf.Clamp(value: EditorGUILayout.IntField("Turns", _turnsCount, GUI.skin.textField), min: 0, max: MaxTurnCount);
                GUI.enabled = true;
            }
            GUILayout.EndHorizontal();
            _freeOpenAllToggle = GUILayout.Toggle(_freeOpenAllToggle, "Show All Pokeballs On Start?");
            GUILayout.EndVertical();
            for (int i = 0; i < (EndlessTurnsToggle ? 10 : _turnsCount); i++)
            {
                _complicatorsDict.TryAdd(i, Array.Empty<ComplicatorData>());
            }
        }
    }
