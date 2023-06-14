using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Core.Input
{
    [Input, Cleanup(CleanupMode.DestroyEntity)]
    public sealed class ButtonStartGameComponent : IComponent
    {
    }

    [Input, Cleanup(CleanupMode.DestroyEntity)]
    public sealed class ButtonContinueGameComponent : IComponent
    {
    }

    [Input, Cleanup(CleanupMode.DestroyEntity)]
    public sealed class ButtonOpenDebugMenuComponent : IComponent
    {
    }

    [Input, Cleanup(CleanupMode.DestroyEntity)]
    public sealed class ButtonCloseDebugMenuComponent : IComponent
    {
    }

    [Input, Cleanup(CleanupMode.DestroyEntity)]
    public sealed class ButtonRestartLevelComponent : IComponent
    {
    }

    [Input, Cleanup(CleanupMode.DestroyEntity)]
    public sealed class ButtonClosePreGameBoosterShopComponent : IComponent
    {
    }

    [Input, Cleanup(CleanupMode.DestroyEntity)]
    public sealed class ButtonStartMainGameComponent : IComponent
    {
    }

    [Input, Cleanup(CleanupMode.DestroyEntity)]
    public sealed class ButtonLooseLevelComponent : IComponent
    {
    }
}