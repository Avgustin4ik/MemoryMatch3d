using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Core.GameStates
{
    [Game, Unique]
    public sealed class StateManagerComponent : IComponent
    {
    }
    [Game, FlagPrefix("State")]
    public sealed class MainMenuComponent : IComponent
    {
    }

    [Game, FlagPrefix("State")]
    public sealed class LooseComponent : IComponent
    {
    }
    
    [Game, FlagPrefix("State")]
    public sealed class MainGameComponent : IComponent
    {
    }

    [Game, FlagPrefix("State")]
    public sealed class CutsceneComponent : IComponent
    {
    }

    [Game,FlagPrefix("State")]
    public sealed class BoosterCutsceneComponent : IComponent
    {
        
    }
    [Game,FlagPrefix("State")]
    public sealed class WrongPairComponent : IComponent
    {
    }

    [Game,FlagPrefix("State")]
    public sealed class SuccessCompareComponent : IComponent
    {
    }

    [Game, FlagPrefix("State")]
    public sealed class LoadingLevelComponent : IComponent
    {
    }
    [Game, FlagPrefix("State")]
    public sealed class VictoryComponent : IComponent
    {
    }

    [Game,FlagPrefix("State")]
    public sealed class PlayerTurnComponent : IComponent
    {
    }

    [Game, FlagPrefix("State")]
    public sealed class BoosterAimingComponent : IComponent
    {
    }

    [Game, FlagPrefix("State")]
    public sealed class BoosterImplementingComponent : IComponent
    {
        
    }

    [Game, FlagPrefix("State")]
    public sealed class PreGameComponent : IComponent
    {
        
    }

    [Game, FlagPrefix("State")]
    public sealed class ComplicatorsImplementationComponent : IComponent
    {
        
    }
}