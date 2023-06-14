using System.Collections.Generic;
using Entitas;

namespace GameCamera.Systems
{
    public class TriggerCameraZoomCheckSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _gameContext;

        public TriggerCameraZoomCheckSystem(GameContext contextsGame) : base(contextsGame)
        {
            _gameContext = contextsGame;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Pokeball.Removed(), GameMatcher.LoadingLevel.Removed());
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            _gameContext.CreateEntity().triggerCheckObjectsInCameraEvent = true;
        }
    }
}