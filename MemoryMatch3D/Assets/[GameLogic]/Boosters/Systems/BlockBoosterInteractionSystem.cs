using System.Collections.Generic;
using Entitas;

namespace Boosters
{
    public class BlockBoosterInteractionSystem : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<UiEntity> _boostersGroup;

        public BlockBoosterInteractionSystem(GameContext contextsGame, UiContext contextsUI) : base(contextsGame)
        {
            _boostersGroup = contextsUI.GetGroup(UiMatcher.AllOf(
                UiMatcher.Booster,
                UiMatcher.InGameBooster));
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.CheckEndTurnConditionsRequest);
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var boosterEntity in _boostersGroup)
            {
                boosterEntity.ReplaceInteractable(false);
            }
        }
    }
}