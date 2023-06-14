using System.Collections.Generic;
using Entitas;

namespace Boosters
{
    public class EnableBoosterInteractionSystem : ReactiveSystem<UiEntity>
    {
        private readonly IGroup<UiEntity> _boostersGroup;

        public EnableBoosterInteractionSystem(GameContext contextsGame, UiContext contextsUI) : base(contextsUI)
        {
            _boostersGroup = contextsUI.GetGroup(UiMatcher.AllOf(
                UiMatcher.Booster,
                UiMatcher.Interactable));
        }

        protected override ICollector<UiEntity> GetTrigger(IContext<UiEntity> context)
        {
            return context.CreateCollector(UiMatcher.TurnEndEventUI.Added());
        }

        protected override bool Filter(UiEntity entity)
        {
            return true;
        }

        protected override void Execute(List<UiEntity> entities)
        {
            foreach (var boosterEntity in _boostersGroup)
            {
                boosterEntity.ReplaceInteractable(true);
            }
        }
    }
}