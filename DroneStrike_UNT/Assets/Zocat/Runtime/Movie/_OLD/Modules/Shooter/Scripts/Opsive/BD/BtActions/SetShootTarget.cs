using Opsive.BehaviorDesigner.Runtime.Tasks;
using Opsive.BehaviorDesigner.Runtime.Tasks.Actions;
using Opsive.Shared.Game;

namespace Zocat
{
    public class SetShootTarget : Action
    {
        public override void OnStart()
        {
            gameObject.GetCachedComponent<EnemyEvents>().SetShootTarget();
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }
    }
}