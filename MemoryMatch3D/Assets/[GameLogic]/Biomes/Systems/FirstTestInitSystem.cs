using System;
using System.Collections.Generic;
using System.Linq;
using Animals;
using Entitas;

namespace Biomes
{
    public class FirstTestInitSystem : ReactiveSystem<GameEntity>
    {
        private readonly DataContext _dataContext;
        private readonly GameContext _gameContext;
        private readonly IGroup<GameEntity> _biomeZonesGroup;
        private readonly IGroup<DataEntity> _selectedData;
        private readonly UiContext _uiContext;

        public FirstTestInitSystem(GameContext contextsGame, DataContext contextsData) : base(contextsGame)
        {
            _uiContext = Contexts.sharedInstance.ui;
            _gameContext = contextsGame;
            _dataContext = contextsData;
            _biomeZonesGroup = _gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.BiomeZone, GameMatcher.BiomeHub));
            _selectedData = _dataContext.GetGroup(DataMatcher.AllOf(
                DataMatcher.AnimalType,
                DataMatcher.UnlockProgress,
                DataMatcher.UnlockGoalLimit));

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
            var catZoneEntity = _gameContext.GetEntityWithBiomeZone(AnimalsType.Cat);

            // var firstReadyBiome = _biomeZonesGroup.GetEntities().OrderBy(e => e.elementIndex.value)
            //     .FirstOrDefault(e => e.isBiomeZoneReadyToPlay);
            // if (firstReadyBiome != null) firstReadyBiome.isSelected = true;

            // if (catZoneEntity != null)
            // {
            //     catZoneEntity.isLocked = false;
            //     catZoneEntity.isSelected = true;
            //     catZoneEntity.isBiomeZoneReadyToPlay = true;
            //     var readyToPlayScreenEntity = Contexts.sharedInstance.ui.readyToPlayScreenEntity;
            //     // readyToPlayScreenEntity.triggerShow = true;
            //     var dataEntity = _selectedData.GetEntities()
            //         .FirstOrDefault(x => x.animalType.value == catZoneEntity.biomeZone.value);
            //     if (dataEntity != null)
            //     {
            //         var progress = dataEntity.unlockProgress.value;
            //         var limit = dataEntity.unlockGoalLimit.value;
            //         readyToPlayScreenEntity.ReplaceUnlockProgress(progress);
            //         readyToPlayScreenEntity.ReplaceUnlockGoalLimit(limit);
            //     }
            // }


        }
    }
}