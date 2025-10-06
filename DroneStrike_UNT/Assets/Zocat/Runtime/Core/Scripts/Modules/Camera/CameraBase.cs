using UnityEngine;

namespace Zocat
{
    public class CameraBase : InstanceBehaviour
    {
        protected CameraManager cameraManager => CameraManager.Instance;
        protected Transform _camera => Camera.main.transform;

        // protected void Awake()
        // {
        //     _cameraManager = CameraManager.Instance;
        // }
    }
}