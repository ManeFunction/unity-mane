using UnityEngine;


namespace Mane
{
    [ExecuteAlways]
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
        
        private void Update()
        {
            Camera cam = Camera;
            if (!cam) return;
        
            transform.LookAt(cam.transform.position, -Vector3.up);
        }
    }
}