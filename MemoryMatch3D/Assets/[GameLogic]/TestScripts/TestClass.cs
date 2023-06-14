using UnityEngine;

namespace _GameLogic_.TestScripts
{
    public class TestClass : MonoBehaviour
    {
        public ItemsConfigsCatalog itemsConfigsCatalog;
        [ContextMenu("Test/Get Test Config")]
        public void Test()
        {
            var itemLists = itemsConfigsCatalog.GetConfig<GradeConfig>();
            Debug.Log($"item list {itemLists.GetType().Name} returned. Items count = {itemLists.Items.Count}");
        }
    }
}