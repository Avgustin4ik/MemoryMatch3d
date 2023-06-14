using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Biomes
{
    public class SelectFirstAvailableBiomeZone : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<DataEntity> _animalDataGroup;
        private readonly IGroup<GameEntity> _biomeZoneGroup;

        public SelectFirstAvailableBiomeZone(GameContext contextsGame, DataContext contextsData) : base(contextsGame)
        {
            _animalDataGroup = contextsData.GetGroup(DataMatcher.AnimalType);
            _biomeZoneGroup = contextsGame.GetGroup(GameMatcher.AllOf(
                GameMatcher.BiomeZone));
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.HouseBiome.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var orderedBiomeZones = _biomeZoneGroup.GetEntities().OrderBy(e => e.elementIndex.value);
            foreach (var gameEntity in orderedBiomeZones)
            {
                var dataEntity = _animalDataGroup.GetEntities().FirstOrDefault(e => e.animalType.value == gameEntity.biomeZone.value);
                if(dataEntity == null) continue;
                if(dataEntity.isAnimalAvailable) continue;
                gameEntity.isSelected = true;
                gameEntity.isBiomeZoneReadyToPlay = true;
                break;
            }
        }
    }
}