using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Complicators
{
    [Complicators]
    public sealed class ComplicatorComponent : IComponent
    {
    }

    [Complicators, Cleanup(CleanupMode.DestroyEntity)]
    public sealed class CheckNextAvailableComplicatorRequestComponent : IComponent
    {
        
    }

    [Complicators]
    public sealed class RandomIndexNeededComponent : IComponent
    {
    }

    [Complicators]
    public sealed class ShiftRowComponent : IComponent
    {
        public bool leftToRight;
    }

    [Complicators]
    public sealed class ShiftColumnComponent : IComponent
    {
        public bool upToDown;
    }

    [Complicators]
    public sealed class PriorityComponent : IComponent
    {
        public int value;
    }

    [Complicators, Cleanup(CleanupMode.DestroyEntity)]
    public sealed class DestroyComponent : IComponent
    {
    }

    [Complicators]
    public sealed class ImplementTriggerComponent : IComponent
    {
    }

    [Complicators]
    public sealed class ImplementAllTriggerComponent : IComponent
    {
    }

    [Complicators]
    public sealed class IndexComponent : IComponent
    {
        public int value;
    }

    [Complicators]
    public sealed class RepeatFrequencyComponent : IComponent
    {
        public uint value;
    }
    [Complicators]
    public sealed class SwitchPokeballsComponent : IComponent
    {
        public Vector2Int[] value;
    }

    [Complicators]
    public sealed class TurnNumberComponent : IComponent
    {
        [EntityIndex]
        public int value;
    }

    [Game]
    public sealed class ComplicatorProcessComponent : IComponent
    {
    }

    [Game]
    public sealed class CheckComplicatorsRequestComponent : IComponent
    {
    }
}