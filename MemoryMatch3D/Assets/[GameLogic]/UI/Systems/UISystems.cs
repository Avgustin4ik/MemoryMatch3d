using System;
using System.Collections.Generic;
using Entitas;
using Rhodos.Core;

namespace Ui
{
    public sealed class UISystems : Systems
    {
        public UISystems(GameContext gameContext, StateContext stateContext, UiContext uiContext)
        {
            Add(new UIRootSchemaInitializeSystem(uiContext));
            Add(new UIUpdateUserDataMoneyDisplaySystem(gameContext, uiContext));
            Add(new UpdateUiTimerSystem(gameContext, uiContext));
            Add(new UpdateUiTurnsCountSystem(gameContext, uiContext));
            Add(new EnableOrDisabaleUiTrurnsCounterSystem(gameContext, uiContext));
        }
    }

    public class EnableOrDisabaleUiTrurnsCounterSystem : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<UiEntity> _uiCounterGroup;

        public EnableOrDisabaleUiTrurnsCounterSystem(GameContext gameContext, UiContext uiContext) : base(gameContext)
        {
            _uiCounterGroup = uiContext.GetGroup(UiMatcher.TurnsRemaining);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.TurnsLimitByLevel.Added(), GameMatcher.TurnCounterDisabled.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var turnControllerEntity in entities)
            {
                foreach (var uiEntity in _uiCounterGroup)
                {
                    if (turnControllerEntity.isTurnCounterDisabled) uiEntity.triggerHide = true;
                    if (!turnControllerEntity.isTurnCounterDisabled) uiEntity.triggerShow = true;
                }
            }
        }
    }
}