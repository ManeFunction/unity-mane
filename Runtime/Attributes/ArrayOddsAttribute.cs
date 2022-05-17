using UnityEngine;

namespace Mane.Inspector
{
    public class ArrayOddsAttribute : PropertyAttribute
    {
        public string TotalWeightProperty { get; }

        public ArrayOddsAttribute(string totalWeightProperty) => 
            TotalWeightProperty = totalWeightProperty;
    }
}