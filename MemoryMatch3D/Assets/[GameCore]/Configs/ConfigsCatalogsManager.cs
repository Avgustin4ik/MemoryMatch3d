using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Configs
{
    public static class ConfigsCatalogsManager
    {
        private static ConfigsCatalog[] _catalogs;

        /// <summary>
        /// Call this function before any other call 
        /// </summary>
        public static ConfigsCatalog[] LoadCatalogs()
        {
            _catalogs = Resources.LoadAll<ConfigsCatalog>("");
            if (_catalogs == null) throw new Exception("No catalogs found at any Resource folder root");

            var catalogsType = new List<Type>();
            foreach (var catalog in _catalogs)
            {
                catalog.LoadConfigs();
                var catalogType = catalog.GetType();
                if (catalogsType.Contains(catalogType)) continue;
                catalogsType.Add(catalogType);
            }

            foreach (var catalogType in catalogsType)
            {
                ConfigsCatalog selectedCatalog = null;
                foreach (var catalog in _catalogs)
                {
                    if (catalog.GetType() != catalogType) continue;
                    if (selectedCatalog == null) selectedCatalog = catalog;
                    if (catalog.selected) selectedCatalog = catalog;
                }
                
                selectedCatalog.selected = true;
            }

            return _catalogs;
        }

        public static T GetCatalog<T>() where T : ConfigsCatalog
        {
            return _catalogs.Where(catalog => catalog.GetType() == typeof(T)).Cast<T>().FirstOrDefault();
        }

        public static T GetConfig<T>() where T : Config
        {
            foreach (var catalog in _catalogs)
            {
                if (catalog.selected == false) continue;
                var config = catalog.GetConfig<T>();
                if (config == null) continue;
                if (config is { } result) return result;
            }

            throw new Exception($"Requested config with type   {typeof(T)}   does not exists in any configs catalogs!");
        }

        public static void Reset()
        {
            _catalogs = null;
        }
    }
}