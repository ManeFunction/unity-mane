using UnityEngine;

namespace Mane
{
    [ExecuteInEditMode]
    public class Area : MonoBehaviour
    {
        [SerializeField] private Vector3 _center;
        [SerializeField] private Vector3 _size;

        
        // pre-cached values to ease calculation in runtime
        private Vector3 _worldCenter;
        private Vector3 _localMin;
        private Vector3 _localMax;
        private Vector3 _globalMin;
        private Vector3 _globalMax;

        
        private void Awake()
        {
            CalculateCache();
        }


        public Vector3 GetLocalPosition()
        {
            return Random.Vector3(_localMin, _localMax);
        }

        public Vector3 GetGlobalPosition()
        {
            return Random.Vector3(_globalMin, _globalMax);
        }
        

        private void CalculateCache()
        {
            _worldCenter = transform.position + _center;
            
            _localMin = _center - _size * .5f;
            _localMax = _center + _size * .5f;

            _globalMin = _worldCenter - _size * .5f;
            _globalMax = _worldCenter + _size * .5f;
        }

#if UNITY_EDITOR
        private void Update()
        {
            CalculateCache();
        }

        private void OnDrawGizmosSelected()
        {
            Color color = Gizmos.color;
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(_worldCenter, _size);
            Gizmos.color = color;
        }
#endif
    }
}
