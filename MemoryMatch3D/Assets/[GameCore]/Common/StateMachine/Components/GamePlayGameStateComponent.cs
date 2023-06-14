using Entitas;

namespace _GameCore_.Common.StateMachine.Components
{
    [Game]
    public sealed class GamePlayGameStateComponent : IComponent, IGameStateComponent
    {
        
    }

    [Game]
    public sealed class IdleStateComponent : IComponent
    {
    }

    [Game]
    public sealed class BoosterImplementationStateComponent : IComponent, IGameStateComponent
    {
    }

    [Game]
    public sealed class WinGameStateComponent : IComponent, IGameStateComponent
    {
    }

    [Game]
    public sealed class LooseGameStateComponent : IComponent, IGameStateComponent
    {
    }

    [Game]
    public sealed class ComplicatorsImplementationStateComponent : IComponent, IGameStateComponent
    {
    }

    [Game]
    public sealed class EndTurnStateComponent : IComponent, IGameStateComponent
    {
    }
    
}