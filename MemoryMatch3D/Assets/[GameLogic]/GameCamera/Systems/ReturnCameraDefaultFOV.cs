using System.Collections.Generic;
using Entitas;

namespace GameCamera.Systems
{
    public class ReturnCameraDefaultFOV : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<GameEntity> _cameraGroup;

        public ReturnCameraDefaultFOV(GameContext contextsGame) : base(contextsGame)
        {
            _cameraGroup = contextsGame.GetGroup(GameMatcher.AllOf(
                GameMatcher.GameCamera));
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.LoadingLevel.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var cameraEntity in _cameraGroup.GetEntities())
            {
                cameraEntity.ReplaceGameCameraOrthographicSize(cameraEntity.defaultOrthographicSize.value,false);
            }
        }
    }
}