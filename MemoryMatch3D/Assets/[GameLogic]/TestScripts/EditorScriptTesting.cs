using System.Collections.Generic;
using System.Linq;
using Animals;
using Entitas;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _GameLogic_.TestScripts
{
    [ExecuteInEditMode]
    public class EditorScriptTesting : MonoBehaviour
    {
        [InfoBox("Default Enum")]
        public AnimalsType AnimalsType;
        [InfoBox("New sexy Enum")]
        [ValueDropdown("GetFilteredList")]
        [SerializeField] private int AnimalComponentIndex;

        private static ValueDropdownList<int> GetFilteredList()
        {
            var array = new ValueDropdownList<int>();
            for (var i = 0; i < GameComponentsLookup.componentTypes.Length; i++)
            { 
                var type = GameComponentsLookup.componentTypes[i].GetInterface(nameof(IAnimalComponent));
                if (type == null) continue;
                var item = new ValueDropdownItem<int>(GameComponentsLookup.componentNames[i], i);
                array.Add(item);
            }
            return array;
        }
    }
}