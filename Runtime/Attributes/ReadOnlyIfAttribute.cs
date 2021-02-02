using UnityEngine;

namespace Mane.Extensions
{
    public class ReadOnlyIfAttribute : PropertyAttribute
    {
        public bool Locked { get; private set; }
        public string PropertyName { get; private set; }
        public bool Invert { get; private set; }

        public ReadOnlyIfAttribute(bool locked = false)
        {
            Locked = locked;
        }

        public ReadOnlyIfAttribute(string propertyName, bool invert = false)
        {
            PropertyName = propertyName;
            Invert = invert;
        }
    }
}