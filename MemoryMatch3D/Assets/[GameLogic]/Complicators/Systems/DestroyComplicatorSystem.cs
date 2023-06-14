using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

namespace Complicators
{
    public class DestroyComplicatorSystem : ReactiveSystem<GameEntity>
    {
        private readonly ComplicatorsContext _complicatorsContext;
        private readonly GameContext _gameContext;
        private readonly IGroup<GameEntity> _pokeballOnComplicatorsProcess;
        private readonly IGroup<ComplicatorsEntity> _complicatorsGroup;

        public DestroyComplicatorSystem(GameContext contextsGame, ComplicatorsContext contextsComplicators) : base(contextsGame)
        {
            _gameContext = contextsGame;
            _complicatorsContext = contextsComplicators;
            _pokeballOnComplicatorsProcess = contextsGame.GetGroup(GameMatcher.ComplicatorProcess);
            _complicatorsGroup = contextsComplicators.GetGroup(ComplicatorsMatcher.Complicator);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.ComplicatorProcess.Removed());
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            if (_pokeballOnComplicatorsProcess.count == 0)
            {
                var totalNumberOfCompletedTurns = _gameContext.turnControllerEntity.totalNumberOfCompletedTurns.value;
                var complicatorsEntities = _complicatorsContext.GetEntitiesWithTurnNumber((int)totalNumberOfCompletedTurns)
                    .Where(c => c.isImplementTrigger).ToArray();
                foreach (var complicatorsEntity in complicatorsEntities)
                {
                    Debug.Log("Complicator was destroyed");
                    complicatorsEntity.Destroy();
                }
            }
        }
    }
}