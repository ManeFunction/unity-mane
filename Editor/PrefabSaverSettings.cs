using System;
using UnityEngine;
using UnityEditor;

namespace Mane.Editor
{
    public class PrefabSaverSettings : EditorWindow
    {
        [MenuItem("Mane/Set Prefabs Saving Path", false, 1095)]
        public static void ShowWindow() => ShowPopup();

        public static PrefabSaverSettings ShowPopup(bool showButtons = false)
        {
            if (showButtons)
                GetWindow(typeof(PrefabSaverSettings)).Close();
            
            PrefabSaverSettings window = GetWindow(typeof(PrefabSaverSettings)) as PrefabSaverSettings;
            window!.titleContent = new GUIContent("Prefab Saver");
            window.minSize = new Vector2(300f, 100f);
            window.maxSize = new Vector2(500f, 150f);
            window._showButtons = showButtons;

            return window;
        }


        [SerializeField] private DefaultAsset _targetFolder;
        [SerializeField] private bool _askEveryTime;

        private static GUIStyle _labelStyle;
        private static GUIStyle _buttonStyle;

        private bool _showButtons;


        public event Action SaveButtonPressed;

        
        private void Awake()
        {
            GUID? folderGuid = PrefabsTools.GetPrefabsPathGuid();
            _targetFolder = folderGuid != null
                ? AssetDatabase.LoadAssetAtPath<DefaultAsset>(AssetDatabase.GUIDToAssetPath(folderGuid.Value))
                : null;
            _askEveryTime = PrefabsTools.GetSavingPopupOption();
        }

        private void OnGUI()
        {
            _labelStyle ??= new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleRight
            };

            _buttonStyle ??= new GUIStyle(GUI.skin.button)
            {
                fixedWidth = 80f,
                fixedHeight = 30f,
                fontSize = 16
            };
            
            if (_showButtons)
                EditorGUILayout.Space(25);
            
            EditorGUILayout.Space();
            DefaultAsset targetFolder = (DefaultAsset)EditorGUILayout.ObjectField(
                "Target Folder", _targetFolder, typeof(DefaultAsset), false);
            if (targetFolder != null && targetFolder != _targetFolder)
            {
                _targetFolder = targetFolder;
                EditorPrefs.SetString(PrefabsTools.PrefabsPathKey,
                    AssetDatabase.GUIDFromAssetPath(AssetDatabase.GetAssetPath(_targetFolder)).ToString());
            }
            
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Ask Every Time ", _labelStyle, GUILayout.Width(100));
            bool askEveryTime = EditorGUILayout.Toggle(_askEveryTime);
            EditorGUILayout.EndHorizontal();
            if (askEveryTime != _askEveryTime)
            {
                _askEveryTime = askEveryTime;
                EditorPrefs.SetBool(PrefabsTools.SavingPopupKey, _askEveryTime);
            }
            
            if (!_showButtons) return;
            
            EditorGUILayout.Space();
            GUI.enabled = _targetFolder;
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Save", _buttonStyle))
            {
                SaveButtonPressed?.Invoke();
                Close();
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            GUI.enabled = true;
        }
    }
}