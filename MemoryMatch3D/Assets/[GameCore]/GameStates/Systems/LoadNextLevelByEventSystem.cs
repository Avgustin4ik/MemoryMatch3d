using System.Collections.Generic;
using Entitas;

namespace Core.GameStates
{
    public class LoadNextLevelByEventSystem : ReactiveSystem<LevelEntity>
    {
        private readonly IGroup<UiEntity> _loadingScreenGroup;

        public LoadNextLevelByEventSystem(LevelContext levelContext, GameContext contextsGame, UiContext contextsUI) : base(levelContext)
        {
            _loadingScreenGroup = contextsUI.GetGroup(UiMatcher.LoadingScreen);
        }

        protected override ICollector<LevelEntity> GetTrigger(IContext<LevelEntity> context)
        {
            return context.CreateCollector(LevelMatcher.LoadNextGameLevel.Added());
        }

        protected override bool Filter(LevelEntity entity)
        {
            return true;
        }

        protected override void Execute(List<LevelEntity> entities)
        {
            var stateManagerEntity = Contexts.sharedInstance.game.stateManagerEntity;
            stateManagerEntity.stateVictory = false;
            stateManagerEntity.stateLoose = false;
            stateManagerEntity.stateLoadingLevel = true;
            stateManagerEntity.AddTimerAmount(1f);//todo жесткий таймер на загрузку
            foreach (var uiEntity in _loadingScreenGroup.GetEntities())
            {
                uiEntity.triggerShow = true;
            }
        }
    }
}