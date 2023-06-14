using Core.Extension;
using Entitas;
using UnityEngine.EventSystems;

namespace DebugMenu
{
    public class DebugBackgroundImage : MonoBehAdvUi, IPointerDownHandler, IPointerUpHandler
    {
        public override void Init(IEntity iEntity)
        {
            base.Init(iEntity);
            UIEntity.isDebugScreenBackground = true;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            CloseDebugScreen();
        }

        private static void CloseDebugScreen()
        {
            Contexts.sharedInstance.input.CreateEntity().isButtonCloseDebugMenu = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            CloseDebugScreen();
        }
    }
}