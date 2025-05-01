using UnityEngine;

namespace Mane
{
    public class PositionFollower : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField, Range(0f, 1f)] private float _speed = .2f;

        public Transform Target
        {
            get => _target;
            set => _target = value;
        }

        public float Speed
        {
            get => _speed;
            set => _speed = value;
        }

        private void Update()
        {
            if (_target == null)
                return;

            transform.position = Vector3.Lerp(transform.position, _target.position, _speed);
        }
    }
}
