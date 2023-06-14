using System.Collections.Generic;
using Entitas;

namespace Feedbacks
{
    public class MatchSuccessFeedback : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<GameEntity> _feedbackManagersGroup;

        public MatchSuccessFeedback(GameContext contextsGame) : base(contextsGame)
        {
            _feedbackManagersGroup = contextsGame.GetGroup(GameMatcher.FeedbackManager);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.SuccessCompare.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var feedback in _feedbackManagersGroup)
            {
                feedback.ReplaceTriggerFeedback(Feedbacks.MatchSuccess);
            }
        }
    }
}