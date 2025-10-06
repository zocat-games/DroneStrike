// namespace Zocat
// {
//     public class EnemySoldierEvents : SoldierEventsBase
//     {
//         // private AimIK _aimIk;
//         // private BehaviorTree _behaviorTree;
//         // private Soldier _owner;
//         // private ObjectIdentifier[] ObjectIdentifiers;
//         // private bool _death => !_owner.Health.IsAlive();
//
//         protected override void Awake()
//         {
//             // base.Awake();
//             // _behaviorTree = GetComponent<BehaviorTree>();
//             // _owner = GetComponent<Soldier>();
//             // _aimIk = GetComponentInChildren<AimIK>();
//             // AbilityManager.StartAbility(gameObject, AbilityType.ThomsonIdle);
//             // EventHandler.RegisterEvent<AbilityType>(gameObject, EventManager.AbilityStarted, OnAbilityStarted);
//             // EventHandler.RegisterEvent<AbilityType>(gameObject, EventManager.AbilityStoped, OnAbilityStoped);
//             // ObjectIdentifiers = GetComponentsInChildren<ObjectIdentifier>();
//         }
//
//
//         /*---------------------------------EVENTS RECEIVED-----------------------------------------------------*/
//         private void OnAbilityStarted(AbilityType abilityType)
//         {
//             // switch (abilityType)
//             // {
//             //     case AbilityType.ThomsonAim: ThomsonAim(); break;
//             //     case AbilityType.ThomsonIdle: ThomsonIdle(); break;
//             //     case AbilityType.Shoot: SetShoot(true); break;
//             //     case AbilityType.StandDeath: OnDeth(); break;
//             // }
//         }
//
//         private void OnAbilityStoped(AbilityType abilityType)
//         {
//             // switch (abilityType)
//             // {
//             //     case AbilityType.Shoot: SetShoot(false); break;
//             // }
//         }
//
//         private void OnDeth()
//         {
//             // _owner.OnTheFront = false;
//             // _aimIk.DoAimWeight(0, 0.5f);
//             // _owner.EnemyWeapon.SetShoot(false);
//             // EventHandler.ExecuteEvent(_behaviorTree, "Death");
//             // Scheduler.Schedule(2, () => gameObject.SetActive(false));
//             // _owner.AudioSource.PlayOneShot(ShooterAudioManager.RandomAudioClip(ShooterAudioType.Hurt));
//         }
//
//         private void ThomsonIdle()
//         {
//             // _aimIk.DoAimWeight(0, 0.5f);
//         }
//
//         private void ThomsonAim()
//         {
//             // _aimIk.DoAimWeight(1, 0.5f);
//         }
//
//         private void SetShoot(bool shooting)
//         {
//             // if (_death) return;
//             // AbilityManager.StartAbility(gameObject, !shooting ? AbilityType.ThomsonIdle : AbilityType.ThomsonAim);
//             // _owner.EnemyWeapon.SetShoot(shooting);
//         }
//
//         /*--------------------------------EVENTS SEND------------------------------------------------------*/
//
//         public void Alarm()
//         {
//             // EventHandler.ExecuteEvent(_behaviorTree, "Alarm");
//         }
//
//         public void Revive()
//         {
//             // _owner.OnTheFront = true;
//             // gameObject.SetActive(true);
//             // _owner.Respawner.Respawn();
//             // AbilityManager.StartAbility(_owner.gameObject, AbilityType.ThomsonIdle);
//         }
//     }
// }

