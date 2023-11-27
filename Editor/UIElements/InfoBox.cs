using Mane.Inspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Mane.Editor.UIElements
{
    public class InfoBox : VisualElement
    {
        private static readonly GUID StyleGUID = new("755944530f8e0404ba9ff91146e112b4");
        
        private const string InfoIconName = "console.infoicon";
        private const string WarningIconName = "console.warnicon";
        private const string ErrorIconName = "console.erroricon";
        
        private const string WarningStyleName = "warning";
        private const string ErrorStyleName = "error";

        private readonly Label _textLabel;
        private readonly VisualElement _icon;

        public InfoBox() => AddToClassList("info-box");

        public InfoBox(string text, InfoBoxType type = InfoBoxType.Info) : this()
        {
            _icon = new VisualElement();
            _icon.AddToClassList("info-box-icon");
            SetIcon(type);
            Add(_icon);

            _textLabel = new Label(text);
            _textLabel.AddToClassList("info-box-text");
            Add(_textLabel);

            styleSheets.Add(AssetDatabase
                .LoadAssetAtPath<StyleSheet>(AssetDatabase.GUIDToAssetPath(StyleGUID)));

            SetStyle(type);
        }

        private void SetText(string text) => _textLabel.text = text;

        private void SetIcon(string iconName, bool changeStyle = true)
        {
            Texture2D iconTexture = EditorGUIUtility.IconContent(iconName).image as Texture2D;
            _icon.style.backgroundImage = iconTexture;

            if (changeStyle)
            {
                ClearStyle();
                SetStyle(GetMessageType(iconName));
            }
        }

        private void SetIcon(InfoBoxType type) => SetIcon(GetIconName(type), false);

        private void SetStyle(InfoBoxType type)
        {
            switch (type)
            {
                case InfoBoxType.Error:
                    AddToClassList(ErrorStyleName);
                    break;
                case InfoBoxType.Warning:
                    AddToClassList(WarningStyleName);
                    break;
            }
        }

        private void ClearStyle()
        {
            RemoveFromClassList(ErrorStyleName);
            RemoveFromClassList(WarningStyleName);
        }

        private static string GetIconName(InfoBoxType type) => type switch
        {
            InfoBoxType.Info => InfoIconName,
            InfoBoxType.Warning => WarningIconName,
            InfoBoxType.Error => ErrorIconName,
            _ => InfoIconName
        };

        private static InfoBoxType GetMessageType(string iconName) => iconName switch
        {
            InfoIconName => InfoBoxType.Info,
            WarningIconName => InfoBoxType.Warning,
            ErrorIconName => InfoBoxType.Error,
            _ => InfoBoxType.Info
        };

        public new class UxmlFactory : UxmlFactory<InfoBox, UxmlTraits> { }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            private readonly UxmlStringAttributeDescription _text = new() { name = "text" };

            private readonly UxmlStringAttributeDescription _iconName =
                new() { name = "iconName", defaultValue = InfoIconName };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);

                var infoBox = (InfoBox)ve;
                infoBox.SetText(_text.GetValueFromBag(bag, cc));
                infoBox.SetIcon(_iconName.GetValueFromBag(bag, cc));
            }
        }
    }
}