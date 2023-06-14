using Entitas;
using UnityEngine;

namespace GameStates
{
    public class TimerExecuteSystem : IExecuteSystem
    {
        private readonly GameContext _gameContext;
        private readonly UiContext _uiContext;

        public TimerExecuteSystem(GameContext gameContext, UiContext uiContext)
        {
            _gameContext = gameContext;
            _uiContext = uiContext;
        }

        public void Execute()
        {
            var stateManagerEntity = _gameContext.stateManagerEntity;
            if(!stateManagerEntity.hasTimerAmount) return;
            if (stateManagerEntity.timerAmount.value <= 0)
            {
                stateManagerEntity.RemoveTimerAmount();
                return;
            }
            stateManagerEntity.ReplaceTimerAmount(stateManagerEntity.timerAmount.value - Time.deltaTime);
        }
    }
}