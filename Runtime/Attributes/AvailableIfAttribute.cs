using UnityEngine;

namespace Mane.Inspector
{
    public class AvailableIfAttribute : PropertyAttribute
    {
        public bool IsAvailable { get; private set; }
        public string PropertyName { get; private set; }
        public bool Hide { get; private set; }
        public bool Invert { get; private set; }

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