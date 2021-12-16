using System;
using UnityEngine;

namespace Mane.Inspector
{
    public class EnumArrayAttribute : PropertyAttribute
    {
        public Type EnumType { get; }
        
        public EnumArrayAttribute(Type enumType) => EnumType = enumType;
    }
}