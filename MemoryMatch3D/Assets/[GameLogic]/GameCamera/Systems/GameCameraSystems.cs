using System;
using UnityEngine;
using UnityEngine.Timeline;

namespace GameCamera.Systems
{
    public class GameCameraSystems : Feature
    {
        public GameCameraSystems(Contexts contexts)
        {
            Add(new ReturnCameraDefaultFOV(contexts.game));
            Add(new InitializeGameCameraSystem(contexts.game));
            Add(new PlaceCameraInCentreSystem(contexts.game));
            Add(new RecalculateScreenEdgesSystem(contexts.game));
            Add(new CheckObjectsInCameraViewSystem(contexts.game));
            Add(new TriggerCameraZoomCheckSystem(contexts.game));

            var findFirstObjectByType = UnityEngine.GameObject.FindObjectsByType<AudioListener>(FindObjectsSortMode.None);
            Debug.Log($"It is {findFirstObjectByType.Length} audio listeners");

#if UNITY_EDITOR
            // Add(new TestExecuteSystem(contexts.game));
#endif
        }
    }
}