using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mane.Inspector.Editor
{
    [CustomPropertyDrawer(typeof(SerializeReferenceInterfaceAttribute))]
    public class SerializeReferenceInterfaceDrawer : PropertyDrawer
    {
        private Type[] _cachedTypes;
        private string[] _cachedLabels;
        private Type _fieldType;
        private const float SpacingAfterDropdown = 2f;
        private const string PrefsPrefix = "ManeSRIFoldout_";

        private bool GetFoldoutState(string fieldName) => EditorPrefs.GetBool(PrefsPrefix + fieldName, true);
        private void SetFoldoutState(string fieldName, bool state) => EditorPrefs.SetBool(PrefsPrefix + fieldName, state);

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

            var mainLineRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            
            bool hasChildren = false;
            if (property.managedReferenceValue != null)
            {
                var tempProperty = property.Copy();
                var parentPath = tempProperty.propertyPath;
                tempProperty.Next(true);
                hasChildren = tempProperty.propertyPath.StartsWith(parentPath);
            }

            EditorGUI.BeginChangeCheck();
            
            var popupRect = mainLineRect;
            int selectedIndex = currentIndex;
            
            if (hasChildren)
            {
                var foldoutRect = new Rect(mainLineRect.x, mainLineRect.y, EditorGUIUtility.labelWidth, mainLineRect.height);
                bool foldout = GetFoldoutState(fieldInfo.Name);
                bool newFoldout = EditorGUI.Foldout(foldoutRect, foldout, label, true);
                if (foldout != newFoldout)
                {
                    SetFoldoutState(fieldInfo.Name, newFoldout);
                }
                
                popupRect.x = foldoutRect.x + EditorGUIUtility.labelWidth;
                popupRect.width = mainLineRect.width - EditorGUIUtility.labelWidth;
                
                selectedIndex = EditorGUI.Popup(popupRect, selectedIndex, _cachedLabels);
            }
            else
            {
                selectedIndex = EditorGUI.Popup(popupRect, label.text, selectedIndex, _cachedLabels);
            }
            
            if (EditorGUI.EndChangeCheck())
            {
                property.serializedObject.Update();
                if (selectedIndex == 0) // None selected
                {
                    property.managedReferenceValue = null;
                }
                else if (selectedIndex != currentIndex) // Only create new instance if type actually changed
                {
                    var selectedType = _cachedTypes[selectedIndex - 1];
                    property.managedReferenceValue = Activator.CreateInstance(selectedType);
                }
                property.serializedObject.ApplyModifiedProperties();
            }

            if (hasChildren && property.managedReferenceValue != null && GetFoldoutState(fieldInfo.Name))
            {
                EditorGUI.indentLevel++;
                
                var childRect = new Rect(
                    position.x,
                    mainLineRect.y + mainLineRect.height + SpacingAfterDropdown,
                    position.width,
                    position.height - mainLineRect.height - SpacingAfterDropdown
                );

                var childProperty = property.Copy();
                var parentPath = childProperty.propertyPath;
                childProperty.Next(true);
                
                while (childProperty.propertyPath.StartsWith(parentPath))
                {
                    EditorGUI.PropertyField(
                        new Rect(childRect.x, childRect.y, childRect.width, EditorGUI.GetPropertyHeight(childProperty)),
                        childProperty,
                        true
                    );
                    childRect.y += EditorGUI.GetPropertyHeight(childProperty) + EditorGUIUtility.standardVerticalSpacing;
                    
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
            // Get all assignable non-abstract types, excluding UnityEngine.Object derivatives
            _cachedTypes = TypeCache.GetTypesDerivedFrom(baseType)
                .Where(t => !t.IsAbstract && !t.IsGenericType && !typeof(UnityEngine.Object).IsAssignableFrom(t))
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
                var tempProperty = property.Copy();
                var parentPath = tempProperty.propertyPath;
                tempProperty.Next(true);
                bool hasChildren = tempProperty.propertyPath.StartsWith(parentPath);

                if (hasChildren && GetFoldoutState(fieldInfo.Name))
                {
                    height += SpacingAfterDropdown;

                    var childProperty = property.Copy();
                    childProperty.Next(true);
                    
                    while (childProperty.propertyPath.StartsWith(parentPath))
                    {
                        height += EditorGUI.GetPropertyHeight(childProperty) + EditorGUIUtility.standardVerticalSpacing;
                        if (!childProperty.Next(false)) break;
                    }
                }
            }

            return height;
        }
    }
}