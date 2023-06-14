using Core.UI;
using Entitas;
using TMPro;
using UnityEngine;

namespace Ui.Screens
{
    public class MainMenuScreen : UIScreen, IUiShowListener, IUiHideListener
    {
        // [SerializeField] private TMP_Text moneyCountText;
        public override void Init(IEntity iEntity)
        {
            base.Init(iEntity);
            UIEntity.isMainMenuScreen = true;
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