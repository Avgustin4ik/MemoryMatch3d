using System.Collections.Generic;
using Entitas;

namespace DebugMenu
{
    public class ShowFPSStatsSystem : ReactiveSystem<GameEntity>
    {
        public ShowFPSStatsSystem(GameContext gameContext, UiContext uiContext) : base(gameContext)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.DebugMode.AddedOrRemoved());
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var uiGraphyFPSCounterEntity = Contexts.sharedInstance.ui.graphyFPSCounterEntity;
            var debugManagerEntity = Contexts.sharedInstance.game.debugManagerEntity;
            foreach (var stateManager in entities)
            {
                if (stateManager.stateDebugMode)
                {
                    uiGraphyFPSCounterEntity.triggerShow = true;
                    continue;
                }
                if (debugManagerEntity.isDisplayFPSAlways)
                    uiGraphyFPSCounterEntity.triggerShow = true;
                else
                    uiGraphyFPSCounterEntity.triggerHide = true;
            }
        }
    }
}