using Opsive.BehaviorDesigner.Runtime.Tasks;
using Opsive.BehaviorDesigner.Runtime.Tasks.Actions;

namespace Zocat
{
    public class StopAbility : Action
    {
        public AbilityType AbilityType;

        public override void OnStart()
        {
            AbilityManager.Instance.StopAbility(gameObject, AbilityType);
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }
    }
}