using UnityEngine;

public class SphereGizmo : MonoBehaviour
{
    [SerializeField] private float _size = .1f;
    [SerializeField] private Color _color = Color.magenta;
    
    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Color color = Gizmos.color;
        Gizmos.color = _color;
        Gizmos.DrawSphere(transform.position, _size);
        Gizmos.color = color;
    }
    #else
    private void Awake() => Destroy(this);
    #endif
}
