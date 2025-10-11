using Opsive.BehaviorDesigner.Runtime.Tasks;
using Opsive.BehaviorDesigner.Runtime.Tasks.Actions;
using Opsive.GraphDesigner.Runtime.Variables;

namespace Zocat
{
    public class AbilityBool : Action
    {
        public SharedVariable<bool> bl;

        public override void OnStart()
        {
        }

        public override TaskStatus OnUpdate()
        {
            IsoHelper.Log(bl.Value);
            return bl.Value ? TaskStatus.Success : TaskStatus.Running;
        }
    }
}