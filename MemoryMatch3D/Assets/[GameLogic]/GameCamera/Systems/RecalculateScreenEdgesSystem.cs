using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

namespace GameCamera.Systems
{
    public class RecalculateScreenEdgesSystem : ReactiveSystem<GameEntity>
    {
        public RecalculateScreenEdgesSystem(GameContext contextsGame) : base(contextsGame)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.AnyOf(
                GameMatcher.GameCameraOrthographicSize,
                GameMatcher.GameCameraFOV));
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var cameraEntity in entities)
            {
                var gameCamera = Contexts.sharedInstance.game.GetGroup(GameMatcher.GameCamera).GetEntities().First();
                var groundPlane = new Plane(Vector3.up, Vector3.zero);
                var gamePlaneCorners = new List<Vector3>();
                var edgePoints = Array.Empty<Vector3>();
                if (gameCamera.gameCameraMain.value.orthographic)
                {
                    var mainCamera = gameCamera.gameCameraMain.value;
                    var bottomLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
                    var topLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, mainCamera.pixelHeight, mainCamera.nearClipPlane));
                    var topRight = mainCamera.ScreenToWorldPoint(new Vector3(mainCamera.pixelWidth, mainCamera.pixelHeight, mainCamera.nearClipPlane));
                    var bottomRight = mainCamera.ScreenToWorldPoint(new Vector3(mainCamera.pixelWidth, 0, mainCamera.nearClipPlane));
                    edgePoints = new [] {bottomLeft,topLeft,topRight,bottomRight};
                    foreach (var edgePoint in edgePoints)
                    {
                        var ray = new Ray(edgePoint, mainCamera.transform.forward * mainCamera.farClipPlane);
                        var hasHit = groundPlane.Raycast(ray, out var enter);
                        gamePlaneCorners.Add(ray.GetPoint(enter));
                    }
#if UNITY_EDITOR
                    for (int i = 0; i < edgePoints.Length -1; i++)
                    {
                        Debug.DrawLine(gamePlaneCorners[i], gamePlaneCorners[i + 1], Color.green, 2f);
                    }
#endif
                }
                else
                {
                    //todo fov implementation
                }
                gameCamera.ReplaceFieldCorners(gamePlaneCorners.ToArray());
            }
        }
    }
}