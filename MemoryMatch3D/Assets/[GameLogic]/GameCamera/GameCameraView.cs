using Core.Extension;
using DG.Tweening;
using Entitas;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameCamera
{
    [RequireComponent(typeof(Camera))]
    public class GameCameraView : MonoBehAdvGame, IGameCameraOrthographicSizeListener
    {
        private Camera _camera;
        [SerializeField] private float defaultFOV;
        [SerializeField] private float defaultOrthographicSize;
        [SerializeField] [Range(0,0.5f)] private float dxOffset = 0.2f;
        [FoldoutGroup("Zoom settings")]
        [SerializeField] private float zoomEdgeOffset = 3f;
        [FoldoutGroup("Zoom settings")]
        [SerializeField] private float zoomInBorder = 8;
        [FoldoutGroup("Zoom settings")]
        [SerializeField] private float zoomOutBorder = 15;
        [FoldoutGroup("Zoom settings")]
        [SerializeField] private float zoomDuration = 1f;
        [FoldoutGroup("Zoom settings")]
        [SerializeField] private Ease zoomEase = Ease.InOutCirc;

        public override void Init(IEntity iEntity)
        {
            _camera = gameObject.GetComponent<Camera>();
            
            base.Init(iEntity);
            GameEntity.AddComponent( GameComponentsLookup.GameCameraSettings,new GameCameraSettingsComponent(
                defaultSize: defaultOrthographicSize,
                zoomInOffset: 3f,
                zoomBorder: new System.Numerics.Vector2(zoomInBorder,zoomOutBorder)));
            GameEntity.isGameCamera = true;
            GameEntity.AddGameCameraMain(_camera);
            GameEntity.AddGameCameraOrthographicSize(_camera.orthographicSize, false);
            GameEntity.AddGameCameraOrthographicSizeListener(this);
            GameEntity.AddDefaultOrthographicSize(defaultOrthographicSize);
            GameEntity.AddGameCameraDetectionOffset(dxOffset);
        }

        public void OnGameCameraOrthographicSize(GameEntity entity, float value, bool animate = true)
        {
            if (!animate) 
            {
                _camera.orthographicSize = value;
                return;
            }
            Tween tween = _camera.DOOrthoSize(value, zoomDuration).SetLink(this.gameObject).SetEase(zoomEase);
        }

        private void OnDrawGizmosSelected()
        {
            if(GameEntity == null) return;
            var fieldCorners = GameEntity.fieldCorners.value;
            for (int i = 0; i < fieldCorners.Length; i++)
            {
                var target = i < fieldCorners.Length - 1 ? fieldCorners[i + 1] : fieldCorners[0];
                Debug.DrawLine(fieldCorners[i], target, Color.green, 2f);
            }
        }
    }
}