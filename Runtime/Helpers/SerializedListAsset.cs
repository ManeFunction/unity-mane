using UnityEditor;
using UnityEngine;

namespace Mane.Inspector
{
    public abstract class SerializedListAsset : ScriptableSingleton<SerializedListAsset>
    {
        [SerializeField] private string[] _list;

        public static string[] List => instance._list;
    }
}