using Entitas;
using UnityEngine;

namespace Animals
{
    public class InitializeAnimalsOnFieldSystem : IInitializeSystem
    {
        private readonly GameContext _gameContext;

        public InitializeAnimalsOnFieldSystem(GameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public void Initialize()
        {
            var animalsGO = Object.FindObjectsOfType<AnimalView>();
            foreach (var animalGameObject in animalsGO)
            {
                var animalEntity = Contexts.sharedInstance.game.CreateEntity();
                animalGameObject.Init(animalEntity);
            }
        }
    }
}