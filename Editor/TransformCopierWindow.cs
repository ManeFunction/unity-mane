using UnityEngine;
using UnityEditor;

namespace Mane.Editor
{
    public class TransformCopierWindow : EditorWindow
    {
        [SerializeField] private GameObject[] _sourceObjects;
        [SerializeField] private GameObject[] _targetObjects;

        private SerializedObject _serializedObject;
        private SerializedProperty _sourceObjectsProperty;
        private SerializedProperty _targetObjectsProperty;

        [MenuItem("Mane/Transform Copier")]
        public static void ShowWindow()
        {
            TransformCopierWindow window = (TransformCopierWindow)GetWindow(typeof(TransformCopierWindow));
            GUIContent titleContent = new GUIContent("Transform Copier");
            window.titleContent = titleContent;
        }

        private void OnEnable()
        {
            _serializedObject = new SerializedObject(this);
            _sourceObjectsProperty = _serializedObject.FindProperty("_sourceObjects");
            _targetObjectsProperty = _serializedObject.FindProperty("_targetObjects");
        }

        private void OnGUI()
        {
            _serializedObject.Update();

            GUILayout.Label("Transform Copier", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox("Drag the prefabs you want to update into the box below.", MessageType.Info);
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(_sourceObjectsProperty, true);
            EditorGUILayout.PropertyField(_targetObjectsProperty, true);

            if (GUILayout.Button("Copy"))
            {
                UpdatePrefabs();
            }

            _serializedObject.ApplyModifiedProperties();
        }

        private void UpdatePrefabs()
        {
            if (_sourceObjects.Length != _targetObjects.Length)
            {
                Debug.LogError("Source and target objects count must be equal.");
                return;
            }

            for (int i = 0; i < _sourceObjects.Length; i++)
            {
                EditorUtility.DisplayProgressBar("Transform copier", $"Copying transforms for {_sourceObjects[i].name}",
                    (float)i / _sourceObjects.Length);
                
                GameObject source = _sourceObjects[i];
                GameObject target = _targetObjects[i];
                target.transform.localPosition = source.transform.localPosition;
                target.transform.localRotation = source.transform.localRotation;
                target.transform.localScale = source.transform.localScale;

                EditorUtility.SetDirty(target);
            }
            
            EditorUtility.ClearProgressBar();
        }
    }
}