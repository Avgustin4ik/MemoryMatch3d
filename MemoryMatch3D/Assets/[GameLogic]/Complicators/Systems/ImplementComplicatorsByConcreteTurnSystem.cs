using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Complicators
{
    public class ImplementComplicatorsByConcreteTurnSystem : ReactiveSystem<ComplicatorsEntity>
    {
        private readonly ComplicatorsContext _complicatorsContext;
        private readonly GameContext _gameContext;

        public ImplementComplicatorsByConcreteTurnSystem(GameContext contextsGame, ComplicatorsContext contextsComplicators) : base(contextsComplicators)
        {
            _complicatorsContext = contextsComplicators;
            _gameContext = contextsGame;
        }
        
        protected override ICollector<ComplicatorsEntity> GetTrigger(IContext<ComplicatorsEntity> context)
        {
            return context.CreateCollector(ComplicatorsMatcher.CheckNextAvailableComplicatorRequest);
        }

        protected override bool Filter(ComplicatorsEntity entity)
        {
            return true;
        }

        protected override void Execute(List<ComplicatorsEntity> entities)
        {
            if(_gameContext.stateManagerEntity.stateLoose || _gameContext.stateManagerEntity.stateVictory) return;
            if(_gameContext.stateManagerEntity.stateComplicatorsImplementation == false) return;
            var turnController = _gameContext.turnControllerEntity;
            var turnNumber = (int)turnController.totalNumberOfCompletedTurns.value;
            var complicatorsEntity = _complicatorsContext
                .GetEntitiesWithTurnNumber(turnNumber);
            if (complicatorsEntity.Count == 0)
            {
                _gameContext.stateManagerEntity.stateComplicatorsImplementation = false;
                _gameContext.CreateEntity().isEndTurnRequest = true;
                return;
            }
            var entity = complicatorsEntity.OrderBy(x => x.priority.value).First();
            //create entity with request to implement this complicator on next turn
            if (entity.hasRepeatFrequency && entity.repeatFrequency.value > 0)
            {
                var nextComplicatorsClone = _complicatorsContext.CreateEntity();
                entity.CopyTo(nextComplicatorsClone, false);
                nextComplicatorsClone.ReplaceTurnNumber((int)(turnNumber + entity.repeatFrequency.value));
            }
            if(entity.isImplementTrigger == false) entity.isImplementTrigger = true;
        }
    }
}