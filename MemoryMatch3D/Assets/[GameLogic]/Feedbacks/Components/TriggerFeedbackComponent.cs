using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Feedbacks
{
    [Game, Event(EventTarget.Self)]
    public sealed class TriggerFeedbackComponent : IComponent
    {
        public Feedbacks value;
    }
}