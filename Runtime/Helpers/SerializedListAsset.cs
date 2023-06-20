using UnityEngine;

namespace Mane.Inspector
{
    public abstract class SerializedListAsset : ScriptableObject
    {
        [SerializeField] private string[] _list;

        public string[] List => _list;
    }
}