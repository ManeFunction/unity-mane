using System;
using UnityEngine;

namespace Mane.Inspector
{
    public class SerializedListAttribute : PropertyAttribute
    {
        public bool NoneField { get; }
        public Type ListType { get; }

        public SerializedListAttribute(Type listType, bool noneField = true)
        {
            ListType = listType;
            NoneField = noneField;
        }
    }
}