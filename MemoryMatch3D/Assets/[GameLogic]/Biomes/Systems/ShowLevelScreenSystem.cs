using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Biomes
{
    public class ShowLevelScreenSystem : ReactiveSystem<GameEntity>
    {
        private readonly UiContext _uiContext;
        private readonly DataContext _dataContext;
        private readonly GameContext _gameContext;
        private readonly IGroup<DataEntity> _selectedData;
        private readonly IGroup<GameEntity> _selectedBiome;

        public ShowLevelScreenSystem(GameContext contextsGame, DataContext contextsData, UiContext contextsUI) : base(contextsGame)
        {
            _gameContext = contextsGame;
            _dataContext = contextsData;
            _uiContext = contextsUI;
            _selectedBiome = _gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.BiomeZone,
                GameMatcher.Selected));
            _selectedData = _dataContext.GetGroup(DataMatcher.AllOf(
                DataMatcher.AnimalType,
                DataMatcher.UnlockProgress,
                DataMatcher.UnlockGoalLimit));
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.ActiveBlend.AddedOrRemoved(),GameMatcher.BiomeZoneReadyToPlay.AddedOrRemoved());
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var readyToPlayScreenEntity = _uiContext.readyToPlayScreenEntity;
            var lockedToPlayScreenEntity = _uiContext.lockedToPlayScreenEntity;
            var emptyBiomeScreenEntity = _uiContext.emptyBiomeZoneScreenEntity;
            var cinemachineBrain =  _gameContext.cinemachineBrainEntity;
            if (cinemachineBrain.isActiveBlend)
            {
                readyToPlayScreenEntity.triggerHide = true;
                lockedToPlayScreenEntity.triggerHide = true;
                emptyBiomeScreenEntity.triggerHide = true;
                return;
            }
            foreach (var biomeZone in _selectedBiome)
            {
                if (biomeZone.isBiomeZoneReadyToPlay)
                {
                    readyToPlayScreenEntity.triggerShow = true;
                    var dataEntity = _selectedData.GetEntities()
                        .FirstOrDefault(x => x.animalType.value == biomeZone.biomeZone.value);
                    var progress = dataEntity.unlockProgress.value;
                    var limit = dataEntity.unlockGoalLimit.value;
                    readyToPlayScreenEntity.ReplaceUnlockProgress(progress);
                    readyToPlayScreenEntity.ReplaceUnlockGoalLimit(limit);
                }
                else
                {
                    if (biomeZone.isLocked)
                    {
                        lockedToPlayScreenEntity.triggerShow = true;
                        continue;
                    }

                    emptyBiomeScreenEntity.triggerShow = true;
                }
            }
        }
    }
}