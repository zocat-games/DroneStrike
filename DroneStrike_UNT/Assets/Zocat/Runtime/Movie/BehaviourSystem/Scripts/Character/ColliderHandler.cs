using Opsive.Shared.Events;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Zocat
{
    public class ColliderHandler : InstanceBehaviour
    {
        public CapsuleCollider Capsule;
        private float _height;

        private void Awake()
        {
            EventHandler.RegisterEvent(gameObject, EventManager.Crouch, OnCrouch);
            EventHandler.RegisterEvent(gameObject, EventManager.Stand, OnStand);
            _height = Capsule.height;
        }

        private void OnCrouch()
        {
            Capsule.height = _height / 2;
            Capsule.center = new Vector3(0, Capsule.height / 4, 0);
        }

        private void OnStand()
        {
            Capsule.height = _height;
            Capsule.center = new Vector3(0, Capsule.height / 2, 0);
        }

        [Button(ButtonSizes.Medium)]
        public void stand()
        {
            OnStand();
        }

        [Button(ButtonSizes.Medium)]
        public void Test()
        {
            OnCrouch();
        }
    }
}