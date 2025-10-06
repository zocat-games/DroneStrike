using Opsive.BehaviorDesigner.Runtime.Tasks;
using Opsive.BehaviorDesigner.Runtime.Tasks.Actions;

namespace Zocat
{
    public class StartAbility : Action
    {
        public AbilityType AbilityType;

        public override void OnStart()
        {
            AbilityManager.Instance.StartAbility(gameObject, AbilityType);
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }
    }
}