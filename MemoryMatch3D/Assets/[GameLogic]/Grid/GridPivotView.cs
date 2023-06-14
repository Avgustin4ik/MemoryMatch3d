using Core.Extension;
using Entitas;

namespace Grid
{
    public class GridPivotView : MonoBehAdvGameLevelCleanup
    {
        public override void Init(IEntity iEntity)
        {
            base.Init(iEntity);
            GameEntity.isGrid = true;
        }
    }
}