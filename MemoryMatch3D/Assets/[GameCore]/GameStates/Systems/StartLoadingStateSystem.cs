using System.Collections.Generic;
using Entitas;

namespace Core.GameStates
{
    public class StartLoadingStateSystem : ReactiveSystem<InputEntity>
    {
        private readonly UiContext _uiContext;
        private readonly IGroup<UiEntity> _loadingScreenGroup;
        private readonly GameContext _gameContext;

        public StartLoadingStateSystem(InputContext contextsInput, GameContext gameContext, UiContext uiContext) : base(contextsInput)
        {
            _uiContext = uiContext;
            _loadingScreenGroup = uiContext.GetGroup(UiMatcher.LoadingScreen);
            _gameContext = gameContext;
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.ButtonStartGame.Added());
        }

        protected override bool Filter(InputEntity entity)
        {
            return entity.isButtonStartGame = true;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            var stateManagerEntity = Contexts.sharedInstance.game.stateManagerEntity;
            stateManagerEntity.stateMainMenu = false;
            stateManagerEntity.stateLoadingLevel = true;
            stateManagerEntity.AddTimerAmount(1f);//todo жесткий таймер на загрузку
            stateManagerEntity.isBoosterUsedFlag = false;//todo debugmode
            foreach (var uiEntity in _loadingScreenGroup.GetEntities())
            {
                uiEntity.triggerShow = true;
            }
            
        }
    }
}