using Opsive.Shared.Events;
using UnityEngine;

namespace Zocat
{
    public class EnemyEvents : EventsBase
    {
        public Transform ShootTarget;
        protected EnemyBase _enemyBase;


        protected virtual void Awake()
        {
            base.Awake();
            _enemyBase = GetComponent<EnemyBase>();
            ShootTarget.transform.parent = null;
        }

        protected virtual void OnDeth()
        {
            EventHandler.ExecuteEvent(EventManager.EnemyDeath, _enemyBase);
        }

        public override void Revive()
        {
            base.Revive();
            _enemyBase.OnTheFront = true;
        }

        public virtual void Alarm()
        {
            EventHandler.ExecuteEvent(_enemyBase.BehaviorTree, "Alarm");
        }

        public void SetShootTarget()
        {
            ShootTarget.transform.position = HeroManager.HeroMain.TargetForEnemy.position;
            transform.DoLookY(HeroManager.HeroMain.TargetForEnemy, .5f);
        }
    }
}