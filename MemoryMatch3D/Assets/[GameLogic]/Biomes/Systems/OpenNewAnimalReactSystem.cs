using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Biomes
{
    public class OpenNewAnimalReactSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _gameContext;
        private readonly IGroup<GameEntity> _questionBoxGroup;
        private readonly DataContext _dataContext;
        private readonly IGroup<DataEntity> _requestGroup;

        public OpenNewAnimalReactSystem(DataContext contextsData, GameContext contextsGame) : base(contextsGame)
        {
            _gameContext = contextsGame;
            _dataContext = contextsData;
            _gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.BiomeZone, GameMatcher.AnimalType));
            _dataContext.GetGroup(DataMatcher.AllOf(DataMatcher.AnimalType, DataMatcher.AnimalAvailable));
            _requestGroup = _dataContext.GetGroup(DataMatcher.AllOf(
                DataMatcher.RequestUnlockAnimal,
                DataMatcher.AnimalType));
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.BiomeAnimalReference.Added()); //todo temporary trigger
        }

        protected override bool Filter(GameEntity entity)
        {
            return _dataContext.sceneLoaderEntity.hasAnimalType && 
                entity.biomeZone.value == _dataContext.sceneLoaderEntity.animalType.value;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var requestEntity in _requestGroup.GetEntities())
            {
                //todo: open animal visual logic
                //temporary logic
                foreach (var biomeZoneEntity in entities)
                {
                    if(!biomeZoneEntity.biomeZone.value.Equals(requestEntity.animalType.value)) continue;
#if UNITY_EDITOR
                    UnityEngine.Debug.Log($"Unbox new animal {requestEntity.animalType.value} in Biome zone");
#endif
                    var nextBiomeIndex = biomeZoneEntity.elementIndex.value + 1;
                    _gameContext.CreateEntity().AddRequestOpenNextBiomeZone(nextBiomeIndex);
                }
                requestEntity.Destroy();
            }
        }
    }
}