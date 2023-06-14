using System.Collections.Generic;
using Entitas;

namespace Turn
{
    public class DecreasePlayersTurnsAmountByOpenedPokeball : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<GameEntity> _playersGroup;

        public DecreasePlayersTurnsAmountByOpenedPokeball(GameContext contextsGame) : base(contextsGame)
        {
            _playersGroup = contextsGame.GetGroup(GameMatcher.ThisPlayersTurn);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.IsPokeballOpen);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isPokeballOpen.value && entity.isOpenByBooster == false;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            
            foreach (var playerEntity in _playersGroup.GetEntities())
            {
                playerEntity.ReplacePlayerTurnsLeft(playerEntity.playerTurnsLeft.value - 1);
            }
        }
    }
}