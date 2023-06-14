using Entitas;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ui.Elements
{
    public class BoosterUiInGame : BoosterUi
    {
        [SerializeField] protected TMPro.TMP_Text count;
        [SerializeField] protected LayoutElement layoutElement;
        [SerializeField] protected float scaleFactor;
        protected Vector2 _tweenEndPosition;
        protected Vector3 _tweenEndScale;
        protected readonly float _endYPos = 400;
        public override void Init(IEntity iEntity)
        {
            base.Init(iEntity);
            UIEntity.isInGameBooster = true;
            TweenInit();
        }
        private void TweenInit()
        {
            _tweenEndPosition = new Vector2(Camera.main.pixelWidth/2,_endYPos);
            _tweenEndScale = Vector3.one * scaleFactor;
        }
        public override void OnInteractable(UiEntity entity, bool value)
        {
            base.OnInteractable(entity, value);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            
        }

        public override void OnBoosterSelected(UiEntity entity, bool value)
        {
            base.OnBoosterSelected(entity, value);
            layoutElement.ignoreLayout = value;
            _rectTransform.localScale = value ? _tweenEndScale : Vector3.one;
            _rectTransform.position = _tweenEndPosition;
        }

        public override void OnHide(UiEntity entity)
        {
            base.OnHide(entity);
        }

        public override void OnShow(UiEntity entity)
        {
            base.OnShow(entity);
        }
    }
}