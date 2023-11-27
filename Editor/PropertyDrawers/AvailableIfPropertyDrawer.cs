using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Mane.Inspector.Editor
{
    [CustomPropertyDrawer(typeof(AvailableIfAttribute))]
    public class AvailableIfPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var attr = ProcessAttribute(property);

            return attr is { isAvailable: false, hide: true } ? 0f : 
                EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var attr = ProcessAttribute(property);

            if (!attr.isAvailable)
            {
                if (attr.hide) return;
                
                GUI.enabled = false;
            }

            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }

        private (bool isAvailable, bool hide) ProcessAttribute(SerializedProperty property)
        {
            AvailableIfAttribute attr = attribute as AvailableIfAttribute;

            bool isAvailable = attr!.IsAvailable;

            if (!string.IsNullOrEmpty(attr.PropertyName))
            {
                // Trying to get SerializedProperty
                SerializedProperty attachedProperty =
                    property.serializedObject.FindProperty(attr.PropertyName);
                if (attachedProperty == null)
                {
                    // Trying to get Method
                    object target = property.serializedObject.targetObject;
                    Type type = target.GetType();
                    MethodInfo method = type.GetMethod(attr.PropertyName);
                    // ...or a Property getter
                    if (method == null)
                        method = type.GetProperty(attr.PropertyName)?.GetMethod;
            
                    if (method != null)
                        isAvailable = (bool)method.Invoke(target, null);
                    else
                        Debug.LogError($"AvailableIf: Can't find {attr.PropertyName} in {type}");
                }
                else
                    isAvailable = !attachedProperty.IsPropertyDefault();
            }
            
            if (attr.Invert)
                isAvailable = !isAvailable;

            return (isAvailable, attr.Hide);
        }
    }
}