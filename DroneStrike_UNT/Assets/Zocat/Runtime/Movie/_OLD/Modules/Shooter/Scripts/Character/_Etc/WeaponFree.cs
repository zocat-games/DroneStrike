using Opsive.Shared.Game;
using Opsive.UltimateCharacterController.Traits;
using UnityEngine;

namespace Zocat
{
    public class WeaponFree : InstanceBehaviour
    {
        private Vector3 _initialPosition;
        private Quaternion _initialRotation;
        private Collider _meshCollider;
        private GameObject _owner;
        private Transform _parent;
        private Rigidbody _rb;

        private void Awake()
        {
            _parent = transform.parent;
            _rb = GetComponent<Rigidbody>();
            _meshCollider = GetComponent<Collider>();
            _meshCollider.enabled = false;
            var helth = GetComponentInParent<Health>();
            var respawner = GetComponentInParent<Respawner>();
            respawner.OnRespawnEvent.AddListener(OnRespawn);
            _owner = helth.gameObject;
            _initialPosition = transform.localPosition;
            _initialRotation = transform.localRotation;
            helth.OnDeathEvent.AddListener(OnDeath);
        }

        private void OnDeath(Vector3 position, Vector3 force, GameObject attacker)
        {
            transform.SetParent(null);
            _rb.isKinematic = false;
            _rb.AddForce(Vector3.up * 2 + Vector3.forward * 2f, ForceMode.Impulse);
            _rb.AddTorque(Random.insideUnitSphere * 5, ForceMode.Impulse);
            _meshCollider.enabled = true;
            Scheduler.Schedule(2, () => _rb.isKinematic = true);
        }

        private void OnRespawn()
        {
            gameObject.SetActive(true);
            _rb.isKinematic = true;
            transform.parent = _parent;
            transform.localPosition = _initialPosition;
            transform.localRotation = _initialRotation;
            _meshCollider.enabled = false;
        }
    }
}