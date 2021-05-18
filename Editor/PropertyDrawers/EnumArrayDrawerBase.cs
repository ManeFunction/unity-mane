using System.Linq;
using Mane.Extensions;
using UnityEditor;
using UnityEngine;

namespace Mane.Inspector.Editor
{
    public abstract class EnumArrayDrawerBase : PropertyDrawer
    {
        protected string GetEnumName(GUIContent label)
        {
            EnumArrayAttribute attr = (EnumArrayAttribute)attribute;
            
            string newLabel = string.Empty;
            if (attr.EnumType.IsEnum)
            {
                int idx = label.text.Split(' ').Last().ParseInt();
                object e = System.Enum.ToObject(attr.EnumType, idx);
                newLabel = System.Enum.GetName(attr.EnumType, e);
            }

            if (string.IsNullOrEmpty(newLabel))
                newLabel = label.text;

            return newLabel;
        }
    }
}