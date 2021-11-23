using UnityEngine;
using Mane.Extensions;
using UnityEngine.AI;

namespace Mane
{
    [ExecuteInEditMode]
    public class ChildrenTransformFreezer : MonoBehaviour
    {
        [SerializeField] private bool _affectDisabled;
        [SerializeField] private bool _keepColliders;
        [SerializeField] private bool _rotateButKeepGeometry;

        private Vector3 _localPosition;
        private Vector3 _position;
        private Quaternion _rotation;
        private Vector3 _scale;

        
        private void Awake() => UpdateValues();

        private void Update()
        {
            if (transform.hasChanged)
            {
                Transform[] children = GetComponentsInChildren<Transform>(_affectDisabled);
                if (Collection.IsNullOrEmpty(children))
                    return;

                Vector3 localScale = transform.localScale;
                if (localScale.x == 0f || localScale.y == 0f || localScale.z == 0f)
                    return;
                
                Vector3 position = transform.position;
                Vector3 positionShift = position - _position;
                Quaternion rotationShift = _rotation * Quaternion.Inverse(transform.rotation);
                Vector3 scaleShift = localScale.Divide(_scale);
                
                foreach (Transform child in children)
                {
                    if (child == transform || child.parent != transform)
                        continue;
                    
                    if (!_rotateButKeepGeometry)
                        child.RotateAround(position, rotationShift);
                    child.localScale = child.localScale.Divide(scaleShift);
                    child.localPosition = child.localPosition.Divide(scaleShift);
                    child.position -= positionShift;
                }

                if (_keepColliders)
                {
                    Collider[] colliders = gameObject.GetComponents<Collider>();
                    NavMeshObstacle[] obstacles = gameObject.GetComponents<NavMeshObstacle>();
                    bool hasColliders = !Collection.IsNullOrEmpty(colliders);
                    bool hasObstacles = !Collection.IsNullOrEmpty(obstacles);

                    Vector3 localPositionShift = default;
                    if (hasColliders || hasObstacles)
                    {
                        Quaternion localRotationShift = Quaternion.Inverse(transform.localRotation);
                        localPositionShift = localRotationShift * (transform.localPosition - _localPosition);
                    }
                    
                    if (hasColliders)
                    {
                        foreach (Collider c in colliders)
                        {
                            if (c is BoxCollider box)
                                box.center -= localPositionShift;
                            else if (c is SphereCollider sphere)
                                sphere.center -= localPositionShift;
                            else if (c is CapsuleCollider capsule)
                                capsule.center -= localPositionShift;
                        }
                    }

                    if (hasObstacles)
                    {
                        foreach (NavMeshObstacle o in obstacles)
                            o.center -= localPositionShift;
                    }
                }
            }
            
            UpdateValues();
        }


        private void UpdateValues()
        {
            _localPosition = transform.localPosition;
            _position = transform.position;
            _rotation = transform.rotation;
            _scale = transform.localScale;
        }
    }
}
