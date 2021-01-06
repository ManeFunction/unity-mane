// This feature is based on BinaryCats solution, took from this thread:
// https://forum.unity.com/threads/how-to-change-the-name-of-list-elements-in-the-inspector.448910/
// Thank you.

using UnityEngine;

namespace Mane.Extensions
{
    public class ArrayElementsAttribute : PropertyAttribute
    {
        private string _titleVariableName;

        public string TitleVariableName
        {
            get { return _titleVariableName; }
        }

        public ArrayElementsAttribute(string titleVariableName)
        {
            _titleVariableName = titleVariableName;
        }
    }
}