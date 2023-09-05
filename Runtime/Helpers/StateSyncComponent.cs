using Mane.Extensions;
using UnityEngine;

namespace Mane
{
    public class StateSyncComponent : MonoBehaviour
    {
        [SerializeField] private GameObject[] _bind;

        private void OnEnable() => _bind.ForEach(b => b.SetActive(true));

        private void OnDisable() => _bind.ForEach(b => b.SetActive(false));
    }
}