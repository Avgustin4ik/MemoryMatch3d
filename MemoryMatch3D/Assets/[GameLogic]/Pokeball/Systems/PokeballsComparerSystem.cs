using System.Collections.Generic;
using System.Linq;
using Core.Configs;
using Entitas;
using UnityEngine;

namespace Pokeball
{
    public class PokeballsComparerSystem : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<GameEntity> _animalsGroup;
        private readonly IGroup<GameEntity> _pokeballsGroup;
        private readonly GameContext _gameContext;

        public PokeballsComparerSystem(GameContext gameContext) : base(gameContext)
        {
            _gameContext = gameContext;
            _animalsGroup = gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.Animal,
                GameMatcher.IsVisible));
            _pokeballsGroup = gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.Pokeball,
                GameMatcher.IsPokeballOpen));
            
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.IsPokeballOpen);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isPokeballOpen.value;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            //todo перемудрил пиздец

            if (!_gameContext.HasEntity(_gameContext.stateManagerEntity)) return; 
            if (_gameContext.stateManagerEntity.stateCutscene) return;
            if (!_gameContext.stateManagerEntity.stateMainGame) return;
            if(_gameContext.stateManagerEntity.stateLoadingLevel) return;

            if (_pokeballsGroup.GetEntities().Count(x => x.isOpenByBooster) == _pokeballsGroup.count)
            {
                foreach (var gameEntity in _pokeballsGroup.GetEntities())
                {
                    gameEntity.triggerHide = true;
                }
                return;
            }

                foreach (var selectedPokeballEntity in entities)
            {
                
                var tag = selectedPokeballEntity.animalTag.value;
                foreach (var pokeballEntity in _pokeballsGroup.GetEntities())
                {
                    if(!pokeballEntity.isPokeballOpen.value) continue;
                    if(!pokeballEntity.animalTag.value.Equals(tag)) continue;
                    if(pokeballEntity.hashCode.value.Equals(selectedPokeballEntity.hashCode.value)) continue;
                    pokeballEntity.isMatchDetected = true;
                    selectedPokeballEntity.isMatchDetected = true;
                    _gameContext.CreateEntity().isMatchEvent = true;
                    Contexts.sharedInstance.ui.CreateEntity().isMatchEventUI = true; 
                }
            }
        }
    }
}