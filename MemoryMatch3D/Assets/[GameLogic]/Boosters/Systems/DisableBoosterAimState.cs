using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Boosters
{
    public class DisableBoosterAimState : ReactiveSystem<UiEntity>
    {
        private readonly IGroup<UiEntity> _boosterGroup;
        private readonly GameContext _gameContext;
        private readonly IGroup<GameEntity> _cellGroup;

        public DisableBoosterAimState(GameContext contextsGame, UiContext contextsUI) : base(contextsUI)
        {
            _gameContext = contextsGame;
            _boosterGroup = contextsUI.GetGroup(UiMatcher.BoosterSelected);
            _cellGroup = contextsGame.GetGroup(GameMatcher.Cell);
        }

        protected override ICollector<UiEntity> GetTrigger(IContext<UiEntity> context)
        {
            return context.CreateCollector(UiMatcher.BoosterImplementationRequest);
            
        }

        protected override bool Filter(UiEntity entity)
        {
            return true;
        }

        protected override void Execute(List<UiEntity> entities)
        {
            _gameContext.stateManagerEntity.stateBoosterAiming = false;
            foreach (var booster in _boosterGroup)
            {
                booster.ReplaceInteractable(true);  
            }
            foreach (var boosterEntity in _boosterGroup.GetEntities().Where(b => b.boosterSelected.value))
            {
                boosterEntity.ReplaceBoosterSelected(false);
            }

            foreach (var cell in _cellGroup.GetEntities())
            {
                cell.ReplaceInFocus(false);
            }
        }
    }
}