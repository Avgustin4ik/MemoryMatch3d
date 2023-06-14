using Core.Extension;
using Entitas;
using UnityEngine;

namespace Feedbacks
{
    public class FeedbacksInitializeSystems : IInitializeSystem
    {
        public FeedbacksInitializeSystems(GameContext contextsGame)
        {
        }

        public void Initialize()
        {
            var managerViews = UnityEngine.Object.FindObjectsByType<FeedbackStandaloneManagerView>(FindObjectsSortMode.None);
            Debug.Assert(managerViews.Length == 1, "Too much standalone feedbacks managers in this scene");
            foreach (var feedbackStandaloneManagerView in managerViews)
            {
                feedbackStandaloneManagerView.Init(Contexts.sharedInstance.game.CreateEntity());
            }
            
        }
    }
}