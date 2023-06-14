using System.Collections.Generic;
using Entitas;

namespace Boosters
{
    public class ClearCellFocusBySelectionCancellingSystem : ReactiveSystem<UiEntity>
    {
        private readonly IGroup<GameEntity> _cellGroup;
        private readonly GameContext _gameContext;

        public ClearCellFocusBySelectionCancellingSystem(UiContext contextsUI, GameContext contextsGame) : base(contextsUI)
        {
            _gameContext = contextsGame;
            _cellGroup = contextsGame.GetGroup(GameMatcher.AllOf(
                GameMatcher.Cell,
                GameMatcher.InFocus));
        }

        protected override ICollector<UiEntity> GetTrigger(IContext<UiEntity> context)
        {
            return context.CreateCollector(UiMatcher.BoosterSelected);
        }

        protected override bool Filter(UiEntity entity)
        {
            return entity.isPreGameBooster == false;
        }

        protected override void Execute(List<UiEntity> entities)
        {
            foreach (var boosterUiEntity in entities)
            {
                if (boosterUiEntity.boosterSelected.value)
                {
                    _gameContext.stateManagerEntity.stateBoosterAiming = true;
                }
                else
                {
                    foreach (var cellEntity in _cellGroup.GetEntities())
                    {
                        cellEntity.ReplaceInFocus(false);
                    }
                    _gameContext.stateManagerEntity.stateBoosterAiming = false;
                }
            }
        }
    }
}