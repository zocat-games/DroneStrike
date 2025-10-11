using Opsive.BehaviorDesigner.Runtime;
using Opsive.Shared.Events;
using Sirenix.OdinInspector;

namespace Zocat
{
    public class EventTest : InstanceBehaviour
    {
        public BehaviorTree Bt;
        public int Ali { get; set; }


        [Button(ButtonSizes.Medium)]
        public void Olay0()
        {
            Ali = 101;
            EventHandler.ExecuteEvent<object>(Bt, "Olay0", 99);
            // var behaviorTree = GetComponent<BehaviorTree>();
            // EventHandler.ExecuteEvent<object>(behaviorTree, "MyEvent", 5);
        }

        [Button(ButtonSizes.Medium)]
        public void Olay1()
        {
            // EventHandler.ExecuteEvent<object>(Bt, "Olay1", 1);
            // var behaviorTree = GetComponent<BehaviorTree>();
            // EventHandler.ExecuteEvent<object>(behaviorTree, "MyEvent", 5);
        }
    }
}