using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace GameCamera
{
    [Game]
    public sealed class GameCameraComponent : IComponent
    {
    }

    [Game, Event(EventTarget.Self)]
    public sealed class GameCameraFOVComponent : IComponent
    {
        public float value;
    }

    [Game]
    public sealed class GameCameraMainComponent : IComponent
    {
        public Camera value;
    }

    [Game]
    public sealed class GameCameraDetectionOffsetComponent : IComponent
    {
        public float value;
    }

    [Game, Event(EventTarget.Any)]
    public sealed class GameCameraZoomEventComponent : IComponent
    {
        public float value;
    }

    [Game, Cleanup(CleanupMode.DestroyEntity), FlagPrefix("Trigger")]
    public sealed class CheckObjectsInCameraEventComponent : IComponent
    {
    }
}