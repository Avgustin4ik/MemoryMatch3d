using System.Collections.Generic;
using Entitas;

namespace Pokeball
{
    public class ReturnAnimationSpeedToDefault : ReactiveSystem<GameEntity>
    {
        public ReturnAnimationSpeedToDefault(GameContext contextsGame) : base(contextsGame)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.IsPokeballOpen);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isAnimationProcess == false
                && entity.isAnimationRewind;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var gameEntity in entities)
            {
                gameEntity.isAnimationRewind = false;
            }
        }
    }
}