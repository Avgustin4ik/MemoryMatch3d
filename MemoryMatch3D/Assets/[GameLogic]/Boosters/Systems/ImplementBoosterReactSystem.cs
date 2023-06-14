using System.Collections.Generic;
using System.Linq;
using Entitas;
using Grid;
using UnityEngine;

namespace Boosters
{
    public class ImplementBoosterReactSystem : ReactiveSystem<InputEntity>
    {
        private readonly IGroup<UiEntity> _boosterGroup;
        private readonly UiContext _uiContext;
        private readonly GameContext _gameContext;
        private readonly Camera _camera;
        private readonly IGroup<GameEntity> _cellGroup;

        public ImplementBoosterReactSystem(InputContext contextsInput, GameContext contextsGame, UiContext contextsUI) :
            base(contextsInput)
        {
            _gameContext = contextsGame;
            _uiContext = contextsUI;
            _boosterGroup = contextsUI.GetGroup(UiMatcher.AllOf(
                UiMatcher.Booster,
                UiMatcher.BoosterSelected));
            _camera = Camera.main;
            _cellGroup = contextsGame.GetGroup(GameMatcher.Cell);

        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.TouchUpPosition);
        }

        protected override bool Filter(InputEntity entity)
        {
            return true;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            if (!_gameContext.stateManagerEntity.stateBoosterAiming) return;
            foreach (var inputEntity in entities)
            {
                var far = _camera.farClipPlane;
                var touchUpPosition = inputEntity.touchUpPosition.value;
                var ray = _camera.ScreenPointToRay(touchUpPosition);
#if UNITY_EDITOR
                Debug.DrawRay(ray.origin, ray.direction * far, Color.red, 1f);
#endif
                LayerMask mask = LayerMask.GetMask("Cell");
                if (Physics.Raycast(ray, out var hit, far, mask) == false) continue;
                var cellEntity = _cellGroup.GetEntities().FirstOrDefault(e=>e.hashCode.value == hit.collider.gameObject.GetHashCode());
                if (!cellEntity.inFocus.value) continue;
                foreach (var boosterEntity in _boosterGroup.GetEntities())
                {
                    if (boosterEntity.boosterSelected.value == false)
                    {
                        boosterEntity.ReplaceInteractable(true);
                        continue;
                    }
                    var requestEntity = _uiContext.CreateEntity();
                    requestEntity.isBoosterImplementationRequest = true;
                    requestEntity.AddBoosterID(boosterEntity.boosterID.value);
                    requestEntity.AddBoosterType(boosterEntity.boosterType.value);
                    if(_gameContext.GetGroup(GameMatcher.ThisPlayersTurn).GetEntities()[0].isBoostersEndless) continue;
                    boosterEntity.Destroy();
                } 
            }
        }
    }
}