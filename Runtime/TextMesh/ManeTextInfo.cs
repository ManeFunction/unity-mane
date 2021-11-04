using System.Collections.Generic;
using System.Linq;

public class ManeTextInfo
{
    public readonly List<string> String = new List<string>();
    public readonly List<float> Length = new List<float>();

    public int TotalCount { get { return String.Sum(s => s.Length); } }
    public float MaxLength { get { return Length.Max(s => s); } }

    public void Append(string str, float length)
    {
        String.Add(str);
        Length.Add(length);
    }
}
