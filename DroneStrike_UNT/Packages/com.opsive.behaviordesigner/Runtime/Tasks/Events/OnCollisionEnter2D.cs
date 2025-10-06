#if GRAPH_DESIGNER
/// ---------------------------------------------
/// Behavior Designer
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------
namespace Opsive.BehaviorDesigner.Runtime.Tasks.Events
{
    using Opsive.GraphDesigner.Runtime;
    using Opsive.GraphDesigner.Runtime.Variables;
    using UnityEngine;

    [AllowMultipleTypes]
    [NodeIcon("06864c37115f11445b04701c616d0e14", "8b8a2793322238240b4f25171d772003")]
    [Opsive.Shared.Utility.Description("Invoked when the agent causes a 2D collision.")]
    public class OnCollisionEnter2D : EventNode
    {
        [Tooltip("The tag of the GameObject that the collision should be checked against.")]
        [SerializeField] protected SharedVariable<string> m_Tag;
        [Tooltip("The collided GameObject.")]
        [SerializeField] protected SharedVariable<GameObject> m_StoredCollisionGameObject;

        /// <summary>
        /// Initializes the node to the specified graph.
        /// </summary>
        /// <param name="graph">The graph that is initializing the task.</param>
        public override void Initialize(IGraph graph)
        {
            base.Initialize(graph);

            m_BehaviorTree.OnBehaviorTreeDestroyed += Destroy;
            m_BehaviorTree.OnBehaviorTreeCollisionEnter2D += EnteredCollision2D;
        }

        /// <summary>
        /// The agent has caused a 2D collision.
        /// </summary>
        /// <param name="collision">The collision that caused the event.</param>
        private void EnteredCollision2D(Collision2D collision)
        {
            if (!string.IsNullOrEmpty(m_Tag.Value) && !collision.gameObject.CompareTag(m_Tag.Value)) {
                return;
            }

            if (m_StoredCollisionGameObject != null && m_StoredCollisionGameObject.IsShared) { m_StoredCollisionGameObject.Value = collision.gameObject; }

            m_BehaviorTree.StartBranch(this);
        }

        /// <summary>
        /// The behavior tree has been destroyed.
        /// </summary>
        private void Destroy()
        {
            m_BehaviorTree.OnBehaviorTreeDestroyed -= Destroy;
            m_BehaviorTree.OnBehaviorTreeCollisionEnter2D -= EnteredCollision2D;
        }
    }
}
#endif