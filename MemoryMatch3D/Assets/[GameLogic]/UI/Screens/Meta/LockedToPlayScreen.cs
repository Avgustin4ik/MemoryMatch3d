using System.Linq;
using Core.UI;
using DG.Tweening;
using Entitas;
using UnityEngine;

namespace Ui.Screens.Meta
{
    public class LockedToPlayScreen : UIScreen,
        IUiShowListener,
        IUiHideListener
    {
        [SerializeField] private float scaleTweenDuration;

        public override void Init(IEntity iEntity)
        {
            base.Init(iEntity);
            UIEntity.isLockedToPlayScreen = true;
            UIEntity.AddUiHideListener(this);
            UIEntity.AddUiShowListener(this);
        }
        
        

        public void OnShow(UiEntity entity)
        {
            Open();
            this.transform.DOScale(1,scaleTweenDuration).SetEase(Ease.OutBack).SetLink(this.gameObject);
        }

        

        public void OnHide(UiEntity entity)
        {
            this.transform.DOScale(0, scaleTweenDuration*0.5f).SetEase(Ease.InCubic).SetLink(this.gameObject)
                .OnComplete(() => { gameObject.SetActive(false); });
        }
        
    }
}