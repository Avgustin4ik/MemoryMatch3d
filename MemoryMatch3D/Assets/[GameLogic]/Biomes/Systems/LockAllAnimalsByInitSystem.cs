using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Biomes
{
    public class LockAllAnimalsByInitSystem : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<GameEntity> _biomeZoneGroup;
        private readonly IGroup<DataEntity> _avaliableAnimalDataGroup;

        public LockAllAnimalsByInitSystem(GameContext contextsGame, DataContext contextsData) : base(contextsGame)
        {
            _biomeZoneGroup = contextsGame.GetGroup(GameMatcher.AllOf(GameMatcher.BiomeZone));
            _avaliableAnimalDataGroup =
                contextsData.GetGroup(DataMatcher.AllOf(DataMatcher.AnimalType, DataMatcher.AnimalAvailable));
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.BiomeZone.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var lastAvailableBiome = -1;
            if (_avaliableAnimalDataGroup.count != 0)
            {
                var gameEntities = _biomeZoneGroup.GetEntities()
                    .Where(biome => _avaliableAnimalDataGroup
                        .GetEntities()
                        .Any(data => data.animalType.value == biome.biomeZone.value));
                if (gameEntities.Any())
                {
                    lastAvailableBiome = gameEntities
                        .Max(biome => biome.elementIndex.value);
                }
            }
            foreach (var biomeEntity in _biomeZoneGroup)
            {
                if (biomeEntity.elementIndex.value <= lastAvailableBiome) continue;
                if (biomeEntity.elementIndex.value == lastAvailableBiome + 1)
                {
                    biomeEntity.isLocked = false;
                    biomeEntity.isBiomeZoneReadyToPlay = true;
                    continue;
                }
                biomeEntity.isLocked = true;
            }
        }
    }
}