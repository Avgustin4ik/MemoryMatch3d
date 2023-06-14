using System.Numerics;
using Entitas;

namespace GameCamera
{
    [Game]
    public class GameCameraSettingsComponent : IComponent
    {
        public GameCameraSettingsComponent()
        {
            isOrthographic = true;
            defaultSize = 20;
            defaultFov = 60;
            zoomInOffset = 3f;
            zoomBorder = new Vector2(8, 15);
        }

        public GameCameraSettingsComponent(bool isOrthographic = true,
            float defaultSize = 20,
            float zoomInOffset = 3f,
            Vector2 zoomBorder = default,
            float defaultFov = 60f)
        {
            this.isOrthographic = isOrthographic;
            this.defaultSize = defaultSize;
            this.defaultFov = defaultFov;
            this.zoomInOffset = zoomInOffset;
            this.zoomBorder = zoomBorder == default ? new Vector2(8, 15) : zoomBorder;
        }

        public bool isOrthographic;
        public float defaultSize;
        public float defaultFov;
        public float zoomInOffset;
        public Vector2 zoomBorder;
    }
}