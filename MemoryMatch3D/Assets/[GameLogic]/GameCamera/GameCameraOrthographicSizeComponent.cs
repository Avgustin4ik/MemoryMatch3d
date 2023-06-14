using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace GameCamera
{
    [Game, Event(EventTarget.Self)]
    public class GameCameraOrthographicSizeComponent : IComponent
    {
        public float value;
        public bool animate = true;
    }
}