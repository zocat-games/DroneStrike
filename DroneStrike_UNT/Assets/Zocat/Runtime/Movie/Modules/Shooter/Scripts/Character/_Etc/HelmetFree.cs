using Opsive.Shared.Events;
using Opsive.Shared.Game;
using Opsive.UltimateCharacterController.Traits;
using UnityEngine;

namespace Zocat
{
    public class HelmetFree : InstanceBehaviour
    {
        public Collider HeadCollider;
        private bool _bulletHit;
        private Collider _helmetCollider;
        private Vector3 _initialPosition;
        private Quaternion _initialRotation;
        private GameObject _owner;
        private Transform _parent;
        private Rigidbody _rb;

        private void Awake()
        {
            _parent = transform.parent;
            _rb = GetComponent<Rigidbody>();
            _helmetCollider = GetComponent<Collider>();
            _helmetCollider.enabled = false;
            var helth = GetComponentInParent<Health>();
            var respawner = GetComponentInParent<Respawner>();
            respawner.OnRespawnEvent.AddListener(OnRespawn);
            _owner = helth.gameObject;
            _initialPosition = transform.localPosition;
            _initialRotation = transform.localRotation;
            EventHandler.RegisterEvent<float, Vector3, Vector3, GameObject, Collider>(_owner, "OnHealthDamage", OnDamage);
        }

        private void OnDamage(float Amount, Vector3 Position, Vector3 ali, GameObject attacker, Collider HitCollider)
        {
            if (_bulletHit) return;
            if (HeadCollider != HitCollider) return;
            _bulletHit = true;
            transform.SetParent(null);
            _rb.isKinematic = false;
            _rb.AddForce(Vector3.up * 5 + Vector3.forward * 2f, ForceMode.Impulse);
            _rb.AddTorque(Random.insideUnitSphere * 10, ForceMode.Impulse);
            _helmetCollider.enabled = true;
            Scheduler.Schedule(3, () => _rb.isKinematic = true);
        }

        private void OnRespawn()
        {
            gameObject.SetActive(true);
            _rb.isKinematic = true;
            transform.parent = _parent;
            transform.localPosition = _initialPosition;
            transform.localRotation = _initialRotation;
            _helmetCollider.enabled = false;
            _bulletHit = false;
        }
    }
}