using UnityEngine;
using UnityEditor;

namespace Mane.Editor
{
    public abstract class MinMaxDrawer<T> : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var min = property.FindPropertyRelative("Min");
            var max = property.FindPropertyRelative("Max");

            T minValue = GetPropertyValue(min);
            T maxValue = GetPropertyValue(max);

            Rect labelPosition = position;
            labelPosition.width /= 3f;
            EditorGUI.LabelField(labelPosition, property.displayName);

            Rect minPosition = position;
            minPosition.width /= 3f;
            minPosition.x += labelPosition.width;

            Rect maxPosition = position;
            maxPosition.width /= 3f;
            maxPosition.x += labelPosition.width * 2f;

            float minLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 50f;
            EditorGUI.BeginChangeCheck();
            minValue = DrawProperty(minPosition, new GUIContent("Min"), minValue);
            maxValue = DrawProperty(maxPosition, new GUIContent("Max"), maxValue);
            if (EditorGUI.EndChangeCheck())
            {
                SetPropertyValue(min, minValue);
                SetPropertyValue(max, maxValue);
            }

            EditorGUIUtility.labelWidth = minLabelWidth;

            EditorGUI.EndProperty();
        }

        protected abstract T GetPropertyValue(SerializedProperty property);

        protected abstract void SetPropertyValue(SerializedProperty property, T value);

        protected abstract T DrawProperty(Rect position, GUIContent label, T value);
    }

    
    [CustomPropertyDrawer(typeof(MinMax))]
    public class MinMaxDrawer : MinMaxDrawer<float>
    {
        protected override float GetPropertyValue(SerializedProperty property) =>
            property.floatValue;

        protected override void SetPropertyValue(SerializedProperty property, float value) =>
            property.floatValue = value;

        protected override float DrawProperty(Rect position, GUIContent label, float value) =>
            EditorGUI.FloatField(position, label, value);
    }

    
    [CustomPropertyDrawer(typeof(MinMaxInt))]
    public class MinMaxIntDrawer : MinMaxDrawer<int>
    {
        protected override int GetPropertyValue(SerializedProperty property) =>
            property.intValue;

        protected override void SetPropertyValue(SerializedProperty property, int value) =>
            property.intValue = value;

        protected override int DrawProperty(Rect position, GUIContent label, int value) =>
            EditorGUI.IntField(position, label, value);
    }
}