using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Core.Common
{
    [Game, Ui]
    public sealed class HashCodeComponent : IComponent // use for GameObject only!
    {
        [PrimaryEntityIndex] public int value;
    }
    
    [Game]
    public sealed class TransformComponent : IComponent // readonly!
    {
        public Transform value;
    }

    [Game, Event(EventTarget.Self)]
    public sealed class PositionComponent : IComponent // write transform position (world/local)
    {
        public Vector3 value;
    }

    [Game, Event(EventTarget.Self)]
    public sealed class RotationComponent : IComponent // write transform rotation (world/local)
    {
        public Quaternion value;
    }
    
    [Game, Event(EventTarget.Self)]
    public sealed class ScaleComponent : IComponent // write transform scale (world/local)
    {
        public Vector3 value;
    }
    
    [Ui]
    public sealed class RectTransformComponent : IComponent // readonly!
    {
        public RectTransform value;
    }
    
    [Game, Event(EventTarget.Self)]
    public sealed class RectComponent : IComponent
    {
        public Rect value;
    }

    [Game, Event(EventTarget.Self)]
    public sealed class RigidbodyComponent : IComponent // readonly!
    {
        public Rigidbody value;
    }
    
    
    [Game, Event(EventTarget.Self)]
    public sealed class ColliderComponent : IComponent // readonly!
    {
        public Collider value;
    }

    [Game, Ui, Event(EventTarget.Self)]
    public sealed class GameObjectEnabledComponent : IComponent
    {
        public bool value;
    }
    
    [Game, Ui, Event(EventTarget.Self)]
    public sealed class GameObjectNameComponent : IComponent
    {
        public string value;
    }
    
    [Game, Ui ,Event(EventTarget.Self),Cleanup(CleanupMode.RemoveComponent), FlagPrefix("Trigger")]
    public class ShowComponent : IComponent
    {
    }

    [Game,Ui, Event(EventTarget.Self),Cleanup(CleanupMode.RemoveComponent), FlagPrefix("Trigger")]
    public class HideComponent : IComponent
    {
    }

    [Game]
    public class InteractableComponent : IComponent
    {
        public bool value;
    }
}