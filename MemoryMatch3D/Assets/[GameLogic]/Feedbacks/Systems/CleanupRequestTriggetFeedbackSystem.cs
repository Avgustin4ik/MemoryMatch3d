using Entitas;

namespace Feedbacks
{
    public class CleanupRequestTriggetFeedbackSystem : ICleanupSystem
    {
        private readonly IGroup<GameEntity> _requestGroup;

        public CleanupRequestTriggetFeedbackSystem(GameContext contextsGame)
        {
            _requestGroup = contextsGame.GetGroup(GameMatcher.TriggerFeedback);
        }

        public void Cleanup()
        {
            foreach (var entity in _requestGroup.GetEntities())
            {
                entity.RemoveTriggerFeedback();
            }
        }
    }
}