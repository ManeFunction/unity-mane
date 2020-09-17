namespace Mane.Extensions
{
    public static class Random
    {
        public static int ThrowDice(int d = 6)
        {
            return (int)(UnityEngine.Random.Range(0f, 1f) / (1f / d));
        }

        public static bool FlipCoin()
        {
            return UnityEngine.Random.Range(0f, 1f) < .5f;
        }
    }
}