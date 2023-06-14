using System.Collections.Generic;
using Entitas;

namespace Core.GameStates
{
    public class MainGameplayStartSystem : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<UiEntity> _uiGameScreenGroup;

        public MainGameplayStartSystem(GameContext gameContext, UiContext uiContext) : base(gameContext)
        {
            _uiGameScreenGroup = uiContext.GetGroup(UiMatcher.MainGameScreen);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.LoadingLevel.Removed());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isStateManager;

        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var stateManager in entities)
            {
                stateManager.statePlayerTurn = true;
                foreach (var uiEntity in _uiGameScreenGroup.GetEntities())
                {
                    uiEntity.triggerShow = true;
                }
            }
        }
    }
}