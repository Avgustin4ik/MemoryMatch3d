using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Turn
{
    public class FirstPlayerTurnInitializeSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _gameContext;
        private readonly IGroup<GameEntity> _playersGroup;

        public FirstPlayerTurnInitializeSystem(GameContext contextsGame) : base(contextsGame)
        {
            _gameContext = contextsGame;
            _playersGroup = _gameContext.GetGroup(GameMatcher.Player);
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
            _playersGroup.GetEntities().First(p => p.turnOrder.value == 1).isThisPlayersTurn = true;
        }
    }
}