// This feature is based on BinaryCats solution, took from this thread:
// https://forum.unity.com/threads/how-to-change-the-name-of-list-elements-in-the-inspector.448910/
// Thank you.

using UnityEngine;

namespace Mane.Inspector
{
    public class ArrayElementsAttribute : PropertyAttribute
    {
        private readonly string _prefix;
        private readonly string _postfix;
        
        public string Prefix => _prefix;
        public string Postfix => _postfix;
        
        public string TitleVariableName { get; }

        public ArrayElementsAttribute(string titleVariableName, string prefix = "", string postfix = "")
        {
            _prefix = prefix;
            _postfix = postfix;
            TitleVariableName = titleVariableName;
        }
    }
}