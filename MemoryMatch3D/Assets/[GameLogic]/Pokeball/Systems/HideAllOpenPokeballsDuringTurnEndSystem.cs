using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Pokeball
{
    public class HideAllOpenPokeballsDuringTurnEndSystem : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<GameEntity> _pokeballsGroup;
        private readonly GameContext _gameContext;

        public HideAllOpenPokeballsDuringTurnEndSystem(GameContext gameContext) : base(gameContext)
        {
            _gameContext = gameContext;
            _pokeballsGroup = gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.Pokeball,
                GameMatcher.IsPokeballOpen));
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.AnyOf(
                GameMatcher.ComplicatorsImplementation,
                GameMatcher.EndTurnRequest));
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var pbEntity in _pokeballsGroup.GetEntities())
            {
                pbEntity.isMatchDetected = false;
                pbEntity.isOpenByBooster = false;
                if(pbEntity.isPokeballOpen.value == false) continue;
                pbEntity.triggerHide = true;
            }
        }
    }
}