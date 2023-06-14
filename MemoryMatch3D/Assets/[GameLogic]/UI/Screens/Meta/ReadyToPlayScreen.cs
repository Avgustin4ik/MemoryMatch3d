using System.Linq;
using Core.UI;
using DG.Tweening;
using Entitas;
using UnityEngine;

namespace Ui.Screens.Meta
{
    public class ReadyToPlayScreen : UIScreen,
        IUiShowListener,
        IUiHideListener, IUiUnlockProgressListener, IUiUnlockGoalLimitListener
    {
        [SerializeField] private ProgressSlider progressSlider;
        private DataContext _dataContext;
        [SerializeField] private float scaleTweenDuration;

        public override void Init(IEntity iEntity)
        {
            base.Init(iEntity);
            UIEntity.isReadyToPlayScreen = true;
            UIEntity.AddUiHideListener(this);
            UIEntity.AddUiShowListener(this);
            
            UIEntity.AddUnlockGoalLimit(0);
            UIEntity.AddUnlockProgress(0);
            UIEntity.AddUiUnlockGoalLimitListener(this);
            UIEntity.AddUiUnlockProgressListener(this);
            
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

        private void UpdateLabel(UiEntity entity)
        {
            progressSlider.ProgressLabel.text = $"{entity.unlockProgress.value}/{entity.unlockGoalLimit.value}";
        }

        

        public void OnUnlockProgress(UiEntity entity, int value)
        {
            progressSlider.SliderElement.value = value;
            UpdateLabel(entity);
        }

        public void OnUnlockGoalLimit(UiEntity entity, int value)
        {
            progressSlider.SliderElement.maxValue = value;
            UpdateLabel(entity);
        }
    }
}