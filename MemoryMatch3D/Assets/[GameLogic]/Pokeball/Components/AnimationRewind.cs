using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Pokeball.Components
{
    [Game, Event(EventTarget.Self, EventType.Added), Event(EventTarget.Self, EventType.Removed)]
    public sealed class AnimationRewind : IComponent
    {
        
    }
}