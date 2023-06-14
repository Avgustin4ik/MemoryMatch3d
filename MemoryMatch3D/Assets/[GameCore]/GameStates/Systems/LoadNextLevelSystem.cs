using System.Collections.Generic;
using Entitas;

namespace Core.GameStates
{
    public class LoadNextLevelSystem : ReactiveSystem<InputEntity>
    {
        private readonly IGroup<UiEntity> _loadingScreenGroup;

        public LoadNextLevelSystem(InputContext contextsInput, GameContext contextsGame, UiContext contextsUI) : base(contextsInput)
        {
            _loadingScreenGroup = contextsUI.GetGroup(UiMatcher.LoadingScreen);
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.ButtonContinueGame);
        }

        protected override bool Filter(InputEntity entity)
        {
            return true;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            var stateManagerEntity = Contexts.sharedInstance.game.stateManagerEntity;
            stateManagerEntity.stateVictory = false;
            stateManagerEntity.stateLoadingLevel = true;
            stateManagerEntity.isBoosterUsedFlag = false;
            stateManagerEntity.AddTimerAmount(1f);//todo жесткий таймер на загрузку
            foreach (var uiEntity in _loadingScreenGroup.GetEntities())
            {
                uiEntity.triggerShow = true;
            }
        }
    }
}