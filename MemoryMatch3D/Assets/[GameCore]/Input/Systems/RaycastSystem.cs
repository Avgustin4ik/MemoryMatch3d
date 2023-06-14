using System.Collections.Generic;
using Core.Extension;
using Entitas;
using Entitas.Unity;
using Grid;
using Pokeball;
using UnityEngine;

namespace Core.Input
{
    public class RaycastSystem : ReactiveSystem<InputEntity>
    {
        private readonly InputContext _inputContext;
        private readonly GameContext _gameContext;
        private readonly Camera _camera;

        public RaycastSystem(InputContext contextsInput, GameContext contextsGame) : base(contextsInput)
        {
            _camera = Camera.main;
            _inputContext = contextsInput;
            _gameContext = contextsGame;
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.TouchPhase);
        }

        protected override bool Filter(InputEntity entity)
        {
            return entity.touchPhase.value == TouchPhase.Began;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            foreach (var inputEntity in entities)
            {
                if (_gameContext.stateManagerEntity == null) return;
                if(_gameContext.stateManagerEntity.stateLoadingLevel) return;
                if(_gameContext.stateManagerEntity.stateDebugMode) return;
                if(_gameContext.stateManagerEntity.stateBoosterAiming) return;
                if(_gameContext.stateManagerEntity.stateCutscene) return;
                //todo считать площади на стратре far and near 
                var near = _camera.nearClipPlane;
                var far = _camera.farClipPlane;
                var touchPosition = inputEntity.touchDownPosition.value;
                var ray = _camera.ScreenPointToRay(touchPosition);
#if UNITY_EDITOR
                Debug.DrawRay(ray.origin, ray.direction*far, Color.cyan,5f);
#endif
                var raycastHits = Physics.RaycastAll(ray, maxDistance: far, Physics.DefaultRaycastLayers);
                foreach (var raycastHit in raycastHits)
                {
                    if (!raycastHit.collider.gameObject.TryGetComponent<CellView>(out var objectView)) continue;
                    objectView.GameEntity.triggerClicked = true;
                }
            }
        }
    }
}