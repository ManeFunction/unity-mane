using UnityEditor;
using UnityEngine;
using System.IO;
using System.Linq;

namespace Mane.Editor
{
    public class AssetReferenceFinder : EditorWindow
    {
        private Object _targetAsset;
        private bool _searchInPackages;
        private Vector2 _scrollPos;

        [MenuItem("Mane/Asset Reference Finder", false, 1105)]
        public static void ShowWindow() => GetWindow(typeof(AssetReferenceFinder));

        private void OnGUI()
        {
            GUILayout.Label("Find Asset References", EditorStyles.boldLabel);

            // Serialized field input for the target asset
            _targetAsset = EditorGUILayout.ObjectField("Target Asset", _targetAsset, typeof(Object), false);
            _searchInPackages = EditorGUILayout.Toggle("Search in Packages", _searchInPackages);

            GUILayout.Space(5f);

            // Search Button
            if (GUILayout.Button("Find References"))
                FindAssetReferences();

            // Log area
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, GUILayout.Height(300));

            // Your logs will appear here in the future, you can make them clickable.
            EditorGUILayout.EndScrollView();
        }

        private void FindAssetReferences()
        {
            if (_targetAsset == null)
            {
                Debug.LogError("No asset selected.");

                return;
            }

            string assetPath = AssetDatabase.GetAssetPath(_targetAsset);
            string assetGUID = AssetDatabase.AssetPathToGUID(assetPath).Split('/')[0];
            string[] allAssets = AssetDatabase.GetAllAssetPaths()
                .Where(p => p.EndsWith(".unity") || p.EndsWith(".prefab")).ToArray();

            Debug.LogWarning($"Searching for references to: {assetGUID} among {allAssets.Length} assets.");

            for (int i = 0; i < allAssets.Length; i++)
            {
                string path = allAssets[i];

                if (!_searchInPackages && path.StartsWith("Packages/")) continue;

                // Update progress bar
                float progress = (float)i / allAssets.Length;
                if (EditorUtility.DisplayCancelableProgressBar("Finding References", $"Scanning {path}", progress))
                {
                    // User pressed cancel
                    break;
                }

                if (File.Exists(path))
                {
                    string fileContent = File.ReadAllText(path);

                    if (fileContent.Contains(assetGUID))
                    {
                        // You can change this line to log clickable entries
                        Debug.Log($"Reference found in: {path}", AssetDatabase.LoadAssetAtPath<Object>(path));
                    }
                }
            }

            // Clear the progress bar
            EditorUtility.ClearProgressBar();
        }
    }
}