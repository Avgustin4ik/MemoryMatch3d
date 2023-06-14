using System;
using System.Collections.Generic;
using Entitas;

namespace Core.GameStates
{
    public class StartMainGameSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _gameContext;

        public StartMainGameSystem(GameContext gameContext) : base(gameContext)
        {
            _gameContext = gameContext;
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
            var stateManager = Contexts.sharedInstance.game.stateManagerEntity;
            // stateManager.stateMainGame = true;
            stateManager.statePreGame = true;
            _gameContext.turnControllerEntity.ReplaceTotalNumberOfCompletedTurns(0);
            // stateManager.stateIntro = true;
        }
    }
}
