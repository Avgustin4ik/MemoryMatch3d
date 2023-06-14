using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Core.Configs
{
    public abstract class ConfigsCatalog : ScriptableObject
    {
        private Dictionary<Type, Config> _configs;
        [HideInInspector] public bool selected;

        public void LoadConfigs()
        {
            _configs = new Dictionary<Type, Config>();

            var fieldInfos = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => x.FieldType.BaseType == typeof(Config));

            if (fieldInfos == null) throw new Exception($"Catalog {name} does not have any configs!");

            foreach (var fieldInfo in fieldInfos)
            {
                if (fieldInfo.GetValue(this) is Config config) _configs.Add(config.GetType(), config);
            }
        }

        public T GetConfig<T>() where T : Config
        {
            var keyValuePairs = _configs.Where(x => x.Key == typeof(T));
            var firstOrDefault = keyValuePairs.FirstOrDefault();
            var config = (T) firstOrDefault.Value;

            return config;
        }
    }
}