using System;
using UnityEngine;

namespace Mane.Inspector
{
    [AttributeUsage(AttributeTargets.Field)]
    public class SerializeReferenceDropdownAttribute : PropertyAttribute { }
}
