using UnityEditor;

namespace Mane.Editor
{
    [CustomEditor(typeof(ColorSchemeSyncComponent), true)]
    public class ColorSchemeSyncComponentEditor : UnityEditor.Editor
    {
        private SerializedProperty _childrenProperty;

        protected virtual void OnEnable() => _childrenProperty = serializedObject.FindProperty("_children");

        public override void OnInspectorGUI()
        {
            serializedObject.UpdateIfRequiredOrScript();

            EditorGUILayout.PropertyField(_childrenProperty, true);

            serializedObject.ApplyModifiedProperties();
        }
    }
}