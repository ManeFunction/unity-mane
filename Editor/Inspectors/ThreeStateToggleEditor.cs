using UnityEditor;
using UnityEditor.UI;

namespace Mane.UI.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ThreeStateToggle), true)]
    public class ThreeStateToggleEditor : SelectableEditor
    {
        private SerializedProperty _onValueChangedProperty;
        private SerializedProperty _transitionProperty;
        private SerializedProperty _graphicProperty;
        private SerializedProperty _offGraphicProperty;
        private SerializedProperty _undefinedGraphicProperty;
        private SerializedProperty _stateProperty;

        protected override void OnEnable()
        {
            base.OnEnable();

            _transitionProperty = serializedObject.FindProperty("toggleTransition");
            _graphicProperty = serializedObject.FindProperty("graphic");
            _offGraphicProperty = serializedObject.FindProperty("offGraphic");
            _undefinedGraphicProperty = serializedObject.FindProperty("undefinedGraphic");
            _stateProperty = serializedObject.FindProperty("_state");
            _onValueChangedProperty = serializedObject.FindProperty("onStateValueChanged");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();

            serializedObject.Update();
            EditorGUILayout.PropertyField(_stateProperty);
            EditorGUILayout.PropertyField(_transitionProperty);
            
            EditorGUILayout.Space();
            
            EditorGUILayout.PropertyField(_graphicProperty);
            EditorGUILayout.PropertyField(_offGraphicProperty);
            EditorGUILayout.PropertyField(_undefinedGraphicProperty);

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(_onValueChangedProperty);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
