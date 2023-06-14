using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Pokeball
{
    public class ShowAnimalByClickSystem : ReactiveSystem<GameEntity>
    {
        public ShowAnimalByClickSystem(GameContext gameContext) : base(gameContext)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Clicked);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isPokeball && entity.interactable.value;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var pokeballEntity in entities)
            {
                if (!pokeballEntity.isPokeballOpen.value)
                {
                    pokeballEntity.triggerShow = true;
                    if (!pokeballEntity.isPokeballChecked) pokeballEntity.isPokeballChecked = true;
                    return;
                }
            }
        }
    }
}