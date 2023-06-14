using System.Collections.Generic;
using Core.Configs;
using Entitas;
using UnityEngine;

namespace Core.GameLevels
{
    public class LoadLevelSystem : ReactiveSystem<GameEntity>
    {
        private readonly LevelContext _levelContext;
        private readonly GameContext _gameContext;
        private readonly IGroup<GameEntity> _gameLevelCleanupGroup;
        private readonly IGroup<LevelEntity> _blueprintsGroup;
        private readonly GameLevelsPrefabsConfig _gameLevelConfig;

        public LoadLevelSystem(LevelContext levelContext, GameContext gameContext) : base(gameContext)
        {
            _levelContext = levelContext;
            _gameContext = gameContext;
            _gameLevelCleanupGroup = gameContext.GetGroup(GameMatcher.GameLevelCleanup);
            _blueprintsGroup = levelContext.GetGroup(LevelMatcher.Blueprint);
            _gameLevelConfig = ConfigsCatalogsManager.GetConfig<GameLevelsPrefabsConfig>();
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.LoadingLevel.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isStateManager;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var stateManager in entities)
            {
                foreach (var gameLevelEntity in _gameLevelCleanupGroup.GetEntities())
                {
                    gameLevelEntity.Destroy();
                }
                var loaderEntity = _levelContext.levelLoaderEntity;
                var nextLevelNumber = loaderEntity.nextLevelNumber.value;
                if(nextLevelNumber >= _blueprintsGroup.count)
                    nextLevelNumber = _blueprintsGroup.count - 1;
                var levelObject = Object.Instantiate<LevelSchema>(
                    (_gameLevelConfig.GetLevel(0)));
                levelObject.Init(_gameContext.CreateEntity());
                loaderEntity.isLoadingComplete = true;
                //prepear to next level
                loaderEntity.ReplaceCurrentLevelNumber(nextLevelNumber);
                loaderEntity.ReplaceNextLevelNumber(nextLevelNumber + 1);
            }
        }
    }
}