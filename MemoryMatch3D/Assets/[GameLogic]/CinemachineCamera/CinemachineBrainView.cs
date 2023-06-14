using Cinemachine;
using Core.Extension;
using Entitas;
using UnityEngine;

namespace CinemachineCamera
{
    public class CinemachineBrainView : MonoBehAdvGame
    {
        [SerializeField] private VirtualCameraView[] virtualCameraList;
        [SerializeField] private CinemachineBrain _brain;
        public override void Init(IEntity iEntity)
        {
            base.Init(iEntity);
            GameEntity.isCinemachineBrain = true;
            foreach (var virtualCameraView in virtualCameraList)
            {
                var gameEntity = Contexts.sharedInstance.game.CreateEntity();
                virtualCameraView.Init(gameEntity);
            }
        }
    }
}