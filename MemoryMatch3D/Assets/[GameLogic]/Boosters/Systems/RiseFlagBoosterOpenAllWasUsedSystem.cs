using System.Collections.Generic;
using Entitas;
using Ui.Elements;

namespace Boosters
{
    public class RiseFlagBoosterOpenAllWasUsedSystem : ReactiveSystem<UiEntity>
    {
        private readonly GameContext _gameContext;

        public RiseFlagBoosterOpenAllWasUsedSystem(UiContext contextsUI, GameContext gameContext) : base(contextsUI)
        {
            _gameContext = gameContext;
        }

        protected override ICollector<UiEntity> GetTrigger(IContext<UiEntity> context)
        {
            return context.CreateCollector(UiMatcher.BoosterImplementationRequest);
        }

        protected override bool Filter(UiEntity entity)
        {
            return entity.boosterType.value == typeof(BoosterOpenAll);
        }

        protected override void Execute(List<UiEntity> entities)
        {
            _gameContext.stateManagerEntity.isBoosterUsedFlag = true;
        }
    }
}