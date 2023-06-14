using System.Collections.Generic;
using System.Linq;
using Core.UI;
using DG.Tweening;
using Entitas;
using UnityEngine;

namespace Ui.Elements
{
    public class UiBoostersHand : UIElement, IUiShowListener, IUiHideListener
    {
        private List<BoosterUi> _boosters;

        public override void Init(IEntity iEntity)
        {
            base.Init(iEntity);
            UIEntity.isBoostersHand = true;
            UIEntity.AddUiHideListener(this);
            UIEntity.AddUiShowListener(this);
            _height = GetComponent<RectTransform>().rect.height;
            InitBoosters();
        }

        private void InitBoosters()
        {
            _boosters = GetComponentsInChildren<BoosterUi>(true).ToList();
            var uiContext = Contexts.sharedInstance.ui;
            foreach (var boosterUi in _boosters)
            {
                boosterUi.Init(uiContext.CreateEntity());
            }
        }

        public void OnShow(UiEntity entity)
        {
            gameObject.SetActive(true);
        }

        private float _height;

        public void OnHide(UiEntity entity)
        {
            gameObject.SetActive(false);
        }
    }
}