using CinemachineCamera;
using Entitas;
using UnityEngine;

namespace Core
{
    public class InstantiateVirtualCameraSystem : IInitializeSystem
    {
        public InstantiateVirtualCameraSystem(Contexts contexts)
        {
        }

        public void Initialize()
        {
            var findObjectOfType = Object.FindObjectOfType<CinemachineBrainView>();
            if (findObjectOfType == null)
            {
                Debug.LogError("CinemachineBrainView not found");
                return;
            }
            findObjectOfType.Init(Contexts.sharedInstance.game.CreateEntity());
        }
    }
}