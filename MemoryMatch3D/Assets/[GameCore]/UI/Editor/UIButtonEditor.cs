using UnityEditor;
using UnityEngine;

namespace Core.UI
{
    [CustomEditor(typeof(UIButton))]
    public class UIButtonEditor : Editor
    {
        private UIButton _button;

        private string[] _componentNames;

        private const string NoComponentSelected = "none";

        private void OnEnable()
        {
            _button = (UIButton) target;

            _componentNames = new string[InputComponentsLookup.TotalComponents + 1];
            _componentNames[0] = NoComponentSelected;

            var isActual = false;
            for (var i = 0; i < InputComponentsLookup.TotalComponents; i++)
            {
                _componentNames[i + 1] = InputComponentsLookup.componentNames[i];
                if (_button.componentName == _componentNames[i + 1])
                    isActual = true;
            }

            if (isActual == false)
                _button.Index = 0;
        }

        public override void OnInspectorGUI()
        {
            if (_componentNames == null || _componentNames.Length == 0)
            {
                EditorGUILayout.HelpBox("InputComponentsLookup does not exist or not filled", MessageType.Warning);
                return;
            }

            if (_button.Index == 0)
            {
                EditorGUILayout.HelpBox($"Select component from the list below", MessageType.Info);
            }

            var index = _button.Index;
            index = EditorGUILayout.Popup("Action", index, _componentNames);
            if (index != _button.Index)
            {
                _button.Index = index;
                _button.componentName = _componentNames[index];
            }

            _button.reactOn = (UIButton.ActionType) EditorGUILayout.EnumPopup("React on", _button.reactOn);

            // using (new EditorGUILayout.VerticalScope("Box"))
            // {
            //     if (_button.componentName != null)
            //     {
            //         EditorGUILayout.LabelField($"Type = {_button.componentName}");
            //         EditorGUILayout.LabelField($"Index = {_button.Index}");
            //     }
            //     else
            //         EditorGUILayout.LabelField("Type = none");
            // }

            if (GUI.changed)
            {
                EditorUtility.SetDirty(_button);
            }
        }
    }
}