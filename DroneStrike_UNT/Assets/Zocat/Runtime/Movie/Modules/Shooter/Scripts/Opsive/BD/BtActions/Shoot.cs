using Opsive.BehaviorDesigner.Runtime.Tasks;
using Opsive.BehaviorDesigner.Runtime.Tasks.Actions;
using Opsive.GraphDesigner.Runtime.Variables;

namespace Zocat
{
    public class Shoot : Action
    {
        public SharedVariable<bool> Start;

        public override void OnStart()
        {
            if (Start.Value)
                AbilityManager.Instance.StartAbility(gameObject, AbilityType.Shoot);
            else
                AbilityManager.Instance.StopAbility(gameObject, AbilityType.Shoot);
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }
    }
}