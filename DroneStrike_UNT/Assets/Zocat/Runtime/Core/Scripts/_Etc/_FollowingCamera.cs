using UnityEngine;

namespace Zocat
{
    public class _FollowingCamera : InstanceBehaviour
    {
        public float followSpeed = 5f;

        private Vector3 offset;
        private Transform target;

        private void Update()
        {
            if (target == null) return;
            var targetPosition = target.position + offset;
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.transform.position, targetPosition, .2f);
        }

        public void Initialize(Transform _target, Vector3 _offset)
        {
            target = _target;
            offset = _offset;
        }
    }
}