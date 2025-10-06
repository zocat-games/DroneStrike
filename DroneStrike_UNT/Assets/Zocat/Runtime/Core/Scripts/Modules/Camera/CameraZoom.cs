using UnityEngine;

// using Unity.Cinemachine;


namespace Zocat
{
    public class CameraZoom : CameraBase
    {
        // private float lerpSpeed = 10f;
        private readonly float maxY = 500f;
        private readonly float minY = 50;
        // private float scrollNormal = .5f;
        private float targetY = 250;
        private float yInterval;
        // private float zoomSpeed = 100f;

        private void Awake()
        {
            yInterval = maxY - minY;
        }

        /*--------------------------------------------------------------------------------------*/

        private void Update()
        {
#if UNITY_EDITOR

            // targetY = cameraManager.ScrollNormal * yInterval + minY;
#endif
            if (Input.touchCount == 2)
            {
                var touch0 = Input.GetTouch(0);
                var touch1 = Input.GetTouch(1);

                var touch0PrevPos = touch0.position - touch0.deltaPosition;
                var touch1PrevPos = touch1.position - touch1.deltaPosition;

                var prevMagnitude = (touch0PrevPos - touch1PrevPos).magnitude;
                var currentMagnitude = (touch0.position - touch1.position).magnitude;

                var difference = currentMagnitude - prevMagnitude;

                targetY -= difference * 0.1f; // hassasiyet
                targetY = Mathf.Clamp(targetY, minY, maxY);
            }

            _camera.PosY(targetY);
        }
    }
}