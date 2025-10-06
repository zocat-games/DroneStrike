using System.Reflection;
using Opsive.Shared.Events;
using Opsive.UltimateCharacterController.Traits;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Zocat
{
    public class LifeCycle : InstanceBehaviour
    {
        public Health health;
        private GameObject _owner;
        private Respawner _respawner;

        private void Awake()
        {
            _owner = transform.parent.gameObject;
            _respawner = transform.parent.GetComponent<Respawner>();
            EventHandler.RegisterEvent<Vector3, Vector3, GameObject>(gameObject, EventManager.OnDeath, OnDeth);
        }

        public void Revive()
        {
        }

        public void OnDeth(Vector3 position, Vector3 force, GameObject attacker)
        {
            IsoHelper.Log(this, MethodBase.GetCurrentMethod().Name);
        }

        [Button(ButtonSizes.Medium)]
        public void Test()
        {
            health.Damage(40);
        }
    }
}