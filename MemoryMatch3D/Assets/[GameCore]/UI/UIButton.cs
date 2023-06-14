using System;
using Entitas;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.UI
{
    [DisallowMultipleComponent]
    public class UIButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public enum ActionType : byte
        {
            PointerUp = 0,
            PointerDown = 1
        }

        [HideInInspector] public string componentName;
        public ActionType reactOn;

        private InputEntity _inputEntity;

        public int Index
        {
            get
            {
                if (componentName == null) return 0;
                for (var i = 0; i < InputComponentsLookup.componentTypes.Length; i++)
                {
                    if (InputComponentsLookup.componentNames[i] == componentName)
                        return i + 1;
                }

                return 0;
            }
            set => componentName = value > 0 ? InputComponentsLookup.componentNames[value - 1] : null;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (reactOn == ActionType.PointerDown) Reaction();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (reactOn == ActionType.PointerUp) Reaction();
        }

        private void Reaction()
        {
            if (string.IsNullOrEmpty(componentName))
            {
                Debug.LogError("Отсутсвует компонент для кнопки");
                return;
            }

            var savableDataComponent = Activator.CreateInstance(InputComponentsLookup.componentTypes[Index - 1]);
            var com = savableDataComponent as IComponent;
            _inputEntity = Contexts.sharedInstance.input.CreateEntity();
            _inputEntity.AddComponent(Index - 1, com);
        }
    }
}