using System;

namespace Mane.Inspector
{
    public class EnumArrayOddsAttribute : EnumArrayAttribute
    {
        public string TotalWeightProperty { get; private set; }

        public EnumArrayOddsAttribute(Type enumType, string totalWeightProperty) : base(enumType)
        {
            TotalWeightProperty = totalWeightProperty;
        }
    }
}