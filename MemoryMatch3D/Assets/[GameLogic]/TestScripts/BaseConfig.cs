using System.Collections.Generic;
using UnityEngine;

namespace _GameLogic_.TestScripts
{
    public abstract class BaseConfig<T> : ScriptableObject where T : Item
    {
        public List<ItemList<T>> Items = new List<ItemList<T>>();
    }
}