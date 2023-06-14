using System;
using System.Collections.Generic;
using System.Linq;
using Animals;
using Core.Extension;
using Entitas;
using UnityEngine;
using DG.Tweening;
using Feedbacks;
using UnityEditor;

namespace Pokeball
{
    public class PokeballView : MonoBehAdvGameLevelCleanup, 
        IGameShowListener,
        IGameHideListener,
        ISkipAnimationListener,
        IMoveToTargetCellListener,
        ISwitchToTargetCellListener,
        IAnimationRewindListener,
        IAnimationRewindRemovedListener
    {
        [SerializeField] private PokeballModelView[] pokeballModelViews;
        [SerializeField] private Transform animalSpawnSpot;
        [SerializeField] private ParticleSystem vfxClick;
        [SerializeField] private FeedbackManagerView feedbackManagerView;
        private AnimalView _animalView;
        public override void Init(IEntity iEntity)
        {
            base.Init(iEntity);
            GameEntity.isPokeball = true;
            GameEntity.AddIsPokeballOpen(true); //todo продублировать системой
            GameEntity.AddInteractable(true); //game logic dependencies
            GameEntity.AddGameHideListener(this);
            GameEntity.AddGameShowListener(this);
            GameEntity.AddSkipAnimationListener(this);
            GameEntity.AddMoveToTargetCellListener(this);
            GameEntity.AddSwitchToTargetCellListener(this);
            GameEntity.AddBounds(gameObject.GetComponent<Collider>().bounds);
            GameEntity.AddAnimationRewindListener(this);
            GameEntity.AddAnimationRewindRemovedListener(this);
            if(feedbackManagerView != null) feedbackManagerView.Init(iEntity);
            Hide();
        }

        private void Awake()
        {
            pokeballModelViews[0].onAnimationBegin += OnFirstAnimationBegin;
            pokeballModelViews[0].onAnimationEnd += OnFirstAnimationEnd;
            pokeballModelViews[1].onAnimationBegin += OnSecondAnimationBegin;
            pokeballModelViews[1].onAnimationEnd += OnSecondAnimationEnd;
        }
        private bool IsNormalDirection(PokeballModelView pokeballModelView)
        {
            return pokeballModelView.Animator.GetFloat(Speed) > 0;
        }

        public void SetAnimal(AnimalView animal)
        {
            var animalTransform = animal.transform;
            animalTransform.position = animalSpawnSpot.position;
            animalTransform.rotation = animalSpawnSpot.rotation;
            _animalView = animal;
            InitializeAnimal();
        }

        private void InitializeAnimal()
        {
            GameEntity.AddAnimalTag(_animalView.GetTag());
            GameEntity.AddLinkedAnimalHashcode(_animalView.gameObject.GetHashCode());
            _animalView.Init(Contexts.sharedInstance.game.CreateEntity());
            _animalView.GameEntity.ReplaceIsVisible(false);
            _animalView.GameEntity.triggerHide = true;
        }

        private void SetAnimationDirection(bool isNormalDirection)
        {
            pokeballModelViews[0].Animator.SetFloat(Speed, isNormalDirection ? 1 : -1);
            pokeballModelViews[1].Animator.SetFloat(Speed, isNormalDirection ? 1 : -1);
        }


        private void OnFirstAnimationBegin()
        {
            if(IsNormalDirection(pokeballModelViews[0])) return;
            // Debug.Log("First Animation Begin");
            // if(GameEntity == null) return;
            GameEntity.isAnimationProcess = false;
            GameEntity.ReplaceIsPokeballOpen(false);
            _animalView.GameEntity.ReplaceIsVisible(false);
            GameEntity.ReplaceInteractable(true);
            GameEntity.isOpenByBooster = false;
        }

        private void OnFirstAnimationEnd()
        {
            // Debug.Log("First Animation End");
            if(IsNormalDirection(pokeballModelViews[0])) _animalView.OnShow(_animalView.GameEntity);
            if(IsNormalDirection(pokeballModelViews[0]) == false) return;
            pokeballModelViews[0].SetActive(false);
            pokeballModelViews[1].SetActive(true);
            pokeballModelViews[1].Animator.Play("BoxOpen", 0,0);
        }

        private void OnSecondAnimationBegin()
        {
            // Debug.Log("Second Animation Begin");
            if(IsNormalDirection(pokeballModelViews[1])) return;
            pokeballModelViews[0].SetActive(true);
            pokeballModelViews[1].SetActive(false);
            pokeballModelViews[0].Animator.Play("MyBoxStand", 0, 1);
        }
        private void OnSecondAnimationEnd()
        {
            if(IsNormalDirection(pokeballModelViews[1]) == false) return;
            // Debug.Log("Second Animation End");
            if(GameEntity == null) return;
            GameEntity.isAnimationProcess = false;
            GameEntity.ReplaceIsPokeballOpen(true);
            _animalView.GameEntity.ReplaceIsVisible(true);
        }

        protected override void OnDestroy()
        {
            pokeballModelViews[0].onAnimationEnd -= OnFirstAnimationEnd;
            pokeballModelViews[0].onAnimationBegin -= OnFirstAnimationBegin;
            pokeballModelViews[1].onAnimationEnd -= OnSecondAnimationEnd;
            pokeballModelViews[1].onAnimationEnd -= OnSecondAnimationBegin;
            // if (_animalView != null) _animalView.transform.SetParent(null);
            base.OnDestroy();
        }

        private void Hide()
        {
            pokeballModelViews[0].SetActive(true);
            pokeballModelViews[1].SetActive(false);
            GameEntity.ReplaceIsPokeballOpen(false);
            GameEntity.ReplaceInteractable(true);
        }

        private readonly Vector3 _hideEndValue = Vector3.forward * -180f;
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Reverse = Animator.StringToHash("Reverse");

        public void OnHide(GameEntity entity)
        {
            _animalView.OnHide(_animalView.GameEntity);
            if (GameEntity.isPokeballOpen.value == false) return;
            GameEntity.isAnimationProcess = true;
            GameEntity.ReplaceInteractable(false);
            SetAnimationDirection(false);
            pokeballModelViews[1].Animator.Play("BoxOpen", 0, 1);
        }

        private void EndTweenLogic(bool isHideAnimation = true)
        {
            GameEntity.ReplaceIsPokeballOpen(!isHideAnimation);
            _animalView.GameEntity.ReplaceIsVisible(!isHideAnimation);
            GameEntity.ReplaceInteractable(isHideAnimation);
            if (isHideAnimation) GameEntity.isOpenByBooster = false;
            GameEntity.isAnimationProcess = false;
        }

        //todo сделать систему для работы с анимациями
        public void OnShow(GameEntity entity)
        {
            if (GameEntity.isPokeballOpen.value == true)
                return;
            GameEntity.isAnimationProcess = true;
            GameEntity.ReplaceInteractable(false);
            if (vfxClick != null) vfxClick.Play();
            SetAnimationDirection(true);
            pokeballModelViews[0].Animator.Play("MyBoxStand", 0,0);
        }

        public void OnSkipAnimation(GameEntity entity)
        {
        }

        public void OnMoveToTargetCell(GameEntity entity, int targetHashcode)
        {
            
        }

        public void OnMoveToTargetCell(GameEntity entity, int targetHashcode, Vector2Int breakBorderDirection)
        {
            var targetCell = Contexts.sharedInstance.game.GetEntityWithHashCode(targetHashcode);
            var destination = targetCell.transform.value.position;
            if (breakBorderDirection == Vector2Int.zero)
            {
                SimpleMovement(destination, targetCell);
                return;
            }

            var targetPosition = targetCell.transform.value.position;
            var gameCamera = Contexts.sharedInstance.game.GetGroup(GameMatcher.GameCamera).GetEntities().First();

            var gamePlaneCorners = Array.Empty<Vector3>();
            gamePlaneCorners = gameCamera.fieldCorners.value;

            var maxX = gamePlaneCorners.Max(c => c.x);
            var minX = gamePlaneCorners.Min(c => c.x);
            var maxZ = gamePlaneCorners.Max(c => c.z);
            var minZ = gamePlaneCorners.Min(c => c.z);
            const int constOffset = 5;
            var bottomOffset= (MathF.Abs(minZ - targetPosition.z) + constOffset) * Vector3.back;
            var bottomOffsetPoint = targetPosition + bottomOffset;
            var topOffset = (MathF.Abs(maxZ - targetPosition.z) + constOffset)* Vector3.forward;
            var topOffsetPoint = targetPosition + topOffset;
            var leftOffset = (MathF.Abs(minX - targetPosition.x) + constOffset)* Vector3.left;
            var leftOffsetPoint = targetPosition + leftOffset;
            var rightOffset = (MathF.Abs(maxX - targetPosition.x) + constOffset)* Vector3.right;
            var rightOffsetPoint = targetPosition + rightOffset;
#if UNITY_EDITOR
            Debug.DrawLine(topOffsetPoint, bottomOffsetPoint,Color.red, 5f);
            Debug.DrawLine(leftOffsetPoint, rightOffsetPoint,Color.red, 5f);
#endif

            var offscreenPoints = Array.Empty<Vector3>();
            if (breakBorderDirection.y != 0) offscreenPoints = new[] { bottomOffsetPoint, topOffsetPoint };
            if (breakBorderDirection.x != 0) offscreenPoints = new[] { rightOffsetPoint, leftOffsetPoint };
            HardMovement(destination, targetCell, breakBorderDirection, offscreenPoints);
        }
        
        private void SimpleMovement(Vector3 destination, GameEntity targetCell)
        {
            GameEntity.isAnimationProcess = true;
            GameEntity.isComplicatorProcess = true;
            GameEntity.ReplaceInteractable(false);
            transform.DOMove(destination, 0.5f)
                .SetEase(Ease.OutCubic)
                .SetLink(gameObject)
                .OnComplete(() =>
                {
                    GameEntity.isAnimationProcess = false;
                    GameEntity.isComplicatorProcess = false;
                    GameEntity.ReplaceInteractable(true);
                    targetCell.ReplaceLinkedPokeball(GameEntity.hashCode.value);

                });
        }
        private void HardMovement(Vector3 destination, GameEntity targetCell, Vector2Int directon, Vector3[] offscreenPoints)
        {
            GameEntity.isAnimationProcess = true;
            GameEntity.isComplicatorProcess = true;


            GameEntity.ReplaceInteractable(false);
            if(directon.y > 0 || directon.x <0) Array.Reverse(offscreenPoints);
            DOTween.Sequence().SetLink(this.gameObject)
                .Append(transform.DOMove(offscreenPoints[0], 0.5f))
                .AppendCallback(() => transform.position = offscreenPoints[1])
                .Append(transform.DOMove(destination,0.5f))
                .AppendCallback(() =>
                {
                    GameEntity.isAnimationProcess = false;
                    GameEntity.isComplicatorProcess = false;
                    GameEntity.ReplaceInteractable(true);
                    targetCell.ReplaceLinkedPokeball(GameEntity.hashCode.value);
                });
        }

        public void OnSwitchToTargetCell(GameEntity entity, int targetHashcode, bool isFirst)
        {
            GameEntity.isAnimationProcess = true;
            GameEntity.isComplicatorProcess = true;
            GameEntity.ReplaceInteractable(false);
            var targetCell = Contexts.sharedInstance.game.GetEntityWithHashCode(targetHashcode);
            var jumpPower = isFirst ? 4 : 8;
            transform.DOJump(targetCell.transform.value.position, jumpPower, 1, 0.8f)
                .SetEase(Ease.InOutSine)
                .SetLink(gameObject)
                .OnComplete(() =>
                {
                    GameEntity.isAnimationProcess = false;
                    GameEntity.isComplicatorProcess = false;
                    GameEntity.ReplaceInteractable(true);
                    targetCell.ReplaceLinkedPokeball(GameEntity.hashCode.value);
                });
        }
#if UNITY_EDITOR
        private string _animalName = null;
        private void OnDrawGizmos()
        {
            if (_animalView == null) return;
            _animalName ??= _animalView.GetTag();

            Handles.color = Color.red;
            Handles.Label(transform.position + Vector3.up * 3, _animalName, new GUIStyle(){fontSize = 20});
        }
#endif
        
        private const float Multiplication = 2f;
        private IEnumerable<Animator> _animators;
        public void OnAnimationRewind(GameEntity entity)
        {
            _animators ??= pokeballModelViews.Select(model => model.GetComponent<Animator>());
            foreach (var animator in _animators)
            {
                var speed = animator.GetFloat(Speed);
                Debug.Log($"Animation speed changed! Was = {speed}, become {speed * Multiplication}");
                animator.SetFloat(Speed, speed * Multiplication);
            }
        }

        public void OnAnimationRewindRemoved(GameEntity entity)
        {
            _animators ??= pokeballModelViews.Select(model => model.GetComponent<Animator>());
            foreach (var animator in _animators)
            {
                var speed = animator.GetFloat(Speed);
                Debug.Log($"Animation speed changed! Was = {speed}, become {speed * Multiplication}");
                animator.SetFloat(Speed, speed / Multiplication);
            }
        }
    }
}