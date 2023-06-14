using System.Collections.Generic;
using System.Linq;
using Core.Configs;
using Entitas;
using NaughtyAttributes.Test;

namespace Turn
{
    public class CheckEndTurnConditionSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _gameContext;
        private readonly GameConfig _gameConfig;
        private readonly IGroup<GameEntity> _pokeballsGroup;
        private readonly ComplicatorsContext _complicatrosContext;
        private readonly LevelContext _levelContext;

        public CheckEndTurnConditionSystem(GameContext contextsGame) : base(contextsGame)
        {
            _gameContext = contextsGame;
            _gameConfig = ConfigsCatalogsManager.GetConfig<GameConfig>();
            _pokeballsGroup = contextsGame.GetGroup(GameMatcher.AllOf(
                GameMatcher.Pokeball,
                GameMatcher.IsPokeballOpen));
            _complicatrosContext = Contexts.sharedInstance.complicators;
            _levelContext = Contexts.sharedInstance.level;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.CheckEndTurnConditionsRequest);
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var listOfConditions = _gameContext.turnControllerEntity.endTurnConditions.value.ListOfConditions;
            if(!listOfConditions.All(condition => condition())) return;
            //todo endTurn Mechanics
            var turnController = _gameContext.turnControllerEntity;

            var complicatorsOnTurn = _complicatrosContext
                .GetEntitiesWithTurnNumber((int)turnController.totalNumberOfCompletedTurns.value);
            if (complicatorsOnTurn.Count > 0)
            {
                _gameContext.stateManagerEntity.stateComplicatorsImplementation = true; 
                return;
            }
            _gameContext.CreateEntity().isEndTurnRequest = true;
        }
    }
}