using System.Collections.Generic;
using Entitas;

namespace Complicators
{
    public class CheckNextComplicatorsSystem : ReactiveSystem<ComplicatorsEntity>
    {
        private readonly GameContext _gameContext;
        private readonly ComplicatorsContext _complicatorsContext;

        public CheckNextComplicatorsSystem(GameContext contextsGame, ComplicatorsContext contextsComplicators) : base(contextsComplicators)
        {
            _gameContext = contextsGame;
            _complicatorsContext = contextsComplicators;
        }

        protected override ICollector<ComplicatorsEntity> GetTrigger(IContext<ComplicatorsEntity> context)
        {
            return context.CreateCollector(ComplicatorsMatcher.Complicator.Removed());
        }

        protected override bool Filter(ComplicatorsEntity entity)
        {
            return _gameContext.stateManagerEntity.stateComplicatorsImplementation;
        }

        protected override void Execute(List<ComplicatorsEntity> entities)
        {
            _complicatorsContext.CreateEntity().isCheckNextAvailableComplicatorRequest = true;
        }
    }
}