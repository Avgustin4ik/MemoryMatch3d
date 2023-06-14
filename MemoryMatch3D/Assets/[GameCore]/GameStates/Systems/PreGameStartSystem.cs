using System.Collections.Generic;
using Entitas;

namespace Core.GameStates
{
    public class PreGameStartSystem : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<UiEntity> _screenGroup;

        public PreGameStartSystem(GameContext contextsGame, UiContext contextsUI) : base(contextsGame)
        {
            _screenGroup = contextsUI.GetGroup(UiMatcher.PreGameScreen);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.PreGame.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isStateManager;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var stateManager in entities)
            {
                foreach (var screen in _screenGroup.GetEntities())
                {
                    screen.triggerShow = true;
                }
            }
        }
    }
}