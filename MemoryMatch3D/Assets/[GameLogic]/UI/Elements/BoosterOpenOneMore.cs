using Entitas;

namespace Ui.Elements
{
    public class BoosterOpenOneMore : BoosterUiInGame
    {
        public override void Init(IEntity iEntity)
        {
            base.Init(iEntity);
            UIEntity.isInGameBooster = true;
            UIEntity.AddBoosterType(this.GetType());
        }
    }
}