using System.Collections.Generic;
using Entitas;

namespace Pokeball
{
    public class SkipPokeballAnimationSystem : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<GameEntity> _pokeballsGroup;

        public SkipPokeballAnimationSystem(GameContext gameContext) : base(gameContext)
        {
            _pokeballsGroup = gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.Pokeball,
                GameMatcher.AnimationProcess));
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Clicked);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isPokeball;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var gameEntity in _pokeballsGroup.GetEntities())
            {
                gameEntity.isSkipAnimation = true;
            }
        }
    }
}