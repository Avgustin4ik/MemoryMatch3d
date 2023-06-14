using System.Collections.Generic;
using Entitas;

namespace Biomes
{
    public class ReadBiomeData : ReactiveSystem<GameEntity>
    {
        private readonly DataContext _dataContext;
        private readonly GameContext _gameContext;
        private readonly IGroup<DataEntity> _animalDataGroup;

        public ReadBiomeData(GameContext contextsGame, DataContext contextsData) : base(contextsGame)
        {
            _gameContext = contextsGame;
            _dataContext = contextsData;
            _animalDataGroup = _dataContext.GetGroup(DataMatcher.AllOf(DataMatcher.AnimalType,
                    DataMatcher.UnlockProgress,
                    DataMatcher.UnlockGoalLimit)
                .NoneOf(DataMatcher.SceneLoader));
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.BiomeAnimalReference.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var biomeZoneEntity in entities)
            {
                foreach (var animalDataEntity in _animalDataGroup)
                {
                    var animalTypeValue = animalDataEntity.animalType.value;
                    if(animalTypeValue != biomeZoneEntity.biomeZone.value) continue;
                    if (animalDataEntity.isAnimalAvailable == false)
                    {
                        biomeZoneEntity.biomeQuestionBoxReference.value.triggerShow = true;
                        biomeZoneEntity.biomeAnimalReference.value.triggerHide = true;
                        continue;
                    }
                    if (animalDataEntity.isAnimalAvailable)
                    {
                        biomeZoneEntity.isBiomeZoneReadyToPlay = false; //todo временно
                        biomeZoneEntity.biomeQuestionBoxReference.value.triggerHide = true;
                        biomeZoneEntity.biomeAnimalReference.value.triggerShow = true;
                        continue;
                    }
                }
            }
        }
    }
}