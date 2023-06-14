using System;
using Boosters;
using Core.UI;
using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Ui
{
    #region screens

    

    [Ui]
    public sealed class IntroGameScreenComponent : IComponent
    {
    }
    [Ui]
    public sealed class MainGameScreenComponent : IComponent
    {
    }

    [Ui]
    public sealed class LooseScreenComponent : IComponent
    {
        
    }
    [Ui]
    public sealed class LoadingScreenComponent : IComponent
    {
    }

    [Ui]
    public sealed class PreGameScreenComponent : IComponent
    {
    }
    #endregion

    [Ui, Unique]
    public sealed class UiRootSchemaComponent : IComponent
    {
        public UIRootSchema value;
    }

    [Ui, Event(EventTarget.Self)]
    public sealed class TimerStepComponent : IComponent
    {
        public float value;
    }

    [Ui, Event(EventTarget.Any)]
    public sealed class UserDataMoneyDisplayComponent : IComponent
    {
        public int value;
    }
    [Ui, Event(EventTarget.Any, EventType.Added), Cleanup(CleanupMode.DestroyEntity)]
    public sealed class MatchEventUIComponent : IComponent
    {
    }
    [Ui, Event(EventTarget.Any, EventType.Added), Cleanup(CleanupMode.DestroyEntity)]
    public sealed class TurnEndEventUIComponent : IComponent
    {
    }

    [Ui]
    public sealed class PreGameBoosterShopComponent : IComponent
    {
        
    }
    

    

    #region boosters

    [Ui]
    public sealed class BoosterComponent : IComponent
    {
        
    }

    [Ui]
    public sealed class InGameBoosterComponent : IComponent
    {
        
    }

    [Ui]
    public sealed class PreGameBoosterComponent : IComponent
    {
        
    }

    [Ui]
    public sealed class PreGameBoosterPanelComponent : IComponent
    {
        
    }

    [Ui]
    public sealed class BoostersHandComponent : IComponent
    {
    }

    [Ui, Event(EventTarget.Self)]
    public sealed class BoosterSelectedComponent : IComponent
    {
        public bool value;
    }

    [Ui, Event(EventTarget.Self)]
    public sealed class InteractableComponent : IComponent
    {
        public bool value;
    }

    [Ui]
    public sealed class BoosterIDComponent : IComponent
    {
        public short value;
    }

    [Ui]
    public sealed class BoosterAimTypeComponent : IComponent
    {
        public AimTarget value;
    }

    [Ui]
    public sealed class BoosterTypeComponent : IComponent
    {
        public Type value;
    }

    [Ui]
    public sealed class BoosterImplementationRequestComponent : IComponent
    {
    }

    
    #endregion

    [Ui]
    public sealed class TimerDurationComponent : IComponent
    {
        public float value;
    }

    [Ui, Event(EventTarget.Any)]
    public sealed class UiTimerComponent : IComponent
    {
        public float value;
    }
}