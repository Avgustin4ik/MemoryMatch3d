using UnityEngine;

namespace _GameLogic_.TestScripts
{
    [CreateAssetMenu(menuName = "Create ItemsConfigsCatalog", fileName = "ItemsConfigsCatalog", order = 0)]
    public class ItemsConfigsCatalog : ScriptableObject
    {
        public ScriptableObject[] GradeConfigs;
        
        public T GetConfig<T>() where T : ScriptableObject
        {
            foreach (var config in GradeConfigs)
            {
                if (config is T)
                {
                    return config as T;
                }
            }
            return null;
        }
        
        [ContextMenu("Test/Get Test Config")]
        public void TestBasicsConfigs()
        {
            var itemLists = GetConfig<GradeConfig>();
            Debug.Log($"item list {itemLists.GetType().Name} returned. Items count = {itemLists.Items.Count}");
        }
    }
}