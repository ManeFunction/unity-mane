using System.Globalization;
using System.Reflection;
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

            var obj = property.serializedObject.targetObject;
            PropertyInfo totalWeightPropertyInfo = obj.GetType().GetProperty(attr.TotalWeightProperty, 
                BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (totalWeightPropertyInfo != null && totalWeightPropertyInfo.PropertyType == typeof(float))
            {
                float totalWeight = (float)totalWeightPropertyInfo.GetValue(obj);
                float odds = property.floatValue / totalWeight;
                newLabel = $"{newLabel} ({(odds * 100f).ToString("F2", CultureInfo.InvariantCulture)}%)";
            }

            EditorGUI.PropertyField(position, property, new GUIContent(newLabel, label.tooltip), true);
        }
    }
}