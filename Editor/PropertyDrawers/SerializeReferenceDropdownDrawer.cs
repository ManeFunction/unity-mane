using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mane.Inspector.Editor
{
    [CustomPropertyDrawer(typeof(SerializeReferenceDropdownAttribute))]
    public class SerializeReferenceDropdownDrawer : PropertyDrawer
    {
        private Type[] _cachedTypes;
        private string[] _cachedLabels;
        private Type _fieldType;
        private const float SpacingAfterDropdown = 2f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            if (_fieldType == null)
                _fieldType = GetFieldType(fieldInfo.FieldType);

            if (_cachedTypes == null || _cachedLabels == null)
                CacheTypeData(_fieldType);

            // Get current selection index
            int currentIndex = 0; // Default to None
            if (property.managedReferenceValue != null)
            {
                var currentType = property.managedReferenceValue.GetType();
                currentIndex = Array.IndexOf(_cachedTypes, currentType) + 1; // +1 because 0 is None
            }

            // Calculate rects
            var dropdownRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            
            // Show dropdown
            EditorGUI.BeginChangeCheck();
            int selectedIndex = EditorGUI.Popup(dropdownRect, label.text, currentIndex, _cachedLabels);
            if (EditorGUI.EndChangeCheck())
            {
                property.serializedObject.Update();
                if (selectedIndex == 0) // None selected
                {
                    property.managedReferenceValue = null;
                }
                else
                {
                    var selectedType = _cachedTypes[selectedIndex - 1];
                    property.managedReferenceValue = Activator.CreateInstance(selectedType);
                }
                property.serializedObject.ApplyModifiedProperties();
            }

            // Draw the fields of the selected implementation
            if (property.managedReferenceValue != null)
            {
                EditorGUI.indentLevel++;
                
                var fieldsRect = new Rect(
                    position.x, 
                    dropdownRect.y + dropdownRect.height + SpacingAfterDropdown,
                    position.width,
                    position.height - dropdownRect.height - SpacingAfterDropdown
                );

                // Draw child properties
                var childProperty = property.Copy();
                var parentPath = childProperty.propertyPath;
                
                // Enter the managed reference
                childProperty.Next(true);
                
                while (childProperty.propertyPath.StartsWith(parentPath))
                {
                    EditorGUI.PropertyField(
                        new Rect(fieldsRect.x, fieldsRect.y, fieldsRect.width, EditorGUI.GetPropertyHeight(childProperty)),
                        childProperty, 
                        true
                    );
                    fieldsRect.y += EditorGUI.GetPropertyHeight(childProperty) + EditorGUIUtility.standardVerticalSpacing;
                    
                    if (!childProperty.Next(false)) break;
                }

                EditorGUI.indentLevel--;
            }

            EditorGUI.EndProperty();
        }

        private Type GetFieldType(Type type)
        {
            if (type.IsArray)
                return type.GetElementType();
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
                return type.GetGenericArguments()[0];
            return type;
        }

        private void CacheTypeData(Type baseType)
        {
            // Get all assignable non-abstract types
            _cachedTypes = TypeCache.GetTypesDerivedFrom(baseType)
                .Where(t => !t.IsAbstract && !t.IsGenericType)
                .ToArray();

            // Create labels array with "None" as first option
            _cachedLabels = new string[_cachedTypes.Length + 1];
            _cachedLabels[0] = "None";
            for (int i = 0; i < _cachedTypes.Length; i++)
            {
                _cachedLabels[i + 1] = _cachedTypes[i].Name;
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = EditorGUIUtility.singleLineHeight;

            if (property.managedReferenceValue != null)
            {
                height += SpacingAfterDropdown;

                // Add height of child properties
                var childProperty = property.Copy();
                var parentPath = childProperty.propertyPath;
                
                // Enter the managed reference
                childProperty.Next(true);
                
                while (childProperty.propertyPath.StartsWith(parentPath))
                {
                    height += EditorGUI.GetPropertyHeight(childProperty) + EditorGUIUtility.standardVerticalSpacing;
                    if (!childProperty.Next(false)) break;
                }
            }

            return height;
        }
    }
}