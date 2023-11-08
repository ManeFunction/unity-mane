using System;
using System.Reflection;
using UnityEditor;

namespace Mane.Editor.Extensions
{
    public static class SerializedPropertyExtensions
    {
        public static T GetAttribute<T>(this SerializedProperty property) where T : Attribute
        {
            // Get the object the property belongs to
            FieldInfo field = GetFieldInfoFromProperty(property);

            // If the field is not null, get the attribute of type T
            if (field != null)
            {
                T attribute = field.GetCustomAttribute<T>();
                return attribute;
            }

            return null;
        }

        private static FieldInfo GetFieldInfoFromProperty(SerializedProperty property)
        {
            // Get the path to the property
            string path = property.propertyPath;
            object targetObject = property.serializedObject.targetObject;
            FieldInfo field = null;

            // Split the path into parts
            string[] parts = path.Split('.');

            // Iterate over the parts and get the field info
            foreach (string part in parts)
            {
                // Handle arrays
                if (part.Contains("["))
                {
                    // Get the field name and index within the array
                    string fieldName = part.Substring(0, part.IndexOf("[", StringComparison.Ordinal));
                    int index = int.Parse(part.Substring(part.IndexOf("[", StringComparison.Ordinal)).Replace("[", "")
                        .Replace("]", ""));
                    field = targetObject.GetType().GetField(fieldName,
                        BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                    if (field != null && field.FieldType.IsArray)
                        targetObject = ((Array)field.GetValue(targetObject)).GetValue(index);
                }
                else
                {
                    field = targetObject.GetType().GetField(part,
                        BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                    if (field != null)
                        targetObject = field.GetValue(targetObject);
                }
            }

            return field;
        }
    }
}