using UnityEngine;

namespace Zocat
{
    public class EventsBase : InstanceBehaviour
    {
        public UnitBase UnitBase;

        protected virtual void Awake()
        {
            UnitBase.Health.OnDeathEvent.AddListener(OnDeath);
        }

        protected virtual void OnDeath(Vector3 position, Vector3 force, GameObject attacker)
        {
            AbilityManager.StartAbility(gameObject, AbilityType.StandDeath);
        }

        public virtual void Revive()
        {
            gameObject.SetActive(true);
            UnitBase.Respawner.Respawn();
        }
    }
}