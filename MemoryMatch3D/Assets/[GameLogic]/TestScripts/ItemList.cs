using System;
using System.Collections.Generic;

namespace _GameLogic_.TestScripts
{
    [Serializable]
    public class ItemList<T>
    {
        public string name;
        public List<T> Items = new List<T>();
    }
}