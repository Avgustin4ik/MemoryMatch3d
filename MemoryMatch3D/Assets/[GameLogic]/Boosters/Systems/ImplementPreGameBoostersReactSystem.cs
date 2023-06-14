using System.Collections.Generic;
using Entitas;

namespace Boosters
{
    public class ImplementPreGameBoostersReactSystem : ReactiveSystem<InputEntity>
    {
        private readonly IGroup<UiEntity> _boostersGroup;
        private readonly UiContext _uiContext;
        private readonly InputContext _inputContext;
        private readonly GameContext _gameContext;

        public ImplementPreGameBoostersReactSystem(InputContext contextsInput, GameContext contextsGame, UiContext contextsUI) : base(contextsInput)
        {
            _gameContext = contextsGame;
            _inputContext = contextsInput;
            _uiContext = contextsUI;
            _boostersGroup = contextsUI.GetGroup(UiMatcher.AllOf(
                UiMatcher.Booster,
                UiMatcher.PreGameBooster,
                UiMatcher.BoosterSelected));
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.ButtonStartMainGame);
        }

        protected override bool Filter(InputEntity entity)
        {
            return true;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            _inputContext.CreateEntity().isButtonClosePreGameBoosterShop = true;
            foreach (var boosterEntity in _boostersGroup.GetEntities())
            {
                if(boosterEntity.boosterSelected.value == false) continue;
                var request = _uiContext.CreateEntity();
                request.isBoosterImplementationRequest = true;
                request.AddBoosterID(boosterEntity.boosterID.value);
                request.AddBoosterType(boosterEntity.boosterType.value);
                if(_gameContext.GetGroup(GameMatcher.ThisPlayersTurn).GetEntities()[0].isBoostersEndless) continue;
                boosterEntity.Destroy();
            }
        }
    }
}