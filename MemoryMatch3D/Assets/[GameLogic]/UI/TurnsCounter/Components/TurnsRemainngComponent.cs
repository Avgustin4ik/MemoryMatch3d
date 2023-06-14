using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Ui.TurnsCounter.Components
{
    [Ui, Event(EventTarget.Self)]
    public sealed class TurnsRemainingComponent : IComponent
    {
        public uint value;
    }
}