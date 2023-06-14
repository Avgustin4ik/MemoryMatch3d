using Entitas;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ui.Elements
{
    public class BoosterUiPreGame : BoosterUi
    {
        private const float Sf = 1.5f;
       

        public override void Init(IEntity iEntity)
        {
            base.Init(iEntity);
            UIEntity.isPreGameBooster = true;
        }

        public override void OnBoosterSelected(UiEntity entity, bool value)
        {
            base.OnBoosterSelected(entity, value);
            _rectTransform.localScale = value ? Sf * Vector3.one : Vector3.one;
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
        }
    }
}