using System.Collections.Generic;
using Core.Configs;
using Entitas;

namespace GameStates.Systems
{
    public class WrongPairStartTimerCountdownSystem : ReactiveSystem<GameEntity>
    {
        private readonly float _duration;
        private readonly IGroup<GameEntity> _pokeballs;

        public WrongPairStartTimerCountdownSystem(GameContext gameContext) : base(gameContext)
        {
            _duration = ConfigsCatalogsManager.GetConfig<GameConfig>().TimerWrongCompare;
            _pokeballs = gameContext.GetGroup(GameMatcher.Pokeball);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.WrongPair.Added());
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