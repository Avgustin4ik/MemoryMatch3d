using Entitas;

namespace Core.Extension
{
    public abstract class MonoBehAdvGameLevelCleanup : MonoBehAdvGame
    {
        public override void Init(IEntity iEntity)
        {
            base.Init(iEntity);
            GameEntity.isGameLevelCleanup = true;
        }
    }
}
