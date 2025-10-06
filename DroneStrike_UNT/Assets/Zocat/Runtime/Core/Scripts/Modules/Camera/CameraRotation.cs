namespace Zocat
{
    public class CameraRotation : CameraBase

    {
        private readonly int maxY = 70;
        private readonly int minY = 30;
        private int interval;

        private void Awake()
        {
            interval = maxY - minY;
        }

        private void Update()
        {
            // var yNormal = cameraManager.ScrollNormal;
            // var targetX = yNormal * interval + minY;
            // var calcX = Mathf.Lerp(_camera.eulerAngles.x, targetX, Time.deltaTime * 10);
            // _camera.RotX(targetX);
        }
    }
}