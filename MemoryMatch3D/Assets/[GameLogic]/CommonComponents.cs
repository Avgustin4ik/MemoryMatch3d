using Entitas;
using Entitas.CodeGeneration.Attributes;

public class CommonComponents
{
    [Game, Event(EventTarget.Any), Cleanup(CleanupMode.DestroyEntity)]
    public sealed class MatchEventComponent : IComponent
    {
    }

    [Game]
    public sealed class MatchDetectedComponent : IComponent
    {
    }

    [Game, Event(EventTarget.Any), Cleanup(CleanupMode.DestroyEntity)]
    public sealed class MistakeComponent : IComponent
    {
    }

    [Game]
    public sealed class TimerDurationComponent : IComponent
    {
        public float value;
    }

    [Game]
    public sealed class TimerAmountComponent : IComponent
    {
        public float value;
    }

    [Game]
    public sealed class TimerEnabledComponent : IComponent
    {
        
    }
    
    #region animations components

    [Game, Event(EventTarget.Self), Cleanup(CleanupMode.RemoveComponent)]
    public sealed class SkipAnimationComponent : IComponent
    {
    }
    
    #endregion
}
