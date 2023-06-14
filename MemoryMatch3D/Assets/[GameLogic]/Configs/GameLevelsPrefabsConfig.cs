using Core.Configs;
using Core.GameLevels;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Game/Create GameLevelsConfig", fileName = "GameLevelsConfig", order = 0)]
public class GameLevelsPrefabsConfig : Config
{
    [SerializeField] private LevelSchema[] gameLevelsPrefabs;

    public LevelSchema GetRandomLevel()
    {
        return gameLevelsPrefabs[Random.Range(0, gameLevelsPrefabs.Length)];
    }
    public LevelSchema GetLevel(int index)
    {
        return gameLevelsPrefabs[index];
    }
}
