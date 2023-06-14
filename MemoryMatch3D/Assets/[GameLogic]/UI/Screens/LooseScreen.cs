using Core.UI;
using Entitas;
using TMPro;
using UnityEngine;

namespace Ui.Screens
{
    public class LooseScreen : UIScreen, IUiShowListener, IUiHideListener
    {
        public override void Init(IEntity iEntity)
        {
            base.Init(iEntity);
            UIEntity.isLooseScreen = true;
            UIEntity.AddUiHideListener(this);
            UIEntity.AddUiShowListener(this);
        }

        public void OnShow(UiEntity entity)
        {
            Open();
        }

        public void OnHide(UiEntity entity)
        {
            gameObject.SetActive(false);
        }
    }
}