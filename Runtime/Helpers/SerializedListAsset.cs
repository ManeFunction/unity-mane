using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Mane.Inspector
{
    public abstract class SerializedListAsset : ScriptableObject
    {
        [SerializeField] private string[] _list;

        public string[] List => _list;
        
        
        private static string _resourcePath;
        
        private static string GetResourcePath<T>() where T : SerializedListAsset
        {
            if (_resourcePath == null)
            {
                FilePathAttribute filePathAttribute = typeof(T).GetCustomAttribute<FilePathAttribute>();
                if (filePathAttribute != null)
                {
                    string path = filePathAttribute.Path;
                    int startIndex = path.IndexOf("Resources", StringComparison.Ordinal) + 10;
                    _resourcePath = path.Substring(startIndex, path.Length - startIndex - 6);
                }
            }
            return _resourcePath;
        }
        
        
        protected static T GetInstance<T>() where T : SerializedListAsset
        {
            T list = Resources.Load<T>(GetResourcePath<T>());
            if (list == null)
            {
                Debug.LogError($"List {typeof(T).Name} not found! Place it in Resources folder for runtime usage!");
                return null;
            }
            return list;
        }
        
        public static IReadOnlyCollection<string> FetchList<T>() where T : SerializedListAsset
        {
            T list = GetInstance<T>();
            return list == null ? null : list.List;
        }

        public static int Count<T>() where T : SerializedListAsset
        {
            T list = GetInstance<T>();
            return list == null ? -1 : list.List.Length;
        }
    }
}