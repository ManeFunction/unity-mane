using UnityEditor;
using UnityEngine;

namespace Mane.Inspector.Editor
{
    [CustomPropertyDrawer(typeof(EnumArrayOddsAttribute))]
    public class EnumArrayOddsDrawer : EnumArrayDrawerBase
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => 
            EditorGUI.GetPropertyHeight(property, label, true);

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            string newLabel = GetEnumName(label);

            EnumArrayOddsAttribute attr = (EnumArrayOddsAttribute) attribute;
            newLabel = property.GetOddsLabel(newLabel, attr.TotalWeightProperty);

            EditorGUI.PropertyField(position, property, new GUIContent(newLabel, label.tooltip), true);
        }
    }
}