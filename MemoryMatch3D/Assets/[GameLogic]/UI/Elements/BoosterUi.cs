using System;
using Boosters;
using Core.UI;
using DG.Tweening;
using Entitas;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ui.Elements
{
    public class BoosterUi : UIElement, IUiHideListener, IUiShowListener, IPointerDownHandler, IBoosterSelectedListener, IInteractableListener
    {
        
        [SerializeField] protected BoosterData boosterData;
        protected RectTransform _rectTransform;
        


        public override void Init(IEntity iEntity)
        {
            base.Init(iEntity);
            _rectTransform = GetComponent<RectTransform>();
            UIEntity.isBooster = true;
            UIEntity.AddUiHideListener(this);
            UIEntity.AddUiShowListener(this);
            UIEntity.AddBoosterSelected(false); 
            UIEntity.AddBoosterSelectedListener(this);
            UIEntity.AddInteractable(true);
            UIEntity.AddInteractableListener(this);
            UIEntity.AddBoosterID(boosterData.ID);
            UIEntity.AddBoosterAimType(boosterData.AimType);
            
           
        }

        

        public virtual void OnHide(UiEntity entity)
        {
            gameObject.SetActive(false);
        }

        public virtual void OnShow(UiEntity entity)
        {
            gameObject.SetActive(true);
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            // Debug.Log($"Booster selected {UIEntity.boosterSelected.value}");
            if(UIEntity.interactable.value == false) return;
            UIEntity.ReplaceBoosterSelected(!UIEntity.boosterSelected.value);
            // UIEntity.triggerClicked = true;

        }

        public virtual void OnBoosterSelected(UiEntity entity, bool value)
        {
        }

        public virtual void OnInteractable(UiEntity entity, bool value)
        {
            //todo paint in gray while uninteractable
        }
    }
}