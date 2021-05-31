// This feature is based on BinaryCats solution, took from this thread:
// https://forum.unity.com/threads/how-to-change-the-name-of-list-elements-in-the-inspector.448910/
// Thank you.

using System.Globalization;
using UnityEditor;
using UnityEngine;

namespace Mane.Inspector.Editor
{
    [CustomPropertyDrawer(typeof(ArrayElementsAttribute))]
    public class ArrayElementsDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        protected virtual ArrayElementsAttribute Attribute
        {
            get { return (ArrayElementsAttribute) attribute; }
        }

        private SerializedProperty _titleNameProp;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            string fullPathName = $"{property.propertyPath}.{Attribute.TitleVariableName}";
            _titleNameProp = property.serializedObject.FindProperty(fullPathName);
            string newLabel = GetTitle();
            if (string.IsNullOrEmpty(newLabel))
            {
                newLabel = label.text;
            }

            EditorGUI.PropertyField(position, property, new GUIContent(newLabel, label.tooltip), true);
        }

        private string GetTitle()
        {
            if (_titleNameProp == null) return string.Empty;

            switch (_titleNameProp.propertyType)
            {
                case SerializedPropertyType.Integer:
                    return _titleNameProp.intValue.ToString();
                
                case SerializedPropertyType.Boolean:
                    return _titleNameProp.boolValue.ToString();
                
                case SerializedPropertyType.Float:
                    return _titleNameProp.floatValue.ToString(CultureInfo.InvariantCulture);
                
                case SerializedPropertyType.String:
                    return _titleNameProp.stringValue;
                
                case SerializedPropertyType.Color:
                    return _titleNameProp.colorValue.ToString();
                
                case SerializedPropertyType.ObjectReference:
                    if (!_titleNameProp.objectReferenceValue)
                        return "None (Empty Object Ref)";
                    return _titleNameProp.objectReferenceValue.ToString();
                
                case SerializedPropertyType.Enum:
                    if (_titleNameProp.enumValueIndex < 0) break;
                    return _titleNameProp.enumDisplayNames[_titleNameProp.enumValueIndex];
                
                case SerializedPropertyType.Vector2:
                    return _titleNameProp.vector2Value.ToString();
                
                case SerializedPropertyType.Vector3:
                    return _titleNameProp.vector3Value.ToString();
                
                case SerializedPropertyType.Vector4:
                    return _titleNameProp.vector4Value.ToString();
                
                case SerializedPropertyType.Rect:
                    return _titleNameProp.rectValue.ToString();
                
                case SerializedPropertyType.Quaternion:
                    return _titleNameProp.quaternionValue.ToString();
                
                default: break;
            }

            return string.Empty;
        }
    }
}