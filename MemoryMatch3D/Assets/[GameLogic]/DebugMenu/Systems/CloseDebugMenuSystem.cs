using System.Collections.Generic;
using Entitas;

namespace DebugMenu
{
    public class CloseDebugMenuSystem : ReactiveSystem<InputEntity>
    {
        public CloseDebugMenuSystem(InputContext inputContext, GameContext gameContext) : base(inputContext)
        {
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.ButtonCloseDebugMenu);
        }

        protected override bool Filter(InputEntity entity)
        {
            return true;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            Contexts.sharedInstance.game.stateManagerEntity.stateDebugMode = false;
        }
    }
}