// This feature is based on BinaryCats solution, took from this thread:
// https://forum.unity.com/threads/how-to-change-the-name-of-list-elements-in-the-inspector.448910/
// Thank you.

using UnityEngine;

namespace Mane.Inspector
{
    public class ArrayElementsAttribute : PropertyAttribute
    {
        public string TitleVariableName { get; }

        public ArrayElementsAttribute(string titleVariableName) => TitleVariableName = titleVariableName;
    }
}