using UnityEngine;

namespace Mane
{
    public class ManeBehaviour : MonoBehaviour
    {
        private Transform _transform;
        
        public new Transform transform => _transform;

        protected virtual void Awake() => _transform = base.transform;
    }
}
