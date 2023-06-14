using System.Collections.Generic;
using Entitas;

namespace Core.GameStates
{
    public class LooseEndLevelSystem : ReactiveSystem<GameEntity>
    {
        private IGroup<UiEntity> _uiScreenGroup;

        public LooseEndLevelSystem(UiContext contextsUI, GameContext contextsGame) : base(contextsGame)
        {
            _uiScreenGroup = contextsUI.GetGroup(UiMatcher.LooseScreen);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Loose);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isStateManager;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            // foreach (var stateManager in entities)
            // {
            //     
            // }
            //todo Loose Level logic
            foreach (var screen in _uiScreenGroup)
            {
                screen.triggerShow = true;
            }
        }
    }
}