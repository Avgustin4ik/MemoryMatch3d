using Core.Extension;
using Entitas;

namespace _GameLogic_.TestScripts.CleanupTest
{
    public class CleanupView : MonoBehAdvGameLevelCleanup
    {
        public override void Init(IEntity iEntity)
        {
            base.Init(iEntity);
            GameEntity.isTestEntity = true;
        }
        
        private void Awake()
        {
            this.Init(Contexts.sharedInstance.game.CreateEntity());
        }
    }
}