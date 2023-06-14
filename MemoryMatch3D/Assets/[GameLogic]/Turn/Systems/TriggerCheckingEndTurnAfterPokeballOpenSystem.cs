using System.Collections.Generic;
using Entitas;

namespace Turn
{
    public class TriggerCheckingEndTurnAfterPokeballOpenSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _gameContext;

        public TriggerCheckingEndTurnAfterPokeballOpenSystem(GameContext contextsGame) : base(contextsGame)
        {
            _gameContext = contextsGame;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.PlayerTurnsLeft);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.playerTurnsLeft.value <= 0;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            _gameContext.CreateEntity().isCheckEndTurnConditionsRequest = true;
        }
    }
}