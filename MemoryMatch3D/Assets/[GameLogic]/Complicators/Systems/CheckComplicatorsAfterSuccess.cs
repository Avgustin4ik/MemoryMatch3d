using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Complicators
{
    public class CheckComplicatorsAfterSuccess : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _gameContext;
        private readonly ComplicatorsContext _complicatorsContext;
        private readonly IGroup<GameEntity> _pbGroup;

        public CheckComplicatorsAfterSuccess(GameContext contextsGame, ComplicatorsContext contextsComplicators) : base(contextsGame)
        {
            _gameContext = contextsGame;
            _complicatorsContext = contextsComplicators;
            _pbGroup = contextsGame.GetGroup(GameMatcher.AllOf(
                GameMatcher.Pokeball,
                GameMatcher.AnimationProcess));
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(new TriggerOnEvent<GameEntity>[]
            {
                GameMatcher.SuccessCompare.Added(),
            });
        }

        protected override bool Filter(GameEntity entity)
        {
            return _gameContext.stateManagerEntity.stateComplicatorsImplementation;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            if(_pbGroup.count > 0)
                return;
            _complicatorsContext.CreateEntity().isCheckNextAvailableComplicatorRequest = true;
        }
    }
}