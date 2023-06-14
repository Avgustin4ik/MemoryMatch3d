using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

namespace Complicators
{
    public class CheckComplicatorsAfterFail : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _gameContext;
        private readonly ComplicatorsContext _complicatorsContext;
        private readonly IGroup<GameEntity> _pokeballGroup;


        public CheckComplicatorsAfterFail(GameContext contextsGame, ComplicatorsContext contextsComplicators) : base(contextsGame)
        {
            _gameContext = contextsGame;
            _complicatorsContext = contextsComplicators;
            _pokeballGroup = contextsGame.GetGroup(GameMatcher.Pokeball);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.AnimationProcess.Removed());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isAnimationProcess == false &&
                   entity.hasIsPokeballOpen &&
                   entity.isPokeballOpen.value == false;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            if(_pokeballGroup.GetEntities().Any(pb=> pb.isAnimationProcess))
                return;
            if (_gameContext.stateManagerEntity.stateComplicatorsImplementation)
            {
                _complicatorsContext.CreateEntity().isCheckNextAvailableComplicatorRequest = true;
            }
        }
    }
}