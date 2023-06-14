using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.UI
{
    [DisallowMultipleComponent]
    public sealed class UIRootSchema : MonoBehaviour
    {
        [SerializeField] private UIScreen[] uiScreens;
        [SerializeField] private UIElement[] uiElements;

        public IEnumerable<UIScreen> UIScreens => uiScreens;
        public IEnumerable<UIElement> UIElements => uiElements;

        private void OnDestroy()
        {
            foreach (var uiScreen in UIScreens)
            {
                uiScreen.UIEntity?.Destroy();
            }

            foreach (var uiElement in UIElements)
            {
                uiElement.UIEntity?.Destroy();
            }
        }
    }
}