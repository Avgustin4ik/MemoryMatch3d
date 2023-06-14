using Core.UI;
using DG.Tweening;
using Entitas;
using TMPro;
using Ui.Elements;
using UnityEngine;

namespace Ui.Screens
{
    public class MainGameScreen : UIScreen,
        IUiShowListener,
        IUiHideListener,
        IAnyUserDataMoneyDisplayListener,
        IAnyMatchEventUIListener,
        IAnyTurnEndEventUIListener
    {
        [SerializeField] private TMP_Text moneyCountText;
        [SerializeField] private TMP_Text matchLabel;
        [SerializeField] private TMP_Text nextTurnLabel;
        [SerializeField] private Vector3 _scaleTarget;
        [SerializeField] private float _duration;
        [SerializeField] private UiBoostersHand _boosterHand;

        public override void Init(IEntity iEntity)
        {
            base.Init(iEntity);
            UIEntity.isMainGameScreen = true;
            UIEntity.AddUiHideListener(this);
            UIEntity.AddUiShowListener(this);
            UIEntity.AddAnyUserDataMoneyDisplayListener(this);
            UIEntity.AddAnyMatchEventUIListener(this);
            UIEntity.AddAnyTurnEndEventUIListener(this);
            
            matchLabel.gameObject.SetActive(false);
            _boosterHand.Init(Contexts.sharedInstance.ui.CreateEntity());
        }

        public void OnShow(UiEntity entity)
        {
            Open();
        }

        public void OnHide(UiEntity entity)
        {
            gameObject.SetActive(false);
        }

        public void OnAnyUserDataMoneyDisplay(UiEntity entity, int value)
        {
            // Debug.Log($"USERDATA ADD {value} MONEY");
            moneyCountText.text = value.ToString();
        }
        [ContextMenu("TWEEN/MATCH")]
        public void OnAnyMatchEventUI(UiEntity entity)
        {
            matchLabel.gameObject.SetActive(true);
            DOTween.Sequence()
                .Append(matchLabel.rectTransform.DOPunchScale(_scaleTarget, _duration, vibrato:0, elasticity:0.2f)
                    .SetEase(Ease.OutCirc))
                .OnStart(() =>
                {
                    matchLabel.alpha = 1;
                    matchLabel.rectTransform.localScale = Vector3.zero;
                })
                .OnComplete(()=>matchLabel.gameObject.SetActive(false));
        }


        public void OnAnyTurnEndEventUI(UiEntity entity)
        {
            nextTurnLabel.gameObject.SetActive(true);
            DOTween.Sequence()
                .Append(nextTurnLabel.rectTransform.DOPunchScale(_scaleTarget, _duration, vibrato:0, elasticity:0.2f)
                    .SetEase(Ease.OutCirc))
                .OnStart(() =>
                {
                    nextTurnLabel.alpha = 1;
                    nextTurnLabel.rectTransform.localScale = Vector3.zero;
                })
                .OnComplete(()=>nextTurnLabel.gameObject.SetActive(false));
        }
    }
}