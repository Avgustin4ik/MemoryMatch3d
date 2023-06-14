using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Core.Configs
{
    public class ConfigsCatalogsManagerWindow : EditorWindow
    {
        private static ConfigsCatalogsManagerWindow _window;
        private static List<List<ConfigsCatalog>> _configsCatalogs;
        private static ConfigsCatalog _showConfigsCatalog;
        private static Vector2 _scrollPosition = Vector2.zero;
        private static bool _catalogsIsEmpty;

        [MenuItem("JB tools/ConfigsManager %m")]
        public static void Open()
        {
            if (_window == null)
            {
                _window = CreateWindow<ConfigsCatalogsManagerWindow>("Configs catalogs manager");
                var windowPosition = _window.position;
                var windowSize = new Vector2(ElementsTotalWidth, 512);
                _window.position = new Rect(windowPosition.position, windowSize);
                CreateStyles();

                GetCatalogsData();
                return;
            }

            _window.Focus();
        }

        private static void GetCatalogsData()
        {
            var catalogs = ConfigsCatalogsManager.LoadCatalogs();
            _catalogsIsEmpty = catalogs == null || catalogs.Length == 0;

            if (_catalogsIsEmpty) return;

            var types = new List<Type>();
            foreach (var catalog in catalogs)
            {
                if (types.Contains(catalog.GetType())) continue;

                types.Add(catalog.GetType());
            }

            _configsCatalogs = new List<List<ConfigsCatalog>>();
            for (var i = 0; i < types.Count; i++)
            {
                _configsCatalogs.Add(new List<ConfigsCatalog>());
                foreach (var catalog in catalogs)
                {
                    if (catalog.GetType() == types[i]) _configsCatalogs[i].Add(catalog);
                }
            }
        }

        private void OnFocus()
        {
            GetCatalogsData();
        }

        #region Styles

        private static int ElementsTotalWidth => FirstColumnSize + SecondColumnSize + ShowContentButtonSize +
                                                 HighlightButtonSize + SelectedButtonSize + CopyButtonSize +
                                                 DeleteButtonSize + 32;

        private const int ShowContentButtonSize = 92;
        private const int HighlightButtonSize = 64;
        private const int SelectedButtonSize = 64;
        private const int CopyButtonSize = 64;
        private const int DeleteButtonSize = 64;

        private const int FirstColumnSize = 192;
        private const int SecondColumnSize = 64;

        private static readonly Color ShowContentButtonColor = new Color(0.9f, 0.3f, 0.9f, 1.0f);
        private static readonly Color HideContentButtonColor = new Color(0.1f, 0.9f, 0.2f, 1.0f);
        private static readonly Color HighlightButtonColor = new Color(1.0f, 0.9f, 0.2f, 1.0f);
        private static readonly Color SelectedButtonColor = new Color(0.2f, 0.4f, 0.9f, 1.0f);
        private static readonly Color CopyButtonColor = new Color(0.2f, 0.8f, 1.0f, 1.0f);
        private static readonly Color DeleteButtonColor = new Color(1.0f, 0.3f, 0.2f, 1.0f);

        private static GUIStyle _middleCenterHeader;
        private static GUIStyle _middleLeftHeader;
        private static GUIStyle _middleCenterNormal;
        private static GUIStyle _middleLeftNormal;
        private static GUIStyle _noData;
        private static GUIStyle _helpLabels;

        private static void CreateStyles()
        {
            _middleCenterHeader = new GUIStyle
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 14,
                fontStyle = FontStyle.Bold,
                normal = {textColor = Color.white}
            };

            _middleLeftHeader = new GUIStyle
            {
                alignment = TextAnchor.MiddleLeft,
                fontSize = 12,
                fontStyle = FontStyle.Bold,
                normal = {textColor = new Color(0.9f, 0.8f, 0.7f, 1.0f)}
            };

            _middleCenterNormal = new GUIStyle
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 11,
                normal = {textColor = Color.white}
            };

            _middleLeftNormal = new GUIStyle
            {
                alignment = TextAnchor.MiddleLeft,
                fontSize = 11,
                normal = {textColor = Color.white}
            };

            _noData = new GUIStyle
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 12,
                fontStyle = FontStyle.Bold,
                normal = {textColor = Color.red}
            };

            _helpLabels = new GUIStyle
            {
                normal = {textColor = ShowContentButtonColor},
                fontStyle = FontStyle.Italic,
                fontSize = 11
            };
        }

        #endregion


        private void OnGUI()
        {
            if (_catalogsIsEmpty)
            {
                EditorGUILayout.LabelField("No one catalog was found!", _noData);
                return;
            }

            var backgroundColor = GUI.backgroundColor;
            EditorGUILayout.LabelField("C A T A L O G S", _middleCenterHeader);

            foreach (var configsCatalog in _configsCatalogs)
            {
                EditorGUILayout.Space(10);
                EditorGUILayout.LabelField($"Catalog: {configsCatalog[0].GetType()}", _middleLeftHeader);

                using (new GUILayout.HorizontalScope("box"))
                {
                    EditorGUILayout.LabelField("Catalog name", _middleCenterNormal, GUILayout.Width(FirstColumnSize));
                    EditorGUILayout.LabelField("Use", _middleCenterNormal, GUILayout.Width(SecondColumnSize));
                    EditorGUILayout.LabelField("", _middleCenterNormal);
                }

                var selectedConfigsCatalog = configsCatalog.FirstOrDefault(catalog => catalog.selected);
                if (selectedConfigsCatalog == null) configsCatalog.FirstOrDefault().selected = true;
                foreach (var catalog in configsCatalog)
                {
                    if (selectedConfigsCatalog != null && selectedConfigsCatalog != catalog) catalog.selected = false;
                    using (new GUILayout.HorizontalScope())
                    {
                        DrawCatalogData(catalog, ref selectedConfigsCatalog);
                        if (_showConfigsCatalog != catalog)
                        {
                            GUI.backgroundColor = ShowContentButtonColor;
                            if (GUILayout.Button("Show content", GUILayout.Width(ShowContentButtonSize)))
                                _showConfigsCatalog = catalog;
                        }
                        else
                        {
                            GUI.backgroundColor = HideContentButtonColor;
                            if (GUILayout.Button("Hide content", GUILayout.Width(ShowContentButtonSize)))
                                _showConfigsCatalog = null;
                        }

                        GUI.backgroundColor = HighlightButtonColor;
                        if (GUILayout.Button("Highlight", GUILayout.Width(HighlightButtonSize)))
                            EditorGUIUtility.PingObject(catalog);
                        GUI.backgroundColor = SelectedButtonColor;
                        if (GUILayout.Button("Select", GUILayout.Width(SelectedButtonSize)))
                            Selection.activeObject = catalog;
                        GUI.backgroundColor = CopyButtonColor;
                        if (GUILayout.Button("Copy", GUILayout.Width(CopyButtonSize))) CopyCatalog(catalog);
                        GUI.backgroundColor = DeleteButtonColor;
                        if (GUILayout.Button("Delete", GUILayout.Width(DeleteButtonSize))) DeleteCatalog(catalog);
                        GUI.backgroundColor = backgroundColor;
                    }
                }

                foreach (var catalog in configsCatalog.Where(catalog => catalog != selectedConfigsCatalog))
                    catalog.selected = false;
            }

            if (_showConfigsCatalog != null) DrawCatalogInspector(position);

            DrawHelp();
        }

        private static void DrawCatalogData(ConfigsCatalog catalog, ref ConfigsCatalog selectedConfigsCatalog)
        {
            EditorGUILayout.LabelField(catalog.name, _middleLeftNormal, GUILayout.Width(FirstColumnSize));
            catalog.selected = EditorGUILayout.Toggle(catalog.selected, GUILayout.Width(SecondColumnSize));
            if (catalog.selected) selectedConfigsCatalog = catalog;
        }

        private static void CopyCatalog(ConfigsCatalog catalog)
        {
            var assetPath = AssetDatabase.GetAssetPath(catalog);
            var assetPathParts = assetPath.Split('/');
            var assetName = assetPathParts[assetPathParts.Length - 1];
            var assetNameParts = assetName.Split('.');
            assetName = assetNameParts[0];
            var newAssetPath = "";
            for (var i = 0; i < assetPathParts.Length - 1; i++)
            {
                newAssetPath = i == 0 ? assetPathParts[i] : $"{newAssetPath}/{assetPathParts[i]}";
            }

            newAssetPath = $"{newAssetPath}/";

            for (var i = 0; i < 20; i++)
            {
                var savePath = $"{newAssetPath}{assetName}_{i}.asset";
                var existsAsset = AssetDatabase.LoadAssetAtPath<ConfigsCatalog>(savePath);
                if (existsAsset) continue;
                if (!AssetDatabase.CopyAsset(assetPath, savePath)) continue;

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                EditorApplication.RepaintProjectWindow();
                GetCatalogsData();
                Debug.Log($"<color=lime>Copying successful at path [{savePath}]</color>");
                return;
            }
        }

        private static void DeleteCatalog(ConfigsCatalog catalog)
        {
            if (EditorUtility.DisplayDialog("Warning!",
                $"Config {catalog.name} will be DESTROY FOREVER?", "Ok", "No way"))
            {
                if (_showConfigsCatalog == catalog) _showConfigsCatalog = null;
                AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(catalog));
                AssetDatabase.Refresh();
                EditorApplication.RepaintProjectWindow();
                GetCatalogsData();
            }
        }

        private static void DrawCatalogInspector(Rect position)
        {
            EditorGUILayout.Space(20);
            using var scrollView = new EditorGUILayout.ScrollViewScope(_scrollPosition, false, false,
                GUILayout.Width(position.width), GUILayout.Height(128));
            _scrollPosition = scrollView.scrollPosition;
            var assetPath = AssetDatabase.GetAssetPath(_showConfigsCatalog);
            EditorGUILayout.LabelField($"{assetPath}", _middleLeftHeader);
            Editor.CreateEditor(_showConfigsCatalog).OnInspectorGUI();
        }

        private static void DrawHelp()
        {
            EditorGUILayout.Space(20);

            using (new EditorGUILayout.VerticalScope("box"))
            {
                EditorGUILayout.LabelField("Help", _middleLeftHeader);
                _helpLabels.normal.textColor = ShowContentButtonColor;
                EditorGUILayout.LabelField("Show / hide to view content of catalog", _helpLabels);
                _helpLabels.normal.textColor = HighlightButtonColor;
                EditorGUILayout.LabelField("Highlight to view catalog in project tab", _helpLabels);
                _helpLabels.normal.textColor = SelectedButtonColor;
                EditorGUILayout.LabelField("Select to activate asset in project tab", _helpLabels);
                _helpLabels.normal.textColor = CopyButtonColor;
                EditorGUILayout.LabelField("Copy to make copy of this catalog", _helpLabels);
                _helpLabels.normal.textColor = DeleteButtonColor;
                EditorGUILayout.LabelField("Delete to remove catalog from project", _helpLabels);

                EditorGUILayout.Space(20);
#if UNITY_EDITOR_WIN
                EditorGUILayout.LabelField("press CTRL + M to open this window", _middleCenterNormal);
#elif UNITY_EDITOR_OSX
                EditorGUILayout.LabelField("press CMD + M to open this window", _middleCenterNormal);
#endif
            }
        }

        private void OnDisable()
        {
            _showConfigsCatalog = null;
            _configsCatalogs = null;
            ConfigsCatalogsManager.Reset();
        }
    }
}