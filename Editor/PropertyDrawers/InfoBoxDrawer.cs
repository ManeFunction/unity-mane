using UnityEditor;
using UnityEngine;

namespace Mane.Inspector.Editor
{
    [CustomPropertyDrawer(typeof(InfoBoxAttribute))]
    public class InfoBoxDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            InfoBoxAttribute infoBox = (InfoBoxAttribute)attribute;

            // Calculate the position for the help box
            Rect helpBoxRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight * 2);
            EditorGUI.HelpBox(helpBoxRect, infoBox.Message, GetMessageType(infoBox.Type));

            // Adjust the position for the property field below the help box
            Rect propertyRect = new Rect(position.x,
                position.y + helpBoxRect.height + EditorGUIUtility.standardVerticalSpacing, position.width,
                EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(propertyRect, property, label, true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // Calculate the height of the help box and the property field
            float helpBoxHeight = EditorGUIUtility.singleLineHeight * 2;
            float propertyHeight = base.GetPropertyHeight(property, label);
            return helpBoxHeight + propertyHeight + EditorGUIUtility.standardVerticalSpacing;
        }

        // TODO: Adapt for UIElements
        // public override VisualElement CreatePropertyGUI(SerializedProperty property)
        // {
        //     InfoBoxAttribute infoBox = (InfoBoxAttribute)attribute;
        //     
        //     // Create property container element.
        //     var container = new VisualElement();
        //
        //     // Create property fields.
        //     container.Add(new InfoBox(infoBox.Message, infoBox.Type));
        //     var amountField = new PropertyField(property);
        //
        //     // Add fields to the container.
        //     container.Add(amountField);
        //
        //     return container;
        // }
        
        private MessageType GetMessageType(InfoBoxType type) => type switch
        {
            InfoBoxType.Info => MessageType.Info,
            InfoBoxType.Warning => MessageType.Warning,
            InfoBoxType.Error => MessageType.Error,
            _ => MessageType.None
        };
    }
}