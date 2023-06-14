using System.Collections.Generic;
using Entitas;

namespace Turn
{
    public class ReplacePlayerTurnAmountSystem : ReactiveSystem<GameEntity>
    {
        public ReplacePlayerTurnAmountSystem(GameContext contextsGame) : base(contextsGame)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.ThisPlayersTurn.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isThisPlayersTurn;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var player in entities)
            {
                player.ReplacePlayerTurnsLeft(player.playersTurnsLimit.value);
            }
        }
    }
}