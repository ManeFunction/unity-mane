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

            _transitionProperty = serializedObject.FindProperty(ThreeStateToggle.TransitionPropertyName);
            _graphicProperty = serializedObject.FindProperty(ThreeStateToggle.GraphicPropertyName);
            _offGraphicProperty = serializedObject.FindProperty(ThreeStateToggle.OffGraphicPropertyName);
            _undefinedGraphicProperty = serializedObject.FindProperty(ThreeStateToggle.UndefinedGraphicPropertyName);
            _stateProperty = serializedObject.FindProperty(ThreeStateToggle.StatePropertyName);
            _onValueChangedProperty = serializedObject.FindProperty(ThreeStateToggle.OnStateValueChangedPropertyName);
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
