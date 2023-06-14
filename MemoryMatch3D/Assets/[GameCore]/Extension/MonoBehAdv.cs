using Entitas;
using Entitas.Unity;
using UnityEngine;

namespace Core.Extension
{
    /// <summary>
    /// Base class for all instantiate object through ECS
    /// </summary>
    public abstract class MonoBehAdv : MonoBehaviour, IViewObject
    {
        public virtual void Init(IEntity iEntity)
        {
            gameObject.Link(iEntity);
            iEntity.OnDestroyEntity += OnOnDestroyEntity;
        }

        public virtual void OnOnDestroyEntity(IEntity iEntity)
        {
            iEntity.OnDestroyEntity -= OnOnDestroyEntity;
            // if(gameObject == null) return;
            // if (gameObject.GetEntityLink() != null)
            // {
            //     gameObject.Unlink();
            // }
        }
        
        // private void OnDestroy()
        // {
        //     if (gameObject.GetEntityLink() != null)
        //     {
        //         var entity = gameObject.GetEntityLink().entity;
        //         if (entity != null)
        //         {
        //             entity.OnDestroyEntity -= OnDestroyEntity;
        //             entity = null;
        //             gameObject.Unlink();
        //         }
        //     }
        // }
    }
}