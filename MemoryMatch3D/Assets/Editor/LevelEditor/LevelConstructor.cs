using System;
using System.Collections.Generic;
using System.Linq;
using Complicators;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class LevelConstructor : OdinMenuEditorWindow
{
    [MenuItem("Tools/LevelEditor/Level Constructor")]
    static void Open()
    {
        GetWindow<LevelConstructor>().Show();
    }


    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree(false);
        var loadAllAssetsAtPath = new List<Object>();
        TryGetUnityObjectsOfTypeFromPath("Assets/Editor/LevelEditor/LevelBlueprints", loadAllAssetsAtPath);
        var treeElements = loadAllAssetsAtPath.Where(element => element is LevelBlueprintData);
        foreach (var asset in treeElements)
        {
            tree.Add(asset.name, asset);
        }
        tree.SortMenuItemsByName();
        tree.Add("New Blueprint...", new CreateNewLevelBlueprint() );
        return tree;
    }
    // [Serializable]
    // class LevelSchemaEditor
    // {
    //     public LevelSchemaEditor(LevelBlueprintData levelBlueprintData)
    //     {
    //         LevelBlueprintData = levelBlueprintData;
    //         var isTurnsCountEndless = levelBlueprintData.IsTurnEndless || levelBlueprintData.TurnsCount <= 0;
    //         _turnsCount = isTurnsCountEndless ? 10 : levelBlueprintData.TurnsCount;
    //     }
    //     private uint _turnsCount = 10; 
    //     [Title("Level constructor")]
    //     [InlineEditor(InlineEditorModes.FullEditor)]
    //     public LevelBlueprintData LevelBlueprintData;
    // }
    

    public class CreateNewLevelBlueprint
    {
        public CreateNewLevelBlueprint()
        {
            BlueprintData = ScriptableObject.CreateInstance<LevelBlueprintData>();
            BlueprintData.CellMatrix = new int[2, 2];
            BlueprintData.gridSize = new Vector2Int(2, 2);
            BlueprintData.ComplicatorsSchema = new Dictionary<int, ComplicatorData[]>();
        }
        public string LevelName = string.Empty;
        [Button(ButtonSizes.Large), GUIColor(0,1,0)]
        public void SaveBlueprint()
        {
            var fileName = LevelName == string.Empty ? "New Level" : LevelName; 
            AssetDatabase.GenerateUniqueAssetPath("Assets/Editor/LevelEditor/LevelBlueprints" + "/" + fileName + ".asset");
            AssetDatabase.CreateAsset(BlueprintData, "Assets/Editor/LevelEditor/LevelBlueprints" + "/" + fileName + ".asset");
            AssetDatabase.SaveAssets();
        }
        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        public LevelBlueprintData BlueprintData;
    }

#if UNITY_EDITOR
 
    /// <summary>
    /// Adds newly (if not already in the list) found assets.
    /// Returns how many found (not how many added)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <param name="assetsFound">Adds to this list if it is not already there</param>
    /// <returns></returns>
    public static int TryGetUnityObjectsOfTypeFromPath<T>(string path, List<T> assetsFound) where T : UnityEngine.Object
    {
        string[] filePaths = System.IO.Directory.GetFiles(path);
 
        int countFound = 0;
 
        Debug.Log(filePaths.Length);
 
        if (filePaths != null && filePaths.Length > 0)
        {
            for (int i = 0; i < filePaths.Length; i++)
            {
                UnityEngine.Object obj = UnityEditor.AssetDatabase.LoadAssetAtPath(filePaths[i], typeof(T));
                if (obj is T asset)
                {
                    countFound++;
                    if (!assetsFound.Contains(asset))
                    {
                        assetsFound.Add(asset);
                    }
                }
            }
        }
 
        return countFound;
    }
 
#endif
}
