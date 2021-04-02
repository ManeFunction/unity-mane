using System.Linq;
using Mane.Extensions;
using UnityEditor;
using UnityEngine;

namespace Mane.Inspector.Editor
{
    [CustomPropertyDrawer(typeof(EnumArrayAttribute))]
    public class EnumArrayDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        protected virtual EnumArrayAttribute Attribute
        {
            get { return (EnumArrayAttribute) attribute; }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            string newLabel = string.Empty;
            if (Attribute.EnumType.IsEnum)
            {
                int idx = label.text.Split(' ').Last().ParseInt();
                object e = System.Enum.ToObject(Attribute.EnumType, idx);
                newLabel = System.Enum.GetName(Attribute.EnumType, e);
            }

            if (string.IsNullOrEmpty(newLabel))
                newLabel = label.text;

            EditorGUI.PropertyField(position, property, new GUIContent(newLabel, label.tooltip), true);
        }
    }
}