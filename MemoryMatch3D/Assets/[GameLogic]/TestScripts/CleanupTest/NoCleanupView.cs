using System;
using Core.Extension;
using Entitas;

namespace _GameLogic_.TestScripts.CleanupTest
{
    public class NoCleanupView : MonoBehAdvGame
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