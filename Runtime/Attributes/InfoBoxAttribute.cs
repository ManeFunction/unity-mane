using UnityEditor;
using UnityEngine;

namespace Mane.Inspector
{
    public class InfoBoxAttribute : PropertyAttribute
    {
        public string Message { get; }
        public MessageType Type { get; }

        public InfoBoxAttribute(string message, MessageType type = MessageType.Info)
        {
            Message = message;
            Type = type;
        }
    }
}