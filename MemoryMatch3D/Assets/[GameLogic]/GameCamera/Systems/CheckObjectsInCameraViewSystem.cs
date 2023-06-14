using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

namespace GameCamera.Systems
{
    public class CheckObjectsInCameraViewSystem : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<GameEntity> _pokeballsGroup;
        private readonly IGroup<GameEntity> _cameraGroup;

        public CheckObjectsInCameraViewSystem(GameContext contextsGame) : base(contextsGame)
        {
            _pokeballsGroup = contextsGame.GetGroup(GameMatcher.Pokeball);
            _cameraGroup = contextsGame.GetGroup(GameMatcher.GameCamera);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.CheckObjectsInCameraEvent.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return _pokeballsGroup.count > 0;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            if(_cameraGroup.count == 0) throw new System.Exception("No camera in scene");
            foreach (var gameCamera in _cameraGroup)
            {
                var offset = gameCamera.gameCameraSettings.zoomInOffset;
                var camera = gameCamera.gameCameraMain.value;
                var pokeballsEntities = _pokeballsGroup.GetEntities();
                var pokeballMaxX = pokeballsEntities.Max(pb=> pb.transform.value.position.x) + offset;
                var pokeballMinX = pokeballsEntities.Min(pb=> pb.transform.value.position.x) - offset;
                const double tolerance = 0.1f;

                var cameraPositionX = gameCamera.transform.value.position.x;
                var newSize = Math.Max(Math.Abs(pokeballMaxX - cameraPositionX), MathF.Abs(pokeballMinX - cameraPositionX));
                var zoomBorder = gameCamera.gameCameraSettings.zoomBorder;
                newSize = Math.Clamp(newSize, zoomBorder.X, zoomBorder.Y);
                var sizeByWidth = camera.orthographicSize * camera.aspect;
                
                if(Math.Abs(newSize - sizeByWidth) <= tolerance) return;
                gameCamera.ReplaceGameCameraOrthographicSize(newSize / camera.aspect, true);
            }
        }
    }
}