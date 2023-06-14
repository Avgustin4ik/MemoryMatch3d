using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;
using EventType = Entitas.CodeGeneration.Attributes.EventType;

namespace Pokeball
{
    [Game]
    public sealed class PokeballComponent : IComponent
    {
    }
    [Game]
    public sealed class IsPokeballOpenComponent : IComponent
    {
        public bool value;
    }
    [Game]
    public sealed class LinkedAnimalHashcodeComponent : IComponent
    {
        public int value;
    }
    
    [Game,Ui,FlagPrefix("Trigger"),Event(EventTarget.Self),Cleanup(CleanupMode.RemoveComponent)]
    public sealed class ClickedComponent : IComponent
    {
    }
    [Game]
    public sealed class AnimationProcessComponent : IComponent
    {
    }

    [Game]
    public sealed class PokeballCheckedComponent : IComponent
    {
    }

    [Game]
    public sealed class BoundsComponent : IComponent
    {
        public Bounds value;
    }

    [Game]
    public sealed class OpenByBoosterComponent : IComponent
    {
        
    }

    [Game, Event(EventTarget.Self, EventType.Added)]
    public sealed class MoveToTargetCellComponent : IComponent
    {
        public int targetHashcode;
        public Vector2Int breakBorderDirection = Vector2Int.zero;
    }

    [Game, Event(EventTarget.Self)]
    public sealed class SwitchToTargetCellComponent : IComponent
    {
        public int targetHashcode;
        public bool isFirst = true;
    }

    [Game]
    public sealed class BorderNearbyComponent : IComponent
    {
        public Vector2Int[] value;
    }
}