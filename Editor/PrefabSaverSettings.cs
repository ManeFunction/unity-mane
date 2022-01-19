using UnityEngine;
using UnityEditor;


namespace Mane.Editor
{
    public class PrefabSaverSettings : EditorWindow
    {
        [MenuItem("Mane/Set Prefabs Saving Path", false, 1095)]
        public static void ShowWindow() => GetWindow(typeof(PrefabSaverSettings))
            .titleContent = new GUIContent("Prefab Saver");

        [SerializeField] private string _path;

        private static GUIStyle _labelStyle;

        private void Awake() => _path = PrefabsTools.GetPrefabsPath();

        private void OnGUI()
        {
            if (_labelStyle == null)
            {
                _labelStyle = new GUIStyle(GUI.skin.label)
                {
                    alignment = TextAnchor.MiddleRight
                };
            }
            
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Saving path: ", _labelStyle, GUILayout.Width(80));
            string path = EditorGUILayout.TextField(_path);
            EditorGUILayout.EndHorizontal();

            if (path != _path)
            {
                _path = path;
                EditorPrefs.SetString(PrefabsTools.PrefsKey, _path);
            }
        }
    }
}