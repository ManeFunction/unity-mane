using UnityEngine;

namespace Mane
{
    public class SphereGizmo : MonoBehaviour
    {
#pragma warning disable CS0414
        [SerializeField] private float _size = .1f;
        [SerializeField] private Color _color = Color.magenta;
#pragma warning restore CS0414

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Color color = Gizmos.color;
            Gizmos.color = _color;
            Gizmos.DrawSphere(transform.position, _size);
            Gizmos.color = color;
        }
#else
    private void Awake() { Destroy(this); }
#endif
    }
}