using System.Collections.Generic;
using Animals;
using Entitas;

namespace Pokeball
{
    public class ReturnIdleAnimationToAnimals : ReactiveSystem<GameEntity>
    {
        public ReturnIdleAnimationToAnimals(GameContext gameContext) : base(gameContext)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.IsVisible);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isAnimal &&
                   entity.isVisible.value == false;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var animalEntity in entities)
            {
                animalEntity.ReplaceAnimationBody(AnimalAnimations.Idle_A);
                // animalEntity.ReplaceAnimationEyes(AnimalEyesAnimations.Idle);
            }
        }
    }
}