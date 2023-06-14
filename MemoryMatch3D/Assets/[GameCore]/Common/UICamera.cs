using UnityEngine;

namespace Core.Common
{
    [DisallowMultipleComponent]
    public sealed class UICamera : MonoBehaviour
    {
        public Camera GetCamera => GetComponent<Camera>();
    }
}