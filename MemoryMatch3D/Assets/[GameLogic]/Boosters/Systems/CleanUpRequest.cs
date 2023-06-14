using System.Collections.Generic;
using Entitas;

namespace Boosters
{
    public class CleanUpRequest : ReactiveSystem<UiEntity>
    {
        public CleanUpRequest(UiContext contextsUI) : base(contextsUI)
        {
            
        }

        protected override ICollector<UiEntity> GetTrigger(IContext<UiEntity> context)
        {
            return context.CreateCollector(UiMatcher.BoosterImplementationRequest);
        }

        protected override bool Filter(UiEntity entity)
        {
            return true;
        }

        protected override void Execute(List<UiEntity> entities)
        {
            foreach (var request in entities)
            {
                request.Destroy();
            }
        }
    }
}