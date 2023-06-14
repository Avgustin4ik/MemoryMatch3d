using System.Collections.Generic;
using Animals;
using Core.Configs;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

namespace Configs
{
    [CreateAssetMenu(fileName = "SceneStorage", menuName = "Configs/New Scene Storage", order = 0)]
    public class SceneStorage : Config
    {
        [field: SerializeField] public List<SceneElement> Scenes { get; private set; }
        [ContextMenu("Tools/Update List")]
        public void GenerateList()
        {
            foreach (var type in AnimalsType.GetValues(typeof(AnimalsType)))
            {
               Scenes.Add(new SceneElement {SceneName = type.ToString().ToUpper(), AnimalType = (AnimalsType) type});
            }
            EditorUtility.SetDirty(this);
        }
    }
}
#endif
