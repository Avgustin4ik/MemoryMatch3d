using System.Collections.Generic;
using Entitas;

namespace Turn
{
    public class ResetPlayersTurnAmountAfterLevelEnd : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<GameEntity> _playerGroup;

        public ResetPlayersTurnAmountAfterLevelEnd(GameContext contextsGame) : base(contextsGame)
        {
            _playerGroup = contextsGame.GetGroup(GameMatcher.Player);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.LoadingLevel.Removed());
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var playerEntity in _playerGroup.GetEntities())
            {
                playerEntity.ReplacePlayerTurnsLeft(playerEntity.playersTurnsLimit.value);
            }
        }
    }
}