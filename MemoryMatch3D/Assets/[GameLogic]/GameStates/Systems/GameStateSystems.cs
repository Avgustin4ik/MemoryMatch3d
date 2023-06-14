using UnityEngine;

namespace GameStates.Systems
{
    public class GameStateSystems : Feature
    {
        public GameStateSystems(Contexts contexts)
        {
            Add(new TimerExecuteSystem(contexts.game, contexts.ui));
            //intro

            //wrong pair
            Add(new WrongPairStartTimerCountdownSystem(contexts.game));
            Add(new WrongPairEndTimerSystem(contexts.game));
            //success compare
            Add(new SuccessCompareStartTimerCountdownSystem(contexts.game));
            Add(new SuccessCompareEndTimerSystem(contexts.game));
            //loading timer
            //Skip systems
            Add(new SkipCutscenesByTapSystem(contexts.input, contexts.game));
        }
    }
}