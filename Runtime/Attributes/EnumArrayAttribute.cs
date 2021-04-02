using System;
using UnityEngine;

namespace Mane.Inspector
{
    public class EnumArrayAttribute : PropertyAttribute
    {
        public Type EnumType { get; private set; }
        
        public EnumArrayAttribute(Type enumType)
        {
            EnumType = enumType;
        }
    }
}