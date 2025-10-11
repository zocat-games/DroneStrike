using Opsive.BehaviorDesigner.Runtime;

namespace Zocat
{
    public class EnemyBase : InstanceBehaviour
    {
        public SubEnemyType SubEnemyType;
        public EnemyWeapon EnemyWeapon;
        public BehaviorTree BehaviorTree;
        public EnemyEvents EnemyEvents;
        public UnitBase UnitBase;
        public bool OnTheFront { get; set; }
    }
}