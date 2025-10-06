using UnityEngine;

namespace Zocat
{
    public class SoldierEventsBase : InstanceBehaviour
    {
        private CharacterControlBase _character;

        protected virtual void Awake()
        {
            _character = GetComponent<CharacterControlBase>();
            _character.Health.OnDeathEvent.AddListener(OnDeath);
        }

        protected virtual void OnDeath(Vector3 position, Vector3 force, GameObject attacker)
        {
            AbilityManager.StartAbility(gameObject, AbilityType.StandDeath);
        }


        protected virtual void OnDeth()
        {
            // _character
        }

        protected virtual void Revive()
        {
        }
        //
        // protected virtual void Reload(bool reloading)
        // {
        // }
    }
}