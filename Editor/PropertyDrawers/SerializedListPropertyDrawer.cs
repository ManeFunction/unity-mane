using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Mane.Inspector.Editor
{
    [CustomPropertyDrawer(typeof(SerializedListAttribute))]
    public class SerializedListPropertyDrawer : PropertyDrawer
    {
        private const string None = "None";
        private const string NotSupported = "Only string and integer are supported!";
        private const string InheritFrom = "Inherit your asset from SerializedListAsset!";
        private const string ListIsEmpty = "Serialized List is empty!";

        private static readonly Dictionary<Type, SerializedListAsset> InitedTypes =
            new Dictionary<Type, SerializedListAsset>();

        private bool _isNone;
        private string[] _list;
        private bool _isInteger;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            EditorGUI.GetPropertyHeight(property, label, true);

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // check if property is string or integer
            _isInteger = property.propertyType == SerializedPropertyType.Integer;
            if (!_isInteger && property.propertyType != SerializedPropertyType.String)
            {
                ShowError(position, property, NotSupported);

                return;
            }

            // get list to show
            if (_list == null)
            {
                SerializedListAttribute attr = attribute as SerializedListAttribute;
                
                // check if asset was loaded
                if (!InitedTypes.ContainsKey(attr.ListType))
                {
                    // get asset path
                    FilePathAttribute pathAttribute = attr.ListType.GetCustomAttribute<FilePathAttribute>();
                    if (pathAttribute != null)
                    {
                        string path = (string)typeof(FilePathAttribute)
                            .GetProperty("filepath", BindingFlags.NonPublic | BindingFlags.Instance)
                            ?.GetValue(pathAttribute);

                        // load asset and store the state
                        SerializedListAsset listAsset =
                            AssetDatabase.LoadAssetAtPath<SerializedListAsset>($"Assets/{path}");
                        
                        InitedTypes.Add(attr.ListType, listAsset);
                    }
                }

                // check if asset is inherited from SerializedListAsset
                if (attr.ListType.BaseType != typeof(SerializedListAsset))
                {
                    ShowError(position, property, InheritFrom);
                    
                    return;
                }

                // get list from asset
                Type type = attr.ListType;
                Type baseType = type?.BaseType;
                PropertyInfo listProp = baseType
                    ?.GetProperty("List", BindingFlags.Public | BindingFlags.Static);
                string[] list = (string[])listProp?.GetValue(null);
                
                // check if something wrong with inheritance
                if (list == null)
                {
                    ShowError(position, property, InheritFrom);
                    
                    return;
                }
                
                // check if list is empty
                if (list.Length == 0)
                {
                    ShowError(position, property, ListIsEmpty);
                    
                    return;
                }
                
                // manage None option
                _isNone = attr.NoneField;
                _list = _isNone
                    ? new[] { None }.Concat(list).ToArray()
                    : list;
            }

            // read value
            int index = _isInteger 
                ? _isNone ? property.intValue + 1 : property.intValue
                : Mathf.Max(0, Array.IndexOf(_list, property.stringValue));
            
            // draw dropdown
            index = EditorGUI.Popup(position, property.displayName, index, _list);

            // write value
            if (_isInteger)
                property.intValue = _isNone ? index - 1 : index;
            else
                property.stringValue = _isNone && index == 0 ? string.Empty : _list[index];
        }

        private void ShowError(Rect position, SerializedProperty property, string label) => 
            EditorGUI.LabelField(position, $"{property.name}: {label}");
    }
}