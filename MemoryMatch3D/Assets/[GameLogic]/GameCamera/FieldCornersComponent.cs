using Entitas;
using UnityEngine;

namespace GameCamera
{
    [Game]
    public class FieldCornersComponent : IComponent
    {
        public Vector3[] value;
    }
}