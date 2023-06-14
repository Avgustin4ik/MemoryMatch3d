using Entitas;

namespace Feedbacks
{
    
    public enum Feedbacks
    {
        PokeballOpen,
        MatchSuccess
    }
    
    public class FeedbackManagerView : FeedbackStandaloneManagerView
    {
        public override void Init(IEntity iEntity)
        {
            var gameEntity = iEntity as GameEntity;
            if (gameEntity != null)
            {
                gameEntity.isFeedbackManager = true;
                gameEntity.AddTriggerFeedbackListener(this);
            }
        }
    }
}