using Entitas;
using UnityEngine;

namespace GameCamera.Systems
{
    public class InitializeGameCameraSystem : IInitializeSystem
    {
        private readonly GameContext _gameContext;

        public InitializeGameCameraSystem(GameContext contextsGame)
        {
            _gameContext = contextsGame;
        }

        public void Initialize()
        {
            var gameCameraView = Object.FindObjectOfType<GameCameraView>();
            if (gameCameraView == null)
            {
                Debug.LogError("GameCameraView not found");
                return;
            }
            gameCameraView.Init(_gameContext.CreateEntity());
        }
    }
}