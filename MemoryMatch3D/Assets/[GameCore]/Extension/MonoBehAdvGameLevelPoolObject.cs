using Entitas;

namespace Core.Extension
{
    public abstract class MonoBehAdvGameLevelPoolObject : MonoBehAdvGame
    {
        public GameEntity entity { get; private set; }

        public override void Init(IEntity iEntity)
        {
            base.Init(iEntity);
            entity = (GameEntity)iEntity;
            entity.isGameLevelCleanup = true;
        }
    }
}
