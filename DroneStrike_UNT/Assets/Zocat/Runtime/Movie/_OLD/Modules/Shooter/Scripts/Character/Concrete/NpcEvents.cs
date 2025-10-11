using Opsive.Shared.Game;
using Opsive.UltimateCharacterController.Objects;
using RootMotion.FinalIK;
using UnityEngine;
using EventHandler = Opsive.Shared.Events.EventHandler;

namespace Zocat
{
    public class NpcEvents : EnemyEvents
    {
        public AimIK AimIK;
        private CharacterBase _characterBase;
        private AbilityType _currentAbility;
        private EnemySoldier _enemySoldier;
        private ObjectIdentifier[] ObjectIdentifiers;

        protected override void Awake()
        {
            base.Awake();
            _enemySoldier = GetComponent<EnemySoldier>();
            _characterBase = GetComponent<CharacterBase>();
            UnitBase.Health.OnDamageEvent.AddListener(OnDamage);
            EventHandler.RegisterEvent<AbilityType>(gameObject, EventManager.AbilityStarted, OnAbilityStarted);
            EventHandler.RegisterEvent<AbilityType>(gameObject, EventManager.AbilityStoped, OnAbilityStoped);
            ObjectIdentifiers = GetComponentsInChildren<ObjectIdentifier>();
        }

        private void OnDisable()
        {
            if (EventManager.ApplicationQuit) return;
            ObjectIdentifiers.SetActiveAll(false);
        }

        /*--------------------------------------------------------------------------------------*/
        private void OnAbilityStarted(AbilityType abilityType)
        {
            _currentAbility = abilityType;
            switch (abilityType)
            {
                case AbilityType.ThomsonAim: ThomsonAim(); break;
                case AbilityType.ThomsonIdle: ThomsonIdle(); break;
                case AbilityType.Shoot: SetShoot(true); break;
                case AbilityType.StandDeath: OnDeth(); break;
            }
        }

        private void OnAbilityStoped(AbilityType abilityType)
        {
            switch (abilityType)
            {
                case AbilityType.Shoot: SetShoot(false); break;
            }
        }

        /*----------------------------- RECEIVED ---------------------------------------------------------*/
        private void OnDamage(float damage, Vector3 a, Vector3 b, GameObject attacker)
        {
            if (!_enemySoldier.Health.IsAlive()) return;
            _characterBase.Animator.SetLayerWeight(1, 1);
            AbilityManager.PlayOneShot(gameObject, _currentAbility, AbilityType.HitUpper);
            Scheduler.Schedule(.5f, () => { _characterBase.Animator.SetLayerWeight(1, 0); });
        }

        protected override void OnDeth()
        {
            base.OnDeth();
            AimIK.DoAimWeight(0, 0.5f);
            _enemyBase.EnemyWeapon.SetShoot(false);
            EventHandler.ExecuteEvent(_enemySoldier.EnemyBase.BehaviorTree, "Death");
            Scheduler.Schedule(5, () =>
            {
                gameObject.SetActive(false);
                _enemyBase.OnTheFront = false;
            });
            _enemySoldier.AudioSource.PlayOneShot(ShooterAudioManager.RandomAudioClip(ShooterAudioType.Hurt));
        }

        private void ThomsonIdle()
        {
            AimIK.DoAimWeight(0, 0.5f);
        }

        private void ThomsonAim()
        {
            AimIK.DoAimWeight(1, 0.5f);
        }

        private void SetShoot(bool shooting)
        {
            if (!_enemySoldier.Health.IsAlive()) return;
            _enemyBase.EnemyWeapon.SetShoot(shooting);
        }

        /*--------------------------------SEND------------------------------------------------------*/

        public override void Revive()
        {
            base.Revive();
            AbilityManager.StartAbility(UnitBase.gameObject, AbilityType.ThomsonIdle);
        }

        /*--------------------------------------------------------------------------------------*/
        // [Button(ButtonSizes.Medium)]
        // public void Damage()
        // {
        //     _enemySoldier.Health.Damage(60);
        // }
        //
        // [Button(ButtonSizes.Medium)]
        // public void TestRevive()
        // {
        //     Revive();
        // }
        //
        // [Button(ButtonSizes.Medium)]
        // public void TestAlarm()
        // {
        //     Alarm();
        // }
    }
}