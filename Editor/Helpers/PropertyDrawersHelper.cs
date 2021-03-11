using UnityEditor;
using UnityEngine;

namespace Mane.Extensions.Editor
{
    public static class PropertyDrawersHelper
    {
        public static bool IsPropertyDefault(this SerializedProperty property)
        {
            bool isDefault;
            switch (property.propertyType)
            {
                case SerializedPropertyType.Integer:
                    isDefault = property.intValue == 0;
                    break;
                case SerializedPropertyType.Boolean:
                    isDefault = !property.boolValue;
                    break;
                case SerializedPropertyType.Float:
                    isDefault = property.floatValue == 0f;
                    break;
                case SerializedPropertyType.String:
                    isDefault = string.IsNullOrEmpty(property.stringValue);
                    break;
                case SerializedPropertyType.ObjectReference:
                    isDefault = property.objectReferenceValue == null;
                    break;
                case SerializedPropertyType.Vector2:
                    isDefault = property.vector2Value == Vector2.zero;
                    break;
                case SerializedPropertyType.Vector3:
                    isDefault = property.vector3Value == Vector3.zero;
                    break;
                case SerializedPropertyType.Vector4:
                    isDefault = property.vector4Value == Vector4.zero;
                    break;
                case SerializedPropertyType.Rect:
                    isDefault = property.rectValue == Rect.zero;
                    break;
                case SerializedPropertyType.Quaternion:
                    isDefault = property.quaternionValue == Quaternion.identity;
                    break;
                case SerializedPropertyType.ExposedReference:
                    isDefault = property.exposedReferenceValue == null;
                    break;
                case SerializedPropertyType.Vector2Int:
                    isDefault = property.vector2IntValue == Vector2Int.zero;
                    break;
                case SerializedPropertyType.Vector3Int:
                    isDefault = property.vector3IntValue == Vector3Int.zero;
                    break;
                default:
                    Debug.LogWarning(
                        $"Property Manager: Unsupported parameter type on {property.serializedObject.targetObject.name}: {property.propertyType} {property.name}!");
                    isDefault = false;
                    break;
            }

            return isDefault;
        }
    }
}