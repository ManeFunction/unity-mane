using UnityEngine;

namespace Mane.Inspector
{
    public class InfoBoxAttribute : PropertyAttribute
    {
        public string Message { get; }
        public InfoBoxType Type { get; }

        public InfoBoxAttribute(string message, InfoBoxType type = InfoBoxType.Info)
        {
            Message = message;
            Type = type;
        }
    }

    public enum InfoBoxType
    {
        Info,
        Warning,
        Error,
    }
}