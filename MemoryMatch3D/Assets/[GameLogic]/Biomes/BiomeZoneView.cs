using Animals;
using Animals.BiomeAnimal;
using CinemachineCamera;
using Core.Extension;
using Entitas;
using UnityEngine;

namespace Biomes
{
    public class BiomeZoneView : MonoBehAdvGame
    {
        [SerializeField] private AnimalsType animalType;
        [SerializeField] private QuestionBoxView questionBoxView;
        [SerializeField] private BiomeAnimalView animal;
        [SerializeField] private VirtualCameraView virtualCameraView;

        public override void Init(IEntity iEntity)
        {
            base.Init(iEntity);
            GameEntity.AddBiomeZone(animalType);
            GameEntity.isBiomeHub = true;
            virtualCameraView.GameEntity.AddAnimalType(animalType);
            var gameContext = Contexts.sharedInstance.game;
            var questionBoxEntity = gameContext.CreateEntity();
            questionBoxView.Init(questionBoxEntity);
            questionBoxEntity.AddAnimalType(animalType);
            var animalEntity = gameContext.CreateEntity();
            animal.Init(animalEntity);
            animalEntity.AddAnimalType(animalType);
            GameEntity.AddBiomeQuestionBoxReference(questionBoxEntity);
            GameEntity.AddBiomeAnimalReference(animalEntity);
        }
    }
}