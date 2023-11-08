using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Mane.Editor.UIElements
{
    public class InfoBox : VisualElement
    {
        private static readonly GUID StyleGUID = new GUID("755944530f8e0404ba9ff91146e112b4");
        
        private const string InfoIconName = "console.infoicon";
        private const string WarningIconName = "console.warnicon";
        private const string ErrorIconName = "console.erroricon";
        
        private const string WarningStyleName = "warning";
        private const string ErrorStyleName = "error";

        private readonly Label _textLabel;
        private readonly VisualElement _icon;

        public InfoBox() => AddToClassList("info-box");

        public InfoBox(string text, MessageType type = MessageType.Info) : this()
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

        private void SetIcon(MessageType type) => SetIcon(GetIconName(type), false);

        private void SetStyle(MessageType type)
        {
            switch (type)
            {
                case MessageType.Error:
                    AddToClassList(ErrorStyleName);
                    break;
                case MessageType.Warning:
                    AddToClassList(WarningStyleName);
                    break;
            }
        }

        private void ClearStyle()
        {
            RemoveFromClassList(ErrorStyleName);
            RemoveFromClassList(WarningStyleName);
        }

        private string GetIconName(MessageType type) => type switch
        {
            MessageType.Info => InfoIconName,
            MessageType.Warning => WarningIconName,
            MessageType.Error => ErrorIconName,
            _ => InfoIconName
        };

        private MessageType GetMessageType(string iconName) => iconName switch
        {
            InfoIconName => MessageType.Info,
            WarningIconName => MessageType.Warning,
            ErrorIconName => MessageType.Error,
            _ => MessageType.Info
        };

        public new class UxmlFactory : UxmlFactory<InfoBox, UxmlTraits> { }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            private readonly UxmlStringAttributeDescription _text =
                new UxmlStringAttributeDescription { name = "text" };

            private readonly UxmlStringAttributeDescription _iconName =
                new UxmlStringAttributeDescription { name = "iconName", defaultValue = InfoIconName };

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