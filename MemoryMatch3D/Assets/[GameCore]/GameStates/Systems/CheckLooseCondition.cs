using System.Collections.Generic;
using Core.Configs;
using Entitas;

namespace Core.GameStates
{
    public class CheckLooseCondition : ReactiveSystem<GameEntity>
    {
        private readonly uint _limit;
        private readonly GameContext _gameContext;
        private readonly IGroup<UiEntity> _uiScreenGroup;

        public CheckLooseCondition(GameContext contextsGame, UiContext uiContext) : base(contextsGame)
        {
            _gameContext = contextsGame;
            _uiScreenGroup = uiContext.GetGroup(UiMatcher.LooseScreen);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.TotalNumberOfCompletedTurns);
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var turnControllerEntity in entities)
            {
                if (turnControllerEntity.isTurnCounterDisabled) continue;
                if (turnControllerEntity.hasTurnsLimitByLevel == false) continue;
                if(turnControllerEntity.totalNumberOfCompletedTurns.value < turnControllerEntity.turnsLimitByLevel.value) continue;
                _gameContext.stateManagerEntity.stateLoose = true;
            }
            
        }
    }
}