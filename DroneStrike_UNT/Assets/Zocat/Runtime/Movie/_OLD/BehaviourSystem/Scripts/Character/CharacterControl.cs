using Opsive.UltimateCharacterController.Traits;
using UnityEngine;

namespace Zocat
{
    public class CharacterControlBase : InstanceBehaviour
    {
        public Health Health;
        public Respawner Respawner;
        public Animator Animator;
        public AudioSource AudioSource;

        protected virtual void Awake()
        {
        }
        //
        // protected virtual void Reload(bool reloading)
        // {
        // }
        //
        // protected virtual void OnDeath(Vector3 position, Vector3 force, GameObject attacker)
        // {
        //     AbilityManager.StartAbility(this, AbilityType.StandDeath);
        //     AudioSource.PlayOneShot(ShooterAudioManager.RandomAudioClip(ShooterAudioType.Hurt));
        // }

        // private void OnDeth(Vector3 position, Vector3 force, GameObject attacker)
        // {
        //   
        // }
        // public virtual void Respawn()
        // {
        //     AbilityManager.StartAbility(this, AbilityType.StandIdle, 0);
        //     Animator.transform.ResetLocalEuler();
        // }
    }
}