using System.Collections.Generic;
using Entitas;

namespace Feedbacks
{
    public class OpenPokeballFeedback : ReactiveSystem<GameEntity>
    {
        public OpenPokeballFeedback(GameContext contextsGame) : base(contextsGame)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) => context.CreateCollector(GameMatcher.Show);

        protected override bool Filter(GameEntity entity) => entity.isPokeball;

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var e in entities)
            {
                e.ReplaceTriggerFeedback(Feedbacks.PokeballOpen);
            }
        }
    }
}