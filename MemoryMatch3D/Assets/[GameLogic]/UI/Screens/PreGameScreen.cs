using System;
using Core.UI;
using Entitas;
using TMPro;
using Ui.Elements;
using UnityEngine;

namespace Ui.Screens
{
    public class PreGameScreen : UIScreen, IUiShowListener, IUiHideListener
    {
        [SerializeField] private PreGameBoosterShopUi boosterShop;
        public override void Init(IEntity iEntity)
        {
            base.Init(iEntity);
            UIEntity.isPreGameScreen = true;
            UIEntity.AddUiShowListener(this);
            UIEntity.AddUiHideListener(this);
            boosterShop.Init(Contexts.sharedInstance.ui.CreateEntity());
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