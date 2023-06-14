using System.Collections.Generic;
using Entitas;

namespace GameCamera.Systems
{
    public class ChangeLiveCameraSystem : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<GameEntity> _vcamGroup;

        public ChangeLiveCameraSystem(GameContext contextsGame) : base(contextsGame)
        {
            _vcamGroup = contextsGame.GetGroup(GameMatcher.VirtualCamera);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Live.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isVirtualCamera && entity.isLive;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var vcam in entities)
            {
                vcam.ReplacePriority(100);
                foreach (var vcamMuteEntity in _vcamGroup)
                {
                    if(vcamMuteEntity.Equals(vcam)) continue;
                    vcamMuteEntity.isLive = false;
                    vcamMuteEntity.ReplacePriority(0);
                }
            }
        }
    }
}