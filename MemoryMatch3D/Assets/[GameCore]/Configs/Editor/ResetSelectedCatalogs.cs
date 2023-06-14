using UnityEditor;
using UnityEngine;

namespace Core.Configs
{
    public static class ResetSelectedCatalogs
    {
        [MenuItem("JB tools/Reset Selected Catalogs", false)]
        private static void Action(MenuCommand menuCommand)
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("<b><color=#FF6F91>Selected catalogs was reset</color></b>");
        }
    }
}