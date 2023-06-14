using System.Collections.Generic;
using Entitas;

namespace Complicators
{
    public class EndLevelComplicatorsCleanupSystem : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<ComplicatorsEntity> _complicatorsGroup;

        public EndLevelComplicatorsCleanupSystem(GameContext contextsGame, ComplicatorsContext contextsComplicators) : base(contextsGame)
        {
            _complicatorsGroup = contextsComplicators.GetGroup(ComplicatorsMatcher.Complicator);
        }
        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.AnyOf(
                GameMatcher.Victory,
                GameMatcher.Loose));
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isStateManager;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var stateManager in entities)
            {
                stateManager.stateComplicatorsImplementation = false;
                foreach (var complicator in _complicatorsGroup.GetEntities())
                {
                    complicator.Destroy();
                }
            }
        }
    }
}