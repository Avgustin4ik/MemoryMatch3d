using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Animals
{
    [Game]
    public sealed class AnimalComponent : IComponent
    {
    }

    [Game]
    public sealed class IsVisibleComponent : IComponent
    {
        public bool value;
    }

    [Game, Event(EventTarget.Self)]
    public sealed class AnimalTagComponent : IComponent
    {
        public string value;
    }
    #region animatoions

    [Game, Event(EventTarget.Self)]
    public sealed class AnimationBodyComponent : IComponent
    {
        public AnimalAnimations value;
    }

    [Game]
    public sealed class AnimationEyesComponent : IComponent
    {
        public AnimalEyesAnimations value;
    }


    [Game, Event(EventTarget.Self)]
    public sealed class AnimalFreeComponent : IComponent
    {
    }
    
    #endregion
    public enum AnimalAnimations
    {
        Idle_A,
        Eat,
        Fear,
        Clicked,
        Bounce,
        Jump,
        Run,
        Walk,
        Sit
    }
    public enum AnimalEyesAnimations
    {
        Idle
    }
}
