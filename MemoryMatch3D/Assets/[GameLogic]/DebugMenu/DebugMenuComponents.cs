using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace DebugMenu
{
    [Ui]
    public sealed class DebugScreenComponent : IComponent
    {
    }

    [Input]
    public sealed class LastTimeTouchDetectedComponent : IComponent
    {
        public float value;
    }

    [Input]
    public sealed class TouchForDebugCountComponent : IComponent
    {
        public int value;
    }

    [Game, FlagPrefix("State")]
    public sealed class DebugModeComponent : IComponent
    {
    }

    [Ui, Unique]
    public sealed class GraphyFPSCounterComponent : IComponent
    {
    }

    [Game, Unique]
    public sealed class DebugManagerComponent : IComponent
    {
    }

    [Game]
    public sealed class DisplayFPSAlwaysComponent : IComponent
    {
    }
    [Ui]
    public sealed class DebugScreenBackgroundComponent : IComponent
    {}
    

}