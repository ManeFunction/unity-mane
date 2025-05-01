using UnityEngine;

namespace Mane
{
    public class PositionFollower : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField, Range(0f, 1f)] private float _speed = 1f;

        private void Update()
        {
            transform.position = Vector3.Lerp(transform.position, _target.position, _speed);
        }
    }
}
