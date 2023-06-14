using System.Collections.Generic;
using Entitas;

namespace Ui
{
    public class UpdateUiTurnsCountSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _gameContext;
        private readonly IGroup<UiEntity> _uiTurnCounter;

        public UpdateUiTurnsCountSystem(GameContext gameContext, UiContext uiContext) : base(gameContext)
        {
            _gameContext = gameContext;
            _uiTurnCounter = uiContext.GetGroup(UiMatcher.TurnsRemaining);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.TotalNumberOfCompletedTurns.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isTurnCounterDisabled == false
                && entity.hasTurnsLimitByLevel;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var turnControllerEntity in entities)
            {
                var limit = turnControllerEntity.turnsLimitByLevel.value;
                foreach (var uiTurnController in _uiTurnCounter)
                {
                    uiTurnController.ReplaceTurnsRemaining(limit - turnControllerEntity.totalNumberOfCompletedTurns.value);
                }
            }
        }
    }
}