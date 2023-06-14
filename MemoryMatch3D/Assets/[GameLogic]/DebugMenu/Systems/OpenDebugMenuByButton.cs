using System.Collections.Generic;
using Entitas;

namespace DebugMenu
{
    public class OpenDebugMenuByButton : ReactiveSystem<InputEntity>
    {
        public OpenDebugMenuByButton(InputContext contextsInput, UiContext contextsUI) : base(contextsInput)
        {
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.ButtonOpenDebugMenu);
        }

        protected override bool Filter(InputEntity entity)
        {
            return true;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            Contexts.sharedInstance.game.stateManagerEntity.stateDebugMode = true;
        }
    }
}