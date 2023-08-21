using System;
using UnityEditor;
using UnityEngine;

namespace Mane.Editor
{
    [CustomEditor(typeof(MaxTextSize), true)]
    public class MaxTextSizeEditor : UnityEditor.Editor
    {
        private SerializedProperty _textProperty;
        private SerializedProperty _layoutElementProperty;
        private SerializedProperty _contentSizeFitterProperty;
        
        private SerializedProperty _maxWidthProperty;
        private SerializedProperty _maxHeightProperty;
        
        
        private void OnEnable()
        {
            _textProperty = serializedObject.FindProperty("_text");
            _layoutElementProperty = serializedObject.FindProperty("_layoutElement");
            _contentSizeFitterProperty = serializedObject.FindProperty("_contentSizeFitter");
            
            _maxWidthProperty = serializedObject.FindProperty("_maxWidth");
            _maxHeightProperty = serializedObject.FindProperty("_maxHeight");
            
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            EditorGUILayout.PropertyField(_textProperty);
            EditorGUILayout.PropertyField(_layoutElementProperty);
            EditorGUILayout.PropertyField(_contentSizeFitterProperty);
            
            GUILayout.Space(10f);
            
            LayoutElementField(_maxWidthProperty, 0);
            LayoutElementField(_maxHeightProperty, 0);

            serializedObject.ApplyModifiedProperties();
        }


        // copy-paste from UnityEditor.UI.LayoutElementEditor
        private void LayoutElementField(SerializedProperty property, float defaultValue) => 
            LayoutElementField(property, _ => defaultValue);

        private void LayoutElementField(SerializedProperty property, Func<RectTransform, float> defaultValue)
        {
            Rect position = EditorGUILayout.GetControlRect();

            // Label
            GUIContent label = EditorGUI.BeginProperty(position, null, property);

            // Rects
            Rect fieldPosition = EditorGUI.PrefixLabel(position, label);

            Rect toggleRect = fieldPosition;
            toggleRect.width = 16;

            Rect floatFieldRect = fieldPosition;
            floatFieldRect.xMin += 16;

            // Checkbox
            EditorGUI.BeginChangeCheck();
            bool enabled = EditorGUI.ToggleLeft(toggleRect, GUIContent.none, property.floatValue >= 0);
            if (EditorGUI.EndChangeCheck())
            {
                // This could be made better to set all of the targets to their initial width, but minimizing code change for now
                property.floatValue = (enabled ? defaultValue((target as MaxTextSize).transform as RectTransform) : -1);
            }

            if (!property.hasMultipleDifferentValues && property.floatValue >= 0)
            {
                // Float field
                EditorGUIUtility.labelWidth = 4; // Small invisible label area for drag zone functionality
                EditorGUI.BeginChangeCheck();
                float newValue = EditorGUI.FloatField(floatFieldRect, new GUIContent(" "), property.floatValue);
                if (EditorGUI.EndChangeCheck())
                {
                    property.floatValue = Mathf.Max(0, newValue);
                }
                EditorGUIUtility.labelWidth = 0;
            }

            EditorGUI.EndProperty();
        }
    }
}