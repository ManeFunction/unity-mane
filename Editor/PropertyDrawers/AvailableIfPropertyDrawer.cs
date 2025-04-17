using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Mane.Inspector.Editor
{
    [CustomPropertyDrawer(typeof(AvailableIfAttribute))]
    public class AvailableIfPropertyDrawer : PropertyDrawer
    {
        private static readonly Dictionary<(Type type, string fieldName), FieldInfo> FieldCache = new();
        private static readonly Dictionary<(Type type, string memberName), MemberInfo> MemberCache = new();

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var attr = ProcessAttribute(property);

            if (!attr.isAvailable && attr.hide) return 0f;
            
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var attr = ProcessAttribute(property);

            if (!attr.isAvailable)
            {
                if (attr.hide) return;
                
                GUI.enabled = false;
            }

            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }

        private (bool isAvailable, bool hide) ProcessAttribute(SerializedProperty property)
        {
            AvailableIfAttribute attr = attribute as AvailableIfAttribute;

            bool isAvailable = attr.IsAvailable;

            if (!string.IsNullOrEmpty(attr.PropertyName))
            {
                SerializedProperty attachedProperty =
                    property.serializedObject.FindProperty(attr.PropertyName);
                if (attachedProperty == null)
                {
                    object target = GetTargetObject(property);
                    Type type = target.GetType();
                    
                    var cacheKey = (type, attr.PropertyName);
                    if (!MemberCache.TryGetValue(cacheKey, out var member))
                    {
                        member = type.GetMethod(attr.PropertyName) ?? 
                                (MemberInfo)type.GetProperty(attr.PropertyName)?.GetMethod;
                        if (member != null)
                            MemberCache[cacheKey] = member;
                    }
                    
                    if (member != null)
                        isAvailable = (bool)((member is MethodInfo method ? method : ((PropertyInfo)member).GetMethod).Invoke(target, null));
                    else
                        Debug.LogError($"AvailableIf: Can't find {attr.PropertyName} in {type}");
                }
                else
                    isAvailable = !attachedProperty.IsPropertyDefault();
            }
            
            if (attr.Invert)
                isAvailable = !isAvailable;

            return (isAvailable, attr.Hide);
        }

        private object GetTargetObject(SerializedProperty property)
        {
            string path = property.propertyPath;
            object obj = property.serializedObject.targetObject;
            
            if (string.IsNullOrEmpty(path)) return obj;

            string parentPath = path[..path.LastIndexOf('.')];
            if (string.IsNullOrEmpty(parentPath)) return obj;

            SerializedProperty parentProperty = property.serializedObject.FindProperty(parentPath);
            if (parentProperty != null)
            {
                string[] elements = parentPath.Split('.');
                for (int i = 0; i < elements.Length; i++)
                {
                    if (elements[i] == "Array")
                    {
                        if (elements[i + 1].StartsWith("data["))
                        {
                            int index = Convert.ToInt32(elements[i + 1][5..^1]);
                            obj = ((System.Collections.IList)obj)[index];
                        }
                    }
                    else
                    {
                        var cacheKey = (obj.GetType(), elements[i]);
                        if (!FieldCache.TryGetValue(cacheKey, out var field))
                        {
                            field = obj.GetType().GetField(elements[i], BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                            if (field != null)
                                FieldCache[cacheKey] = field;
                        }
                        
                        if (field != null)
                            obj = field.GetValue(obj);
                    }
                }
            }
            return obj;
        }
    }
}