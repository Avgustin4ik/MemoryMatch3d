using System;
using Entitas;
using Entitas.Unity;

namespace Core.Extension
{
    public abstract class MonoBehAdvGame : MonoBehAdv
    {
        public GameEntity GameEntity { get; private set; }

        public override void Init(IEntity iEntity)
        {
            base.Init(iEntity);
            GameEntity = (GameEntity) iEntity;
            GameEntity.AddTransform(transform);
            GameEntity.AddHashCode(gameObject.GetHashCode());
        }
        
        public override void OnOnDestroyEntity(IEntity iEntity)
        {
            base.OnOnDestroyEntity(iEntity);
            GameEntity = null;
            // if(gameObject == null) return;
            if(gameObject.GetEntityLink().entity != null)
                gameObject.Unlink();
            Destroy(gameObject);
        }

        protected virtual void OnDestroy()
        {
            if (gameObject.GetEntityLink()!= null)
            {
                if (gameObject.GetEntityLink().entity != null)
                {
                    GameEntity = null;
                    OnOnDestroyEntity(gameObject.GetEntityLink().entity);
                }
            }

        }
    }
}