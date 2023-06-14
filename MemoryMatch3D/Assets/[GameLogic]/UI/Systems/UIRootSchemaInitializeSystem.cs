using System;
using Core.UI;
using Entitas;

namespace Ui
{
    public class UIRootSchemaInitializeSystem : IInitializeSystem
    {
        private readonly UiContext _uiContext;

        public UIRootSchemaInitializeSystem(UiContext uiContext)
        {
            _uiContext = uiContext;
        }

        public void Initialize()
        {
            var uiRootSchema = UnityEngine.Object.FindObjectOfType<UIRootSchema>();
            _uiContext.ReplaceUiRootSchema(uiRootSchema);
            // if (uiRootSchema != null) _uiContext.SetUiRootSchema(uiRootSchema);
            
            if (_uiContext.hasUiRootSchema == false) throw new NotImplementedException("UI Root not found");

            var uiSchema = _uiContext.uiRootSchema.value;

            InitScreens(ref uiSchema);
            InitElements(ref uiSchema);
        }

        private void InitScreens(ref UIRootSchema uiRootSchema)
        {
            foreach (var screen in uiRootSchema.UIScreens)
            {
                var screenEntity = _uiContext.CreateEntity();
                screen.Init(screenEntity);
            }
        }

        private void InitElements(ref UIRootSchema uiRootSchema)
        {
            foreach (var element in uiRootSchema.UIElements)
            {
                var elementElement = _uiContext.CreateEntity();
                element.Init(elementElement);
            }
        }
    }
}