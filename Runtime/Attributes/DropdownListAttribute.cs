using System;
using System.Reflection;
using UnityEngine;

namespace Mane.Inspector
{
    public class DropdownListAttribute : PropertyAttribute
    {
        public string[] Strings { get; private set; }

        public DropdownListAttribute(params string[] strings) { Strings = strings; }

        public DropdownListAttribute(Type type, string methodName)
        {
            MethodInfo method = type.GetMethod(methodName);
            if (method != null)
            {
                Strings = method.Invoke(null, null) as string[];
            }
            else
            {
                Debug.LogError($"Dropdown List: Can't find method {methodName} in {type}");
            }
        }
    }
}