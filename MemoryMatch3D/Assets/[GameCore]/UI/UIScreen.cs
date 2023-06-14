using Core.Extension;
using Entitas;
using UnityEngine;

namespace Core.UI
{
    [DisallowMultipleComponent]
    public abstract class UIScreen : MonoBehAdvUi
    {
        public static UIScreen currentOpenScreen;

        public override void Init(IEntity iEntity)
        {
            base.Init(iEntity);
            gameObject.SetActive(false);
            UIEntity.rectTransform.value.localPosition = Vector2.zero;
        }

        public virtual void Open()
        {
            if(currentOpenScreen != null)
            {
                currentOpenScreen.gameObject.SetActive(false);
            }
           
            currentOpenScreen = this;
            currentOpenScreen.gameObject.SetActive(true);
        }

        protected override void OnDestroy()
        {
            currentOpenScreen = null;
            base.OnDestroy();
        }
    }
}