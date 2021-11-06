using UnityEngine;

namespace Mane.Inspector
{
    public class AvailableIfAttribute : PropertyAttribute
    {
        public bool IsAvailable { get; }
        public string PropertyName { get; }
        public bool Hide { get; }
        public bool Invert { get; }

        public AvailableIfAttribute(bool isAvailable, bool hide = false)
        {
            IsAvailable = isAvailable;
            Hide = hide;
        }

        public AvailableIfAttribute(string propertyName, bool invert = false, bool hide = false)
        {
            PropertyName = propertyName;
            Invert = invert;
            Hide = hide;
        }
    }
}