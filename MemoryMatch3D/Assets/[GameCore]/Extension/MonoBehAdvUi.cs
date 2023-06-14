using Entitas;
using Entitas.Unity;
using UnityEngine;

namespace Core.Extension
{
    public abstract class MonoBehAdvUi : MonoBehAdv
    {
        public UiEntity UIEntity { get; private set; }

        public override void Init(IEntity iEntity)
        {
            base.Init(iEntity);
            UIEntity = (UiEntity)iEntity;
            UIEntity.AddRectTransform(GetComponent<RectTransform>());
            UIEntity.AddHashCode(gameObject.GetHashCode());
        }
        
        public override void OnOnDestroyEntity(IEntity iEntity)
        {
            base.OnOnDestroyEntity(iEntity);
            UIEntity = null;
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
                    UIEntity = null;
                    OnOnDestroyEntity(gameObject.GetEntityLink().entity);
                }
            }

        }
        
        
    }
}
