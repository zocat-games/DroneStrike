// using Iso;

using System;
using UnityEngine;

namespace Zocat
{
    public class CameraManager : MonoSingleton<CameraManager>
    {
        public CameraPivot CameraPivot;
        public ZoomHandler ZoomHandler;
        public Camera ActionCamera;
        public Transform RayTarget;
        public LayerMask LayerMask;
        private float _rayDistance;

        private void Awake()
        {
            // EventHandler.RegisterEvent(EventManager.AfterCreateLevel, AfterCreateLevel);
            _rayDistance = CameraManager.ActionCamera.farClipPlane;
        }

        private void Update()
        {
            if (!LevelManager.LevelExists) return;
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, _rayDistance, LayerMask))
            {
                RayTarget.position = hit.point;
            }
        }
    }
}