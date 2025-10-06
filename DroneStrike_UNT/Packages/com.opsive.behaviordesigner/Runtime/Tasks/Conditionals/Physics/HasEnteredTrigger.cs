#if GRAPH_DESIGNER
/// ---------------------------------------------
/// Behavior Designer
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------
namespace Opsive.BehaviorDesigner.Runtime.Tasks.Conditionals.Physics
{
    using Opsive.GraphDesigner.Runtime.Variables;
    using UnityEngine;

    [Opsive.Shared.Utility.Description("Returns success when an object enters the trigger. This task will only receive the physics callback if it is being reevaluated (with a conditional abort or under a parallel task).")]
    [Shared.Utility.Category("Physics")]
    public class HasEnteredTrigger : Conditional
    {
        [Tooltip("The tag of the GameObject that the trigger should be checked against.")]
        [SerializeField] protected SharedVariable<string> m_Tag;
        [Tooltip("The entered trigger.")]
        [SerializeField] protected SharedVariable<Collider> m_StoredOtherCollider;

        protected override bool ReceiveTriggerEnterCallback => true;

        private bool m_EnteredTrigger;

        /// <summary>
        /// Returns true when the agent has entered a trigger.
        /// </summary>
        /// <returns>True when the agent has entered a trigger.</returns>
        public override TaskStatus OnUpdate()
        {
            return m_EnteredTrigger ? TaskStatus.Success : TaskStatus.Failure;
        }

        /// <summary>
        /// The task has ended.
        /// </summary>
        public override void OnEnd()
        {
            base.OnEnd();

            m_EnteredTrigger = false;
        }

        /// <summary>
        /// The agent has entered a trigger.
        /// </summary>
        /// <param name="other">The trigger that the agent entered.</param>
        protected override void OnTriggerEnter(Collider other)
        {
            if (!string.IsNullOrEmpty(m_Tag.Value) && !other.gameObject.CompareTag(m_Tag.Value)) {
                return;
            }

            if (m_StoredOtherCollider != null && m_StoredOtherCollider.IsShared) { m_StoredOtherCollider.Value = other; }

            m_EnteredTrigger = true;
        }
    }
}
#endif