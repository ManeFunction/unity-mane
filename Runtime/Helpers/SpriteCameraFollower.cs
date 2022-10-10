using UnityEngine;


namespace Mane
{
    public class SpriteCameraFollower : MonoBehaviour
    {
        private Camera _camera;
        
        protected virtual Camera Camera
        {
            get
            {
                if (!_camera)
                    _camera = Camera.main;
                
                return _camera;
            }
        }
        
        private void Update() =>
            transform.LookAt(Camera.transform.position, -Vector3.up);
    }
}