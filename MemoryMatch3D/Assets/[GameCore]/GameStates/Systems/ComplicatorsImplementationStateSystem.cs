using System.Collections.Generic;
using Entitas;

namespace Core.GameStates
{
    public class ComplicatorsImplementationStateSystem : ReactiveSystem<ComplicatorsEntity>
    {
        private readonly GameContext _gameContext;
        private readonly IGroup<ComplicatorsEntity> _complicatorsGroup;

        public ComplicatorsImplementationStateSystem(ComplicatorsContext contextsComplicators, GameContext contextsGame) : base(contextsComplicators)
        {
            _gameContext = contextsGame;
            _complicatorsGroup = contextsComplicators.GetGroup(ComplicatorsMatcher.ImplementTrigger);
        }

        protected override ICollector<ComplicatorsEntity> GetTrigger(IContext<ComplicatorsEntity> context)
        {
            return context.CreateCollector(ComplicatorsMatcher.ImplementTrigger.AddedOrRemoved());
        }

        protected override bool Filter(ComplicatorsEntity entity)
        {
            return true;
        }

        protected override void Execute(List<ComplicatorsEntity> entities)
        {
            _gameContext.stateManagerEntity.stateComplicatorsImplementation = _complicatorsGroup.count > 0;
        }
    }
}