using UnityEngine;

namespace Mane.Extensions
{
    public class Aligner : MonoBehaviour
    {
        [Header("Init with")]
        [SerializeField] private Transform _itemPrefab;
        [SerializeField] private int _multiplications;
        
        [Header("Preset")]
        [SerializeField] private float _margin = 1f;
        [SerializeField] private Vector3 _axis = Vector3.right;
        [SerializeField] private Transform[] _items;

        private void OnValidate()
        {
            Align();
        }

        private void Start()
        {
            if (_items.Length > 0 || _itemPrefab == null || _multiplications <= 0) return;

            _items = new Transform[_multiplications];
            _items.FillWith(i => Instantiate(_itemPrefab, transform));
            Align();
        }

        private void Align()
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