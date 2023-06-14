using System.Collections.Generic;
using Core.Configs;
using Entitas;

namespace GameStates.Systems
{
    public class SuccessCompareStartTimerCountdownSystem : ReactiveSystem<GameEntity>
    {
        private readonly float _duration;

        public SuccessCompareStartTimerCountdownSystem(GameContext gameContext) : base(gameContext)
        {
            _duration = ConfigsCatalogsManager.GetConfig<GameConfig>().TimerSuccessCompare;

        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.SuccessCompare.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isStateManager;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var stateManager in entities)
            {
                stateManager.AddTimerAmount(_duration);
            }
        }
    }
}