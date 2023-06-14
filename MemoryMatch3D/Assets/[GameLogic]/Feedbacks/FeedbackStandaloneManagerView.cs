using System.Linq;
using Core.Extension;
using Entitas;
using UnityEngine;

namespace Feedbacks
{
    public class FeedbackStandaloneManagerView : MonoBehAdvGame, ITriggerFeedbackListener
    {
        [SerializeField] private FeedbackTrigger[] _feedbacks;
        public override void Init(IEntity iEntity)
        {
            base.Init(iEntity);
            GameEntity.isFeedbackManager = true;
            GameEntity.AddTriggerFeedbackListener(this);
        }

        public void OnTriggerFeedback(GameEntity entity, Feedbacks value)
        {
            var triggeredFeedback = _feedbacks.Where(f => f.FeedbackType == value);
            triggeredFeedback.FirstOrDefault()?.Play();
        }
    }
}