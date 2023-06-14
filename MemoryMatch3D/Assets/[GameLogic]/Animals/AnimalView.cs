using System;
using Core.Extension;
using DG.Tweening;
using Entitas;
using UnityEngine;

namespace Animals
{
    public enum AnimalsType
    {
        Mouse = 0,
        Cat = 1,
        Parrot = 2,
        Dog = 3,
        GoldFish = 4,
        Tortoise = 5,
        Hamster = 6
    }
    public class AnimalView : MonoBehAdvGameLevelCleanup,IGameHideListener, IGameShowListener,  IAnimationBodyListener, IAnimalFreeListener
    {
        [SerializeField] private AnimalModel model;
        [SerializeField] public AnimalsType animalType;
        [SerializeField] private Transform animalSpot;
        private Animator[] animators;
        private MeshRenderer _meshRenderer;
        private Vector3 _defaultScale;
        public string GetTag() => name;
        public override void Init(IEntity iEntity)
        {
            base.Init(iEntity);
            GameEntity.isAnimal = true;
            GameEntity.AddAnimalTag(name);
            GameEntity.AddIsVisible(true);
            GameEntity.AddAnimationBody(AnimalAnimations.Idle_A);
            GameEntity.AddAnimalFreeListener(this);
            GameEntity.AddAnimationBodyListener(this);
            animators = gameObject.GetComponentsInChildren<Animator>(includeInactive: true);
            GameEntity.AddGameHideListener(this);
            GameEntity.AddGameShowListener(this);
            _defaultScale = model.transform.localScale;
        }
        
        public void OnShow(GameEntity entity)
        {
            model.transform.DOScale(_defaultScale,duration).SetEase(Ease.InOutCubic).SetLink(gameObject);
            model.transform.DOLocalMoveY(1, duration).SetEase(Ease.InOutCubic).SetLink(gameObject).SetRelative();
            GameEntity.ReplaceIsVisible(true);
        }
        public void OnHide(GameEntity entity)
        {
            model.transform.DOScale(0,duration).SetEase(Ease.InOutCubic).SetLink(gameObject);
            model.transform.DOLocalMoveY(0, duration).SetEase(Ease.InOutCubic).SetLink(gameObject);
            GameEntity.ReplaceIsVisible(false);
        }

        public void OnAnimationBody(GameEntity entity, AnimalAnimations value)
        {
            foreach (var animator in animators)
            {
                animator.Play(value.ToString());
            }
        }


        private Tween _runDown;
        [SerializeField] private float duration;

        public void OnAnimalFree(GameEntity entity)
        {
            GameEntity.ReplaceAnimationBody(AnimalAnimations.Run);
            _runDown = transform.DOMoveZ(-20,
                    3f)
                .SetEase(Ease.Linear)
                .SetRelative(false)
                .SetLink(gameObject)
                .OnComplete(() => GameEntity.Destroy());
        }

        protected override void OnDestroy()
        {
            _runDown?.Kill();
            base.OnDestroy();
        }
    }
}