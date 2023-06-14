using Core.UI;
using DG.Tweening;
using Entitas;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Ui.TurnsCounter
{
    public class TurnsCounterView : UIElement, ITurnsRemainingListener, IUiHideListener, IUiShowListener
    {
        [SerializeField] private TMP_Text counter;
        
        [FoldoutGroup("Tween setup")] [SerializeField] private float scalefactor;
        [FoldoutGroup("Tween setup")] [SerializeField] private float duration;
        [FoldoutGroup("Tween setup")] [SerializeField] private Ease ease;

        public override void Init(IEntity iEntity)
        {
            base.Init(iEntity);
            UIEntity.AddTurnsRemaining(99);
            UIEntity.AddTurnsRemainingListener(this);
            UIEntity.AddUiHideListener(this);
            UIEntity.AddUiShowListener(this);
        }

        public void OnTurnsRemaining(UiEntity entity, uint value)
        {
            counter.text = value.ToString();
            counter.rectTransform.DOPunchScale(Vector3.one * scalefactor, duration, 0, 0)
                .SetEase(ease)
                .SetRelative(true)
                .SetLink(this.gameObject);
        }

        public void OnHide(UiEntity entity)
        {
            gameObject.SetActive(false);
        }

        public void OnShow(UiEntity entity)
        {
            gameObject.SetActive(true);
        }
    }
}