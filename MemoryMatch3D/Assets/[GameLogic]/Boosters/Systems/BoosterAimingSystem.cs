using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using Grid;
using UnityEngine;

namespace Boosters
{
    public class BoosterAimingSystem : ReactiveSystem<InputEntity>
    {
        private readonly GameContext _gameContext;
        private readonly Camera _camera;
        private readonly IGroup<GameEntity> _cellGroup;
        private readonly IGroup<UiEntity> _boosterGroup;

        public BoosterAimingSystem(InputContext contextInput, GameContext contextGame, UiContext uiContext) : base(contextInput)
        {
            _gameContext = contextGame;
            _camera = Camera.main;
            _cellGroup = contextGame.GetGroup(GameMatcher.Cell);
            _boosterGroup = uiContext.GetGroup(UiMatcher.AllOf(
                UiMatcher.BoosterSelected,
                UiMatcher.Booster,
                UiMatcher.BoosterAimType));
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.TouchMovePosition);
        }

        protected override bool Filter(InputEntity entity)
        {
            return true;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            foreach (var inputEntity in entities)
            {
                if(_gameContext.stateManagerEntity.stateBoosterAiming == false) continue;
                var near = _camera.nearClipPlane;
                var far = _camera.farClipPlane;
                var touchMovePosition = inputEntity.touchMovePosition.value;
                var ray = _camera.ScreenPointToRay(touchMovePosition);
#if UNITY_EDITOR
                Debug.DrawRay(ray.origin, ray.direction*far, Color.magenta,1f);
#endif
                var raycastHits = Physics.RaycastAll(ray, maxDistance: far);
                CellView objectView = null;

                foreach (var raycastHit in raycastHits)
                {
                    if (raycastHits.Select(r => r.collider.gameObject.TryGetComponent<CellView>(out objectView))
                        .All(x => x ==false))
                    {
                        foreach (var cellEntity in _cellGroup.GetEntities())
                        {
                            cellEntity.ReplaceInFocus(false);
                        }
                        continue;
                    }
                    if(objectView.GameEntity.hasLinkedPokeball == false) continue;
                    foreach (var boosterEntity in _boosterGroup.GetEntities().Where(e=>e.boosterSelected.value))
                    {
                        HighlightCells(boosterEntity.boosterAimType.value, objectView, false);
                    }
                }
            }
        }

        private void HighlightCells(AimTarget AimType, CellView objectView, bool includeEmpty = false)
        {
            var focusCells = includeEmpty ? _cellGroup.GetEntities() : _cellGroup.GetEntities().Where(cell=>cell.hasLinkedPokeball).ToArray();
            foreach (var cell in _cellGroup.GetEntities())
            {
                cell.ReplaceInFocus(false);
            }
            switch (AimType)
            {
                case AimTarget.All:
                    break;
                case AimTarget.One:
                    focusCells = new GameEntity[] { objectView.GameEntity };
                    break;
                case AimTarget.Column:
                    focusCells = _cellGroup.GetEntities().Where(x=> x.index.value.y == objectView.GameEntity.index.value.y).ToArray();
                    break;
                case AimTarget.Raw:
                    focusCells = _cellGroup.GetEntities().Where(x=> x.index.value.x == objectView.GameEntity.index.value.x).ToArray();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(AimType), AimType, null);
            }

            foreach (var cell in focusCells)
            {
                cell.ReplaceInFocus(true);
            }
        }
    }
}