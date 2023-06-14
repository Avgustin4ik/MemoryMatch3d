using System.Collections.Generic;
using Core.Common;
using Core.UI;
using Entitas;
using Ui.Screens.Meta.Components;

namespace Ui.Screens.Meta
{
    public class EmptyBiomeZoneScreen : UIScreen, IUiHideListener, IUiShowListener
    {
        public override void Init(IEntity iEntity)
        {
            base.Init(iEntity);
            iEntity.AddComponent(UiComponentsLookup.EmptyBiomeZoneScreen, new EmptyBiomeZoneScreenComponent());
            iEntity.AddComponent(UiComponentsLookup.Hide, new HideComponent());
            iEntity.AddComponent(UiComponentsLookup.Show, new ShowComponent());
            iEntity.AddComponent(UiComponentsLookup.UiHideListener, new UiHideListenerComponent(){value = new List<IUiHideListener>(){this}});
            iEntity.AddComponent(UiComponentsLookup.UiShowListener, new UiShowListenerComponent(){value = new List<IUiShowListener>(){this}});
        }

        public void OnHide(UiEntity entity)
        {
            gameObject.SetActive(false);
        }

        public void OnShow(UiEntity entity)
        {
            Open();
        }
    }
}