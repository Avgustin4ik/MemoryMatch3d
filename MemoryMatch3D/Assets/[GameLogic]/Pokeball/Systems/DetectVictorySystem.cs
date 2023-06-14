using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Pokeball
{
    public class DetectVictorySystem : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<GameEntity> _pokeballsGroup;

        public DetectVictorySystem(GameContext gameContext) : base(gameContext)
        {
            _pokeballsGroup = gameContext.GetGroup(GameMatcher.Pokeball);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.SuccessCompare.Removed());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isStateManager;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            if(_pokeballsGroup.count > 0) return;
            foreach (var stateManager in entities)
            {
                stateManager.stateVictory = true;
            }
        }
    }
}