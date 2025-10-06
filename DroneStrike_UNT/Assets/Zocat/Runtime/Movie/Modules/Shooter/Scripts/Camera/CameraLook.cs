using DG.Tweening;
using Opsive.Shared.Events;
using UnityEngine;

namespace Zocat
{
    public class CameraLook : InstanceBehaviour
    {
        private float _pitch;
        private float _yaw;
        private readonly float _sens = 50;
        private readonly Vector2 PitchRange = new(0, 90); // y aşağı doğru +, x yukarı doğru - olmalı
        // private readonly Vector2 YawRange = new(-180, 180);
        private float mx;
        private float my;
        private bool _initialized;

        private void Awake()
        {
            EventHandler.RegisterEvent(EventManager.AfterCompleteLevel, AfterCompleteLevel);
        }

        private void AfterCompleteLevel()
        {
            _initialized = false;
        }

        private void Update()
        {
            if (!InputManager.MouseRotationEnabled) return;
            if (!_initialized) return;
            mx = Input.GetAxis("Mouse X") * _sens * Time.deltaTime;
            my = Input.GetAxis("Mouse Y") * -_sens * Time.deltaTime;
            _yaw += mx;
            _pitch += my;
            _pitch = TransformTools.ClampAngleAroundZero(_pitch, PitchRange.x, PitchRange.y);
            // _yaw = TransformTools.ClampAngleAroundZero(_yaw, YawRange.x, YawRange.y);
            transform.localEulerAngles = new Vector3(_pitch, _yaw, 0);
        }

        public void Initialize(Transform cameraPivot)
        {
            transform.LookAt(cameraPivot);
            _yaw = transform.localEulerAngles.y;
            _pitch = transform.localEulerAngles.x;
            _initialized = true;
        }
    }
}