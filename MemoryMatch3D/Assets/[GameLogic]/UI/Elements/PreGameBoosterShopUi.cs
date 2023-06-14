using Core.UI;
using Entitas;
using UnityEngine;

namespace Ui.Elements
{
    public class PreGameBoosterShopUi : UIElement
    {
        [SerializeField] private PreGameBoosterPanel _panel;
        public override void Init(IEntity iEntity)
        {
            base.Init(iEntity);
            UIEntity.isPreGameBoosterShop = true;
            _panel.Init(Contexts.sharedInstance.ui.CreateEntity());
        }
    }
}