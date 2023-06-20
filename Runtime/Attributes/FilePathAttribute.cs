using System;

namespace Mane.Inspector
{
    [AttributeUsage(AttributeTargets.Class)]
    public class FilePathAttribute : Attribute
    {
        public string Path { get; }
        
        public FilePathAttribute(string path) => Path = $"Assets/{path}";
    }
}