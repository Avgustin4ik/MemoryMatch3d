using System;
using System.IO;
using Entitas;

using Newtonsoft.Json;

using UnityEngine;

namespace Grid
{
    public class ReadLevelsBlueprintsInitializationSystem : IInitializeSystem
    {
        public ReadLevelsBlueprintsInitializationSystem(LevelContext contextsLevel)
        {
        }

        public void Initialize()
        {
            var levelContext = Contexts.sharedInstance.level;
            var filePath = string.Empty;
            var readAllText = string.Empty;

#if UNITY_EDITOR
            filePath = Application.streamingAssetsPath + "/LevelsBlueprints.json";
            readAllText = File.ReadAllText(filePath);
#elif PLATFORM_ANDROID
            filePath =  Path.Combine(Application.streamingAssetsPath, "LevelsBlueprints.json");
            using (UnityEngine.Networking.UnityWebRequest uwr = UnityEngine.Networking.UnityWebRequest.Get(filePath))
            {
                uwr.SendWebRequest();
                while (!uwr.isDone)
                {
                }
                if (uwr.isNetworkError || uwr.isHttpError)
                {
                    Debug.LogError(uwr.error);
                }
                else
                {
                    readAllText = uwr.downloadHandler.text;
                }
                
            }
#endif 
            Debug.LogWarning($"FilePath = {filePath}");
            if(string.IsNullOrEmpty(readAllText)) throw new Exception("LevelsBlueprints not found");
            GenerateBlueprints(readAllText, levelContext);
        }

        private static void GenerateBlueprints(string readAllText, LevelContext levelContext)
        {
            var settings = new JsonSerializerSettings()
            {   
                TypeNameHandling = TypeNameHandling.All
            };
            var blueprintsCatalog = JsonConvert.DeserializeObject<LevelBlueprintData[]>(readAllText, settings);
            int index = 0;
            foreach (var blueprintData in blueprintsCatalog)
            {
                // if(blueprintData.animalsType != Contexts.sharedInstance.data.sceneLoaderEntity.animalType.value) continue;
                var entity = levelContext.CreateEntity();
                entity.AddBlueprint(index);
                entity.AddGridSize(blueprintData.gridSize);
                entity.AddCellContent(blueprintData.CellMatrix);
                entity.AddTurnsCount(blueprintData.TurnsCount);
                entity.isTurnsEndless = blueprintData.IsTurnEndless;
                entity.AddComplicatorsSchema(blueprintData.ComplicatorsSchema);
                entity.AddAnimalType(blueprintData.animalsType);
                index++;
            }
        }
    }
}