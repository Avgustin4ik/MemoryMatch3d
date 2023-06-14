using System.Collections.Generic;
using System.Linq;
using Core.Configs;
using Entitas;

namespace Turn
{
    public class InitializeFirstEndTurnConditionsSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _gameContext;
        private readonly IGroup<GameEntity> _pokeballsGroup;
        private readonly int _limit;
        private readonly IGroup<GameEntity> _playerGroup;

        public InitializeFirstEndTurnConditionsSystem(GameContext contextsGame) : base(contextsGame)
        {
            _gameContext = contextsGame;
            _pokeballsGroup = contextsGame.GetGroup(GameMatcher.Pokeball);
            _playerGroup = contextsGame.GetGroup(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.ThisPlayersTurn));
            _limit = ConfigsCatalogsManager.GetConfig<GameConfig>().OpenItemsLimit;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.LoadingLevel.Removed());
        }

        protected override bool Filter(GameEntity entity)   
        {
            return entity.stateLoadingLevel == false;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var turnController = _gameContext.turnControllerEntity;
            if(turnController.hasEndTurnConditions) return;
            var endTurnConditions = new EndTurnConditions();
            endTurnConditions.AddCondition((() =>
            {
                return _playerGroup.GetEntities().FirstOrDefault().playerTurnsLeft.value <= 0;
            }));
            turnController.AddEndTurnConditions(endTurnConditions);
            
        }
    }
}