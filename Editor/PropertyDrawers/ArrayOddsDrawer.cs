using System.Linq;
using Mane.Extensions;
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

            string customLabel = label.text;
            if (!string.IsNullOrWhiteSpace(attr.CustomLabel))
            {
                if (attr.UseNumeration)
                {
                    var s = customLabel.Split(' ');
                    if (s.Length > 0)
                    {
                        int number = s.Last().ParseInt();
                        if (attr.IsHumanReadableNumeration)
                            number++;
                        customLabel = attr.CustomLabel.Replace("{0}", number.ToString());
                    }
                }
                else
                    customLabel = attr.CustomLabel;
            }
            
            string newLabel = property.GetOddsLabel(customLabel, attr.TotalWeightProperty);
            
            EditorGUI.PropertyField(position, property, new GUIContent(newLabel, label.tooltip), true);
        }
    }
}