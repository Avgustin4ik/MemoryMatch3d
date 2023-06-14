
using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Turn
{
    [Game, Unique]
    public sealed class TurnControllerComponent : IComponent
    {
    }

    [Game, Event(EventTarget.Any), Cleanup(CleanupMode.DestroyEntity)]
    public sealed class EndTurnRequestComponent : IComponent
    {
    }
    
    [Game, Event(EventTarget.Any), Cleanup(CleanupMode.DestroyEntity)]
    public sealed class NextPlayerTurnRequestComponent : IComponent
    {
    }

    [Game] //on playerEntity
    public sealed class TurnOrderComponent : IComponent
    {
        public short value;
    }

    [Game]
    public sealed class ThisPlayersTurnComponent : IComponent
    {
        
    }

    [Game]
    public sealed class TurnTotalCountComponent : IComponent
    {
        public long value;
    }

    [Game, Cleanup(CleanupMode.DestroyEntity)]
    public sealed class CheckEndTurnConditionsRequestComponent : IComponent
    {
    }

    [Game]
    public sealed class EndTurnConditionsComponent : IComponent
    {
        public EndTurnConditions value;
    }

    [Game]
    public sealed class TurnsLimitByLevelComponent : IComponent
    {
        public uint value;
    }

    [Game]
    public sealed class TotalNumberOfCompletedTurnsComponent : IComponent
    {
        public uint value;
    }

    [Game]
    public sealed class TurnCounterDisabledComponent : IComponent
    {
    }
    
}