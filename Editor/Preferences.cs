using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;


namespace Mane.Editor
{
    internal static class ManeSettingsUIElementsRegister
    {
        [SettingsProvider]
        public static SettingsProvider CreateManeSettingsProvider()
        {
            SettingsProvider provider =
                new SettingsProvider("Project/ManeUIElementsSettings", SettingsScope.Project)
            {
                label = "Mane Settings",
                activateHandler = (searchContext, rootElement) =>
                {
                    SerializedObject settings = ManeSettings.GetSerializedSettings();

                    // rootElement is a VisualElement. If you add any children to it, the OnGUI function
                    // isn't called because the SettingsProvider uses the UIElements drawing framework.
                    StyleSheet styleSheet = AssetDatabase
                        .LoadAssetAtPath<StyleSheet>("Packages/com.manefunction.tools/Editor/UI/mane_settings_ui.uss");
                    if (styleSheet)
                        rootElement.styleSheets.Add(styleSheet);
                    Label title = new Label
                    {
                        text = "Mane Settings"
                    };
                    title.AddToClassList("title");
                    rootElement.Add(title);

                    VisualElement properties = new VisualElement
                    {
                        style =
                        {
                            flexDirection = FlexDirection.Column
                        }
                    };
                    rootElement.Add(properties);

                    AddProperty(properties, new PropertyField(settings.FindProperty("_prefabsSavingPath")));

                    rootElement.Bind(settings);
                },

                // Populate the search keywords to enable smart search filtering and label highlighting
                keywords = new HashSet<string>(new[] { "Prefabs Saving Path" })
            };

            return provider;


            void AddProperty(VisualElement properties, VisualElement property)
            {
                properties.Add(property);
                property.AddToClassList("property");
            }
        }
    }
}