using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

namespace Grid
{
    public class CellTriggerPokeballSystem : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<GameEntity> _pokeballGroup;
        private readonly IGroup<GameEntity> _playersGroup;

        public CellTriggerPokeballSystem(GameContext contextsGame) : base(contextsGame)
        {
            _pokeballGroup = contextsGame.GetGroup(GameMatcher.AllOf(
                GameMatcher.Pokeball,
                GameMatcher.IsPokeballOpen));
            _playersGroup = contextsGame.GetGroup(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.ThisPlayersTurn));
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Clicked);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isCell 
                   && entity.hasLinkedPokeball;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var cellEntity in entities)
            {
                var linkedPokeballValue = cellEntity.linkedPokeball.value;
                var pokeballEntity = _pokeballGroup.GetEntities()
                    .First(pb => pb.hashCode.value == linkedPokeballValue)!;
                if(pokeballEntity.isPokeballOpen.value) continue;
                Debug.Log($"Pokeball should be open");
                pokeballEntity.triggerClicked = true;
            }
        }
    }
}