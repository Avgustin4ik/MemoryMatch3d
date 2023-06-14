using System.Collections.Generic;
using Entitas;

namespace Core.GameStates
{
    public class DebugStateReactSystem : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<UiEntity> _debugScreensGroup;

        public DebugStateReactSystem(GameContext contextsGame, UiContext contextsUI) : base(contextsGame)
        {
            _debugScreensGroup = contextsUI.GetGroup(UiMatcher.DebugScreen);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.DebugMode.AddedOrRemoved());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isStateManager;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var stateEntity in entities)
            {
                foreach (var uiEntity in _debugScreensGroup.GetEntities())
                {
                    if(stateEntity.stateDebugMode) uiEntity.triggerShow = true;
                    else uiEntity.triggerHide = true;
                }
            }
        }
    }
}