using System.Collections.Generic;
using Entitas;

namespace Ui
{
    public class UpdateUiTimerSystem : ReactiveSystem<GameEntity>
    {
        private readonly UiContext _uiContext;
        private readonly GameContext _gameContext;
        private readonly IGroup<UiEntity> _uiTimersGroup;

        public UpdateUiTimerSystem(GameContext gameContext, UiContext uiContext) : base(gameContext)
        {
            _gameContext = gameContext;
            _uiContext = uiContext;
            _uiTimersGroup = uiContext.GetGroup(UiMatcher.UiTimer);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.TimerAmount);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isStateManager
                   && entity.stateBoosterCutscene;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var stateManager in entities)
            {
                foreach (var uiEntity in _uiTimersGroup.GetEntities())
                {
                    
                    if (stateManager.hasTimerAmount == false)
                    {
                        uiEntity.triggerHide = true;
                        continue;
                    }

                    if (stateManager.hasTimerAmount)
                    {
                        uiEntity.triggerShow = true;
                        uiEntity.ReplaceUiTimer(stateManager.timerAmount.value);
                        continue;
                    }
                }
            }
        }
    }
}