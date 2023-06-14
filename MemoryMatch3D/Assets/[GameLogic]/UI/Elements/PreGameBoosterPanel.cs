using System.Collections.Generic;
using Core.UI;
using Entitas;

namespace Ui.Elements
{
    public class PreGameBoosterPanel : UIElement
    {
        private BoosterUiPreGame[] _boosters;
        public override void Init(IEntity iEntity)
        {
            base.Init(iEntity);
            UIEntity.isPreGameBoosterPanel = true;
            _boosters = GetComponentsInChildren<BoosterUiPreGame>(true);
            if(_boosters.Length == 0) return;
            foreach (var boosterUiPreGame in _boosters)
            {
                boosterUiPreGame.Init(Contexts.sharedInstance.ui.CreateEntity());
            }
        }
    }
}