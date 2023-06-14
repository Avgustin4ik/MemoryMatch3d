using System;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Feedbacks
{
    [RequireComponent(typeof(MMF_Player))]
    public class FeedbackTrigger : MonoBehaviour
    {
        [field: SerializeField] public Feedbacks FeedbackType { get; set; }
        private MMF_Player _feedback;

        private void Awake()
        {
            _feedback ??= GetComponent<MMF_Player>();
        }

        public void Play()
        {
            if (_feedback != null) _feedback.PlayFeedbacks();
        }
        
    }
}