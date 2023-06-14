using System;
using System.Collections.Generic;
using System.Linq;
using Animals;
using Complicators;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Sirenix.Utilities;
using UnityEditor;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Configs/New LevelEditor Data...", fileName = "DataStorageForLevelEditor")]
[Serializable]
public class LevelBlueprintData : SerializedScriptableObject
{
    public void SetData(Vector2Int gridSize, int[] cellContent)
    {
        this.gridSize = gridSize;
        ReconstructCellContentMatrix();
        for (var i = 0; i < gridSize.x; i++)
        {
            for (var j = 0; j < gridSize.y; j++)
            {
                CellMatrix[i, j] = cellContent[j * this.gridSize.x + i];
            }
        }
    }
    [SerializeField, GUIColor(255f/255f, 80f/255f, 47f/255f, 1)] public AnimalsType animalsType;
    [OnValueChanged("ReconstructCellContentMatrix")]
    public Vector2Int gridSize;

    #region generate random cell matrix

    [ToggleGroup("RandomizeCellMatrix")] public bool RandomizeCellMatrix = false;
    [ToggleGroup("RandomizeCellMatrix"), Min(0)] public int animalsCount;
    [ToggleGroup("RandomizeCellMatrix"), DisableIf("@true")] public uint propsCount;
    
    [ToggleGroup("RandomizeCellMatrix")]
    [Button("Randomize", ButtonSizes.Large), GUIColor(0,1,0)]
    public void GenerateCellMatrix()
    {
        var s = new Color(255, 80, 47, 1).ToString();
        var size = gridSize.x * gridSize.y;
        var count = animalsCount == 0 ? Random.Range(4, size + 1) : animalsCount;
        if (count % 2 != 0) count += 1;
        var data = new List<int>();
        for (int i = 0; i < size; i++)
        {
            if(i < count) 
                data.Add((int)CellContentEnum.Animal);
            else
                data.Add((int)CellContentEnum.Empty);
        }

        var rnd = new System.Random();
        var randomizedData = data.OrderBy(x => rnd.Next()).ToArray();
        CellMatrix = new int[gridSize.x, gridSize.y];
        var debugCount = randomizedData.Count(element => element == 1);
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                var index = j + i * gridSize.x;
                var element = randomizedData[index];
                CellMatrix[i, j] = element;
            }
        }
    }
    #endregion
    [HorizontalGroup(marginLeft:35, marginRight:35)]
    [TableMatrix(SquareCells = true, DrawElementMethod = "DrawColoredEnumElement", ResizableColumns = true, RespectIndentLevel = true)]
    public int[,] CellMatrix;
    private void ReconstructCellContentMatrix()
    {
        if(CellMatrix != null 
           && CellMatrix.GetLength(0) == gridSize.x 
           && CellMatrix.GetLength(1) == gridSize.y)
            return;
        CellMatrix = new int[gridSize.x, gridSize.y];
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                CellMatrix[i, j] = 0;
            }
        }
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
#endif
    }
#if UNITY_EDITOR
    private static int DrawColoredEnumElement(Rect rect, int value)
    {
        if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
        {
            value = (value + 1) % Enum.GetValues(typeof(CellContentEnum)).Length;
            GUI.changed = true;
            Event.current.Use();
        }
        Texture2D[] _icons = new Texture2D[]
        {
            AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor/LevelEditor/Textures/EmptyIcon.png"),
            AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor/LevelEditor/Textures/AnimalIcon.png"),
            AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor/LevelEditor/Textures/PropsIcon.png")
        };
        var texture2D = _icons[value];
        UnityEditor.EditorGUI.DrawTextureAlpha(rect.Padding(1), texture2D);
        return value;
    }   
#endif
    public enum CellContentEnum
    {
        Empty = 0,
        Animal = 1
        // Props = 2
    }

    [HorizontalGroup("Turns")]
    public bool IsTurnEndless = true;
    [HorizontalGroup("Turns"), DisableIf("IsTurnEndless", false)]
    public uint TurnsCount = 0;
    public bool IsOpenAllBoosterFree = false;
    public Dictionary<int, ComplicatorData[]> ComplicatorsSchema;
}

