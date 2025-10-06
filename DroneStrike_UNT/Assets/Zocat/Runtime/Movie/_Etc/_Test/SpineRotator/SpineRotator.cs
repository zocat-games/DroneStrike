using UnityEngine;

namespace Zocat
{
    public class SpineRotator : InstanceBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _spine;

        [SerializeField] private Vector3 _rotationOffset;

        public Transform target;

        private void LateUpdate()
        {
            var rotation = Quaternion.LookRotation(target.position - _spine.position).eulerAngles + _rotationOffset;
            if (_spine != null) _spine.localEulerAngles = new Vector3(_spine.localEulerAngles.x, _spine.localEulerAngles.y, -rotation.x);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(_spine.position, target.position);
        }
    }
}