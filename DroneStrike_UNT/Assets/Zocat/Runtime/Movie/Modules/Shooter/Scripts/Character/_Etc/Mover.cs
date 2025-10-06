using UnityEngine;

namespace Zocat
{
    public class Mover : InstanceBehaviour
    {
        public Transform target;
        private readonly float _moveSpeed = 5f;
        private readonly float _rotationSpeed = 1;

        private readonly float _stopDistance = 0.1f;

        private bool _isMoving;
        public bool Arrived { get; private set; }

        private void Update()
        {
            if (target == null || !_isMoving)
                return;

            // --- Hedefe doğru hareket ---
            var direction = target.position - transform.position;
            var distance = direction.magnitude;

            if (distance <= _stopDistance)
            {
                _isMoving = false;
                Arrived = true;
                return;
            }

            var move = direction.normalized * _moveSpeed * Time.deltaTime;
            transform.position += move;

            // --- Hedef rotasyonuna dön ---
            var targetRotation = target.rotation;
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                _rotationSpeed * Time.deltaTime
            );
        }

        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
            _isMoving = true;
            Arrived = false;
        }

        public void StopMoving()
        {
            _isMoving = false;
        }
    }
}