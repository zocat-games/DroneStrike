using UnityEngine;

namespace Zocat
{
    [CreateAssetMenu(fileName = "CameraStateSo", menuName = "Zocat/CameraStateSo", order = 1)]
    public class CameraStateSo : CustomScriptable
    {
        public Vector3 CameraPosition;
        public Vector3 CameraRotation;
    }
}