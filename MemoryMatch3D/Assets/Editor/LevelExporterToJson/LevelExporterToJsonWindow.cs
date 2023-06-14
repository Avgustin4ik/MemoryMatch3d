using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;


public class LevelExporterToJsonWindow : EditorWindow
    {
        private static LevelExporterToJsonWindow _levelExporterToJsonWindow;
        [SerializeField] [HideInInspector] private LevelBlueprintData[] _levelsDataArray;
        private static SerializedProperty serArray;
        private static SerializedObject obj;
        private string json;

        [MenuItem("Tools/LevelEditor/Level Exporter")]

        public static void OpenWindow()
        {
            _levelExporterToJsonWindow = GetWindow<LevelExporterToJsonWindow>();
            _levelExporterToJsonWindow.name = $"Level Exporter";
            _levelExporterToJsonWindow.Show();
            obj = new SerializedObject(_levelExporterToJsonWindow);
            serArray = obj.FindProperty("_levelsDataArray");
            if(serArray == null) Debug.LogWarning("Array Property not find");
        }

        private void OnGUI()
        {
            var headerStyle = GUI.skin.label;
            headerStyle.alignment = TextAnchor.MiddleCenter;
            headerStyle.fontStyle = FontStyle.Bold;
            GUILayout.BeginVertical();
            EditorGUILayout.LabelField($"Level Exporter to Json",headerStyle);
            obj.Update();
            var propertyField = EditorGUILayout.PropertyField(serArray, true);
            obj.ApplyModifiedProperties();
            var buttonStyle = GUI.skin.button;
            buttonStyle.alignment = TextAnchor.MiddleCenter;
            buttonStyle.normal.textColor = Color.green;
            buttonStyle.fixedWidth = 100;
            buttonStyle.fixedHeight = 50;
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            var button = GUILayout.Button($"Export to Json", buttonStyle);
            GUILayout.FlexibleSpace();
            buttonStyle.fixedWidth = 25;
            buttonStyle.fixedHeight = 25;
            GUILayout.EndHorizontal();
            if (button)
            {
                Export();
            }
            GUILayout.Space(EditorGUIUtility.singleLineHeight);
            var skinTextField = GUI.skin.textField;
            skinTextField.stretchHeight = true;
            GUILayout.TextArea(json, skinTextField);
            GUILayout.EndVertical();
            buttonStyle = default;
        }

        private void Export()
        {
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            };
            json = Newtonsoft.Json.JsonConvert.SerializeObject(_levelsDataArray, settings);
            File.WriteAllText(Application.streamingAssetsPath + "/LevelsBlueprints.json", json);
            PlayerPrefs.SetString("LevelsBlueprints", json);
            PlayerPrefs.Save();
        }
    }
