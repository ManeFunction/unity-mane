using UnityEditor;
using UnityEngine;

namespace Mane.Inspector.Editor
{
    [CustomPropertyDrawer(typeof(ArrayOddsAttribute))]
    public class ArrayOddsDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => 
            EditorGUI.GetPropertyHeight(property, label, true);

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ArrayOddsAttribute attr = (ArrayOddsAttribute) attribute;
            string newLabel = property.GetOddsLabel(label.text, attr.TotalWeightProperty);
            
            EditorGUI.PropertyField(position, property, new GUIContent(newLabel, label.tooltip), true);
        }
    }
}