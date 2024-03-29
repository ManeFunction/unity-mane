﻿using System.Globalization;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Mane.Inspector.Editor
{
    /// <summary>
    /// Provides helper methods for working with Unity's SerializedProperty class.
    /// </summary>
    public static class PropertyDrawersHelper
    {
        /// <summary>
        /// Checks if a SerializedProperty is set to its default value.
        /// </summary>
        /// <param name="property">The SerializedProperty to check.</param>
        /// <returns>True if the property is set to its default value, false otherwise.</returns>
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

        /// <summary>
        /// Gets a label for a SerializedProperty that includes the odds of the property's value.
        /// </summary>
        /// <param name="property">The SerializedProperty to get the label for.</param>
        /// <param name="originalLabel">The original label of the property.</param>
        /// <param name="totalWeightPropertyName">The name of the property that represents the total weight.</param>
        /// <returns>A string representing the label of the property, including the odds of the property's value.</returns>
        public static string GetOddsLabel(this SerializedProperty property,
            string originalLabel, string totalWeightPropertyName)
        {
            string result = originalLabel;
            
            var obj = property.serializedObject.targetObject;
            PropertyInfo totalWeightPropertyInfo = obj.GetType().GetProperty(totalWeightPropertyName, 
                BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (totalWeightPropertyInfo != null && totalWeightPropertyInfo.PropertyType == typeof(float))
            {
                float totalWeight = (float)totalWeightPropertyInfo.GetValue(obj);
                float odds = property.floatValue / totalWeight;
                result = $"{result} ({(odds * 100f).ToString("F2", CultureInfo.InvariantCulture)}%)";
            }

            return result;
        }
    }
}