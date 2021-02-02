using System;
using UnityEditor;
using UnityEngine;

namespace Mane.Extensions.Editor
{
    [CustomPropertyDrawer(typeof(ReadOnlyIfAttribute))]
    public class ReadOnlyIfPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ReadOnlyIfAttribute attr = attribute as ReadOnlyIfAttribute;

            bool locked = attr.Locked;

            if (!string.IsNullOrEmpty(attr.PropertyName))
            {
                SerializedProperty attachedProperty =
                    property.serializedObject.FindProperty(attr.PropertyName);
                if (attachedProperty != null)
                {
                    switch (attachedProperty.propertyType)
                    {
                        case SerializedPropertyType.Integer:
                            locked = attachedProperty.intValue == 0;
                            break;
                        case SerializedPropertyType.Boolean:
                            locked = attachedProperty.boolValue;
                            break;
                        case SerializedPropertyType.Float:
                            locked = attachedProperty.floatValue == 0f;
                            break;
                        case SerializedPropertyType.String:
                            locked = string.IsNullOrEmpty(attachedProperty.stringValue);
                            break;
                        case SerializedPropertyType.ObjectReference:
                            locked = attachedProperty.objectReferenceValue == null;
                            break;
                        case SerializedPropertyType.Vector2:
                            locked = attachedProperty.vector2Value == Vector2.zero;
                            break;
                        case SerializedPropertyType.Vector3:
                            locked = attachedProperty.vector3Value == Vector3.zero;
                            break;
                        case SerializedPropertyType.Vector4:
                            locked = attachedProperty.vector4Value == Vector4.zero;
                            break;
                        case SerializedPropertyType.Rect:
                            locked = attachedProperty.rectValue == Rect.zero;
                            break;
                        case SerializedPropertyType.Quaternion:
                            locked = attachedProperty.quaternionValue == Quaternion.identity;
                            break;
                        case SerializedPropertyType.ExposedReference:
                            locked = attachedProperty.exposedReferenceValue == null;
                            break;
                        case SerializedPropertyType.Vector2Int:
                            locked = attachedProperty.vector2IntValue == Vector2Int.zero;
                            break;
                        case SerializedPropertyType.Vector3Int:
                            locked = attachedProperty.vector3IntValue == Vector3Int.zero;
                            break;
                        default:
                            Debug.LogWarning($"ReadOnlyIf: Unsupported parameter type on {property.serializedObject.targetObject.name}: {attachedProperty.propertyType} {attachedProperty.name}!");
                            break;
                    }

                    if (attr.Invert)
                    {
                        locked = !locked;
                    }
                }
                else
                {
                    Debug.LogWarning($"ReadOnlyIf: Parameter {attr.PropertyName} not found on {property.serializedObject.targetObject.name}!");
                }
            }

            if (locked)
            {
                GUI.enabled = false;
            }

            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
}