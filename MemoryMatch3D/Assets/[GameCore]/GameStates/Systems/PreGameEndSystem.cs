using System.Collections.Generic;
using Entitas;

namespace Core.GameStates
{
    public class PreGameEndSystem : ReactiveSystem<InputEntity>
    {
        private readonly GameContext _gameContext;
        private readonly IGroup<UiEntity> _preGameScreenGroup;
        private readonly IGroup<UiEntity> _mainGameScreenGroup;

        public PreGameEndSystem(InputContext inputContext, GameContext gameContext) : base(inputContext)
        {
            _gameContext = gameContext;
            _preGameScreenGroup = Contexts.sharedInstance.ui.GetGroup(UiMatcher.PreGameScreen);
            _mainGameScreenGroup = Contexts.sharedInstance.ui.GetGroup(UiMatcher.MainGameScreen);
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.ButtonClosePreGameBoosterShop);
        }

        protected override bool Filter(InputEntity entity)
        {
            return true;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            _gameContext.stateManagerEntity.statePreGame = false;
            foreach (var uiEntity in _preGameScreenGroup.GetEntities())
            {
                uiEntity.triggerHide = true;
            }

            foreach (var uiEntity in _mainGameScreenGroup.GetEntities())
            {

                uiEntity.triggerShow = true;
            }

            _gameContext.stateManagerEntity.stateMainGame = true;
        }
    }
}