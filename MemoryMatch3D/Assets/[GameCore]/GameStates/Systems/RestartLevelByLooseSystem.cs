using System.Collections.Generic;
using Entitas;

namespace Core.GameStates
{
    public class RestartLevelByLooseSystem : ReactiveSystem<InputEntity>
    {
        public RestartLevelByLooseSystem(InputContext contextsInput, UiContext contextsUI) : base(contextsInput)
        {
            
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.ButtonRestartLevel);
        }

        protected override bool Filter(InputEntity entity)
        {
            return true;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            var levelLoader = Contexts.sharedInstance.level.levelLoaderEntity;
            levelLoader.ReplaceNextLevelNumber(levelLoader.currentLevelNumber.value);
            Contexts.sharedInstance.level.CreateEntity().eventLoadNextGameLevel = true;
            
        }
    }
}