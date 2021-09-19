using UnityEngine;
using Mane.Extensions;

namespace Mane
{
    [ExecuteInEditMode]
    public class ChildrenTransformFreezer : MonoBehaviour
    {
        [SerializeField] private bool _rotateButKeepGeometry;
        
        private Vector3 _position;
        private Quaternion _rotation;
        private Vector3 _scale;

        private void Awake() => UpdateValues();

        private void Update()
        {
            if (transform.hasChanged)
            {
                Transform[] children = GetComponentsInChildren<Transform>();
                if (Collection.IsNullOrEmpty(children)) return;

                Vector3 localScale = transform.localScale;
                if (localScale.x == 0f || localScale.y == 0f || localScale.z == 0f)
                    return;
                
                Vector3 position = transform.position;
                Vector3 positionShift = position - _position;
                Quaternion rotationShift = _rotation * Quaternion.Inverse(transform.rotation);
                Vector3 scaleShift = localScale.Divide(_scale);
                
                foreach (Transform child in children)
                {
                    if (child == transform) continue;
                    
                    if (!_rotateButKeepGeometry)
                        child.RotateAround(position, rotationShift);
                    child.localScale = child.localScale.Divide(scaleShift);
                    child.localPosition = child.localPosition.Divide(scaleShift);
                    child.position -= positionShift;
                }
            }
            
            UpdateValues();
        }


        private void UpdateValues()
        {
            _position = transform.position;
            _rotation = transform.rotation;
            _scale = transform.localScale;
        }
    }
}
