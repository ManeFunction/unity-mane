using System;
using System.Linq;
using Codice.CM.SEIDInfo;
using UnityEditor;
using UnityEngine;

namespace Mane.Inspector.Editor
{
    [CustomPropertyDrawer(typeof(SerializedListAttribute))]
    public class SerializedListPropertyDrawer : PropertyDrawer
    {
        private const string None = "None";

        private bool _isNone;
        private string[] _chestIDs;
        private bool _isInteger;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            EditorGUI.GetPropertyHeight(property, label, true);

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _isInteger = property.propertyType == SerializedPropertyType.Integer;
            if (!_isInteger && property.propertyType != SerializedPropertyType.String)
            {
                base.OnGUI(position, property, label);

                return;
            }

            if (_chestIDs == null)
            {
                SerializedListAttribute attr = attribute as SerializedListAttribute;

                if (attr.ListType.BaseType != typeof(SerializedListAsset))
                {
                    base.OnGUI(position, property, label);
                    
                    return;
                }

                Type type = attr.ListType;
                Type parentType = type.BaseType;
                var listProperty = parentType.GetProperty("List");
                string[] list = (string[])listProperty.GetValue(null);
                
                _isNone = attr.NoneField;
                _chestIDs = _isNone
                    ? new[] { None }.Concat(list).ToArray()
                    : list;
            }

            int index = _isInteger 
                ? _isNone ? property.intValue + 1 : property.intValue
                : Mathf.Max(0, Array.IndexOf(_chestIDs, property.stringValue));
            
            index = EditorGUI.Popup(position, property.displayName, index, _chestIDs);

            if (_isInteger)
                property.intValue = _isNone ? index - 1 : index;
            else
                property.stringValue = _isNone && index == 0 ? string.Empty : _chestIDs[index];
        }
    }
}