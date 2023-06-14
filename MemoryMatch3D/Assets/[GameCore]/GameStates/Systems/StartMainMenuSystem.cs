using System.Collections.Generic;
using Entitas;

namespace Core.GameStates
{
    public class StartMainMenuSystem : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<UiEntity> _mainMenuScreenGroup;

        public StartMainMenuSystem(GameContext gameContext, UiContext uiContext) : base(gameContext)
        {
            _mainMenuScreenGroup = uiContext.GetGroup(UiMatcher.MainMenuScreen);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.StateManager.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isStateManager;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var stateManager in entities)
            {
                stateManager.stateMainMenu = true;
                foreach (var uiEntity in _mainMenuScreenGroup)
                {
                    uiEntity.triggerShow = true;
                }
            }
        }
    }
}