#if GRAPH_DESIGNER
/// ---------------------------------------------
/// Behavior Designer
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------
namespace Opsive.BehaviorDesigner.Runtime.Tasks.Conditionals.Physics
{
    using Opsive.GraphDesigner.Runtime;
    using Opsive.GraphDesigner.Runtime.Variables;
    using UnityEngine;

    [Opsive.Shared.Utility.Description("Returns success when a 2D collision ends. This task will only receive the physics callback if it is being reevaluated (with a conditional abort or under a parallel task).")]
    [Shared.Utility.Category("Physics")]
    public class HasExitedCollision2D : Conditional
    {
        [Tooltip("The tag of the GameObject that the collision should be checked against.")]
        [SerializeField] protected SharedVariable<string> m_Tag;
        [Tooltip("The collided GameObject.")]
        [SerializeField] protected SharedVariable<GameObject> m_StoredCollisionGameObject;

        protected override bool ReceiveCollisionExit2DCallback => true;

        private bool m_ExitedCollision;

        /// <summary>
        /// Returns true when the agent has left a collision.
        /// </summary>
        /// <returns>True when the agent has left a collision.</returns>
        public override TaskStatus OnUpdate()
        {
            return m_ExitedCollision ? TaskStatus.Success : TaskStatus.Failure;
        }

        /// <summary>
        /// The task has ended.
        /// </summary>
        public override void OnEnd()
        {
            base.OnEnd();

            m_ExitedCollision = false;
        }

        /// <summary>
        /// The agent has left a collision.
        /// </summary>
        /// <param name="collision">The collision that caused the event.</param>
        protected override void OnCollisionExit2D(Collision2D collision)
        {
            if (!string.IsNullOrEmpty(m_Tag.Value) && !collision.gameObject.CompareTag(m_Tag.Value)) {
                return;
            }

            if (m_StoredCollisionGameObject != null && m_StoredCollisionGameObject.IsShared) { m_StoredCollisionGameObject.Value = collision.gameObject; }

            m_ExitedCollision = true;
        }
    }
}
#endif