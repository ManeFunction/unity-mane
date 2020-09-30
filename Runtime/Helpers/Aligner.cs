using UnityEngine;

namespace Mane.Extensions
{
    public class Aligner : MonoBehaviour
    {
        [SerializeField] private float _margin = 1f;
        [SerializeField] private Vector3 _axis = Vector3.right;
        [SerializeField] private Transform[] _items;

        private void OnValidate()
        {
            Vector3 step = _axis * _margin;
            Vector3 startingPosition = step * (-.5f * (_items.Length - 1));
            for (int i = 0; i < _items.Length; i++)
            {
                _items[i].localPosition = startingPosition + step * i;
            }
        }
    }
}