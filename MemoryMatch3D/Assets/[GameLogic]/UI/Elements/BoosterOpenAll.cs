using Entitas;
using UnityEngine;

namespace Ui.Elements
{
    public class BoosterOpenAll : BoosterUiPreGame
    {
        [field: SerializeField] public float Duration { get; private set; }
        public override void Init(IEntity iEntity)
        {
            base.Init(iEntity);
            UIEntity.AddBoosterType(this.GetType());
            UIEntity.AddTimerDuration(Duration);
        }
    }
}