namespace Mane
{
    public static class MMath
    {
        public static int RoundUp(this float value) => 
            (value == (int)value) ? (int)value : (int)value + 1;

        public static int RoundDown(this float value) => (int)value;
    }
}
