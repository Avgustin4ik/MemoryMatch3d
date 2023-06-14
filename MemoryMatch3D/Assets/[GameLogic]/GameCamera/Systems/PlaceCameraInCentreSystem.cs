using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using Unity.Mathematics;
using UnityEngine;

namespace GameCamera.Systems
{
    public class PlaceCameraInCentreSystem : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<GameEntity> _pokeballGroup;
        private readonly IGroup<GameEntity> _cameraGroup;

        public PlaceCameraInCentreSystem(GameContext contextsGame) : base(contextsGame)
        {
            _cameraGroup = contextsGame.GetGroup(GameMatcher.GameCamera);
            _pokeballGroup = contextsGame.GetGroup(GameMatcher.Pokeball);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.LoadingLevel.Removed());
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var gameCamera in _cameraGroup)
            {
                var pokeballsEntities = _pokeballGroup.GetEntities();
                var zoomInOffset = gameCamera.gameCameraSettings.zoomInOffset;
                var pokeballMaxX = pokeballsEntities.Max(pb=> pb.transform.value.position.x) + zoomInOffset;
                var pokeballMinX = pokeballsEntities.Min(pb=> pb.transform.value.position.x) - zoomInOffset;
                var pokeballMaxZ = pokeballsEntities.Max(pb=> pb.transform.value.position.z) + zoomInOffset;
                var pokeballMinZ = pokeballsEntities.Min(pb=> pb.transform.value.position.z) - zoomInOffset; 
                var zNewPos = (pokeballMaxZ - pokeballMinZ) / 2f;
                var xNewPos = (Math.Abs(pokeballMaxX) - Math.Abs(pokeballMinX)) / 2f;
                var camPos = gameCamera.transform.value.position;
                gameCamera.transform.value.position = new Vector3(xNewPos, camPos.y, camPos.z);
            }
        }
    }
}