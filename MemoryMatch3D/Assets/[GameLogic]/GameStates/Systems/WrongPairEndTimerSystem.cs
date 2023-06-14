using System.Collections.Generic;
using Entitas;

namespace GameStates.Systems
{
    public class WrongPairEndTimerSystem : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<GameEntity> _pokeballs;

        public WrongPairEndTimerSystem(GameContext gameContext) : base(gameContext)
        {
            _pokeballs = gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.Pokeball,
                GameMatcher.IsPokeballOpen));
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.TimerAmount.Removed());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isStateManager
                   && entity.hasTimerAmount == false
                   && entity.stateWrongPair;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var stateEntity in entities)
            {
                stateEntity.stateWrongPair = false;
                foreach (var entity in _pokeballs.GetEntities())
                {
                    entity.triggerHide = true;
                    entity.isMatchDetected = false;
                }
            }
        }
    }
}