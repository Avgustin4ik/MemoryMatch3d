using System.Collections.Generic;
using Complicators;
using Entitas;
using Entitas.CodeGeneration.Attributes;
// using Services.GameLevelsLoadRule;
using UnityEngine;
using EventType = Entitas.CodeGeneration.Attributes.EventType;

namespace Core.GameLevels
{
    #region Game context

    [Game, Unique, FlagPrefix("isStatic")]
    public sealed class CurrentGameLevelComponent : IComponent
    {
    }
    
    // [Game]
    // public sealed class GameLevelsLoadRuleComponent : IComponent
    // {
    //     public IGameLevelsLoadRule value;
    // }
    
    [Game]
    public sealed class GameLevelCleanupComponent : IComponent
    {
    }

    [Game]
    public sealed class LevelComponent : IComponent
    {
    }
    
    #endregion

    [Level, Unique]
    public sealed class LevelLoaderComponent : IComponent
    {
    }

    [Level]
    public sealed class CurrentLevelNumberComponent : IComponent
    {
        public int value;
    }

    [Level]
    public sealed class NextLevelNumberComponent : IComponent
    {
        public int value;
    }
    [Level, Cleanup(CleanupMode.RemoveComponent)]
    public sealed class LoadingCompleteComponent : IComponent
    {
    }
    


    #region Level context

    [Level, FlagPrefix("event")]
    public class CleanUpGameLevelCompletedComponent : IComponent
    {
    }

    [Level, FlagPrefix("event"), Cleanup(CleanupMode.DestroyEntity)]
    public class LoadNextGameLevelComponent : IComponent
    {
    }

    [Level]
    public sealed class BlueprintComponent : IComponent
    {
        [EntityIndex] public int index;
    }

    [Level]
    public sealed class GridSizeComponent : IComponent
    {
        public Vector2Int value;
    }

    [Level]
    public sealed class CellContentComponent : IComponent
    {
        public int[,] value;
    }

    [Level]
    public sealed class TurnsEndlessComponent : IComponent
    {
        
    }

    [Level]
    public sealed class TurnsCountComponent : IComponent
    {
        public uint value;
    }

    [Level]
    public sealed class FreePreGameBoosterComponent : IComponent
    {
        
    }

    [Level]
    public sealed class ComplicatorsSchemaComponent : IComponent
    {
        public Dictionary<int, ComplicatorData[]> value;
    }
    

    #endregion

}