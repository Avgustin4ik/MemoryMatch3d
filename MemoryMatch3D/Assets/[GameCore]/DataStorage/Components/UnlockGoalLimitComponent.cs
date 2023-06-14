using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Core.DataStorage.Components
{
    [Data, Ui, Event(EventTarget.Self)]
    public sealed class UnlockGoalLimitComponent : IComponent
    {
        public int value;
    }
}