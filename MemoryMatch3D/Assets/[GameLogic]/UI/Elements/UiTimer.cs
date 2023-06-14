using Core.UI;
using Entitas;
using TMPro;
using UnityEngine;

namespace Ui.Elements
{
    public class UiTimer : UIElement, IAnyUiTimerListener, IUiShowListener, IUiHideListener
    {
        [SerializeField] private TMP_Text label;
        public override void Init(IEntity iEntity)
        {
            base.Init(iEntity);
            UIEntity.AddUiTimer(0f);
            UIEntity.AddAnyUiTimerListener(this);
            UIEntity.AddUiHideListener(this);
            UIEntity.AddUiShowListener(this);
            UIEntity.triggerHide = true;
        }

        public void OnAnyUiTimer(UiEntity entity, float value)
        {
            label.text =  value.ToString("0");
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