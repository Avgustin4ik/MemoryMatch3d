using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace CinemachineCamera.Components
{
    [Game, Event(EventTarget.Self)]
    public sealed class PriorityComponent : IComponent
    {
        public int value;
    }
}