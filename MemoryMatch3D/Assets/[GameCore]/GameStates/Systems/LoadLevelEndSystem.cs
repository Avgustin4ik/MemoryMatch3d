using System.Collections.Generic;
using Entitas;

namespace Core.GameStates
{
    public class LoadLevelEndSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _gameContext;

        public LoadLevelEndSystem(GameContext gameContext) : base(gameContext)
        {
            _gameContext = gameContext;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.TimerAmount.Removed());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasTimerAmount == false &&
                   entity.isStateManager &&
                   entity.stateLoadingLevel;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var stateManagerEntity = _gameContext.stateManagerEntity;
            stateManagerEntity.stateLoadingLevel = false;
        }
    }
}