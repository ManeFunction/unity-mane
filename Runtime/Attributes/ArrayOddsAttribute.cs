using UnityEngine;

namespace Mane.Inspector
{
    public class ArrayOddsAttribute : PropertyAttribute
    {
        public string TotalWeightProperty { get; }
        
        public string CustomLabel { get; }
        
        public bool UseNumeration { get; }
        
        public bool IsHumanReadableNumeration { get; }

        public ArrayOddsAttribute(string totalWeightProperty, string customLabel = default, 
            bool useNumeration = true, bool isHumanReadableNumeration = true)
        {
            TotalWeightProperty = totalWeightProperty;
            CustomLabel = customLabel;
            UseNumeration = useNumeration;
            IsHumanReadableNumeration = isHumanReadableNumeration;
        }
    }
}