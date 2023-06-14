using System;
using System.Collections.Generic;
using Entitas;

namespace Feedbacks
{
    public class FeedbacksSystems : Feature
    {
        public FeedbacksSystems(Contexts contexts)
        {
            Add(new CleanupRequestTriggetFeedbackSystem(contexts.game));
            Add(new FeedbacksInitializeSystems(contexts.game));
            Add(new OpenPokeballFeedback(contexts.game));
            Add(new MatchSuccessFeedback(contexts.game));
        }
    }
}