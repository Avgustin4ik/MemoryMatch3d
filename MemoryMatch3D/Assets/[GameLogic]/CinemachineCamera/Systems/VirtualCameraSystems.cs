using GameCamera.Systems;
using UnityEngine;

namespace Core
{
    public class VirtualCameraSystems : Feature
    {
        public VirtualCameraSystems(Contexts contexts)
        {
            Add(new InstantiateVirtualCameraSystem(contexts));
            Add(new ChangeLiveCameraSystem(contexts.game));
            Add(new CheckIsActiveBlendObserverSystem(contexts.game));
        }
    }
}