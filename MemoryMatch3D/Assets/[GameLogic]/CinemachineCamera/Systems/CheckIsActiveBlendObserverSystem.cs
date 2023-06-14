using Cinemachine;
using Entitas;

namespace Core
{
    public class CheckIsActiveBlendObserverSystem : IExecuteSystem
    {
        private readonly GameContext _gameContext;
        private readonly CinemachineBrain _cinemachineBrain;

        public CheckIsActiveBlendObserverSystem(GameContext contextsGame)
        {
            _gameContext = contextsGame;
            _cinemachineBrain = UnityEngine.Object.FindObjectOfType<CinemachineBrain>(); 
        }

        public void Execute()
        {
            var cinemachineBrainEntity = _gameContext.cinemachineBrainEntity;
            cinemachineBrainEntity.isActiveBlend = _cinemachineBrain.ActiveBlend != null;
        }
    }
}