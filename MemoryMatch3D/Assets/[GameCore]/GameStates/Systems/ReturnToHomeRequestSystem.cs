using System.Collections.Generic;
using Entitas;

namespace Core.GameStates
{
    public class ReturnToHomeRequestSystem : ReactiveSystem<InputEntity>
    {
        public ReturnToHomeRequestSystem(InputContext contextsInput, GameContext contextsGame, UiContext contextsUI) : base(contextsInput) 
        {
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.ButtonReturnToBiomeSceneRequest);
        }

        protected override bool Filter(InputEntity entity)
        {
            return true;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            var instance = SceneLoader.TryGetInstance();
            if(instance == null) return;
            instance.MMLoadGameLevel(instance.MainScene);
        }
    }
}