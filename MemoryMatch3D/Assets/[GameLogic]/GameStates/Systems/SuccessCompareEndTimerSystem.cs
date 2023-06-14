using System.Collections.Generic;
using Entitas;

namespace GameStates.Systems
{
    public class SuccessCompareEndTimerSystem : ReactiveSystem<GameEntity>
    {
        public SuccessCompareEndTimerSystem(GameContext gameContext) : base(gameContext)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.TimerAmount.Removed());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isStateManager &&
                   entity.stateSuccessCompare &&
                   entity.hasTimerAmount == false;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var stateManager in entities)
            {
                stateManager.stateSuccessCompare = false;
            }
        }
    }
}