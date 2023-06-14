using System.Collections.Generic;
using Entitas;

namespace Pokeball
{
    public class HideAllAnimalAtIntroEndedSystem : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<GameEntity> _pokeballsGroup;
        private readonly IGroup<UiEntity> _uiTimerGroup;

        public HideAllAnimalAtIntroEndedSystem(GameContext gameContext) : base(gameContext)
        {
            _pokeballsGroup = gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.Pokeball,
                GameMatcher.IsPokeballOpen));
            _uiTimerGroup = Contexts.sharedInstance.ui.GetGroup(UiMatcher.UiTimer);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.TimerAmount.Removed());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isStateManager
                && entity.stateBoosterCutscene
                && entity.hasTimerAmount == false;
        }
        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var stateManager in entities)
            {
                stateManager.stateBoosterCutscene = false;
                stateManager.stateMainGame = true;
                
            }
            foreach (var gameEntity in _pokeballsGroup.GetEntities())
            {
                gameEntity.triggerHide = true;
            }

            foreach (var uiEntity in _uiTimerGroup.GetEntities())
            {
                uiEntity.triggerHide = true;
            }
        }
    }
}