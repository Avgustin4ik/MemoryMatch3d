using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Core.DataStorage.Components
{
    [Data, Ui,Event(EventTarget.Self)]
    public sealed class UnlockProgressComponent : IComponent
    {
        public int value;
    }
}