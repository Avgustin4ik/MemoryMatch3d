using System;
using Core.UI;
using Entitas;

namespace DebugMenu
{
    public class GraphyView : UIElement, IUiShowListener, IUiHideListener
    {
        public void Start()
        {
            Init(Contexts.sharedInstance.ui.CreateEntity());
        }

        public override void Init(IEntity iEntity)
        {
            base.Init(iEntity);
            UIEntity.isGraphyFPSCounter = true;
            UIEntity.AddUiShowListener(this);
            UIEntity.AddUiHideListener(this);
            UIEntity.triggerHide = true;
        }

        public void OnShow(UiEntity entity)
        {
            gameObject.SetActive(true);
        }

        public void OnHide(UiEntity entity)
        {
            gameObject.SetActive(false);
        }

    }
}