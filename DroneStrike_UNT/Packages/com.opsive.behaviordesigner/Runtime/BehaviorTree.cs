﻿#if GRAPH_DESIGNER
/// ---------------------------------------------
/// Behavior Designer
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Opsive.BehaviorDesigner.Editor")]
namespace Opsive.BehaviorDesigner.Runtime
{
    using Opsive.BehaviorDesigner.Runtime.Components;
    using Opsive.BehaviorDesigner.Runtime.Groups;
    using Opsive.BehaviorDesigner.Runtime.Systems;
    using Opsive.BehaviorDesigner.Runtime.Tasks;
    using Opsive.BehaviorDesigner.Runtime.Tasks.Events;
    using Opsive.BehaviorDesigner.Runtime.Utility;
    using Opsive.GraphDesigner.Runtime;
    using Opsive.GraphDesigner.Runtime.Variables;
    using Unity.Entities;
    using UnityEngine;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Container for managing behavior tree logic.
    /// </summary>
    public class BehaviorTree : MonoBehaviour, IGraph, IGraphComponent, ISharedVariableContainer
    {
        [Tooltip("The name of the behavior tree.")]
        [SerializeField] [Delayed] private string m_GraphName = "Behavior Tree";
        [Tooltip("A user specified ID for the graph.")]
        [SerializeField] [Delayed] private int m_Index;
        [Tooltip("The graph data.")]
        [SerializeField] private BehaviorTreeData m_Data = new BehaviorTreeData();
        [Tooltip("Should the behavior tree start when the component is enabled?")]
        [SerializeField] private bool m_StartWhenEnabled = true;
        [Tooltip("Should the behavior tree pause when the tree is disabled?")]
        [SerializeField] private bool m_PauseWhenDisabled;
        [Tooltip("Specifies when the behavior tree should be updated.")]
        [SerializeField] private UpdateMode m_UpdateMode;
        [Tooltip("Specifies how many tasks should be updated during a single tick.")]
        [SerializeField] private EvaluationType m_EvaluationType;
        [Tooltip("The maximum number of tasks that can run if the evaluation type is set to EvaluationType.Count.")]
        [SerializeField] [Range(1, ushort.MaxValue)] private int m_MaxEvaluationCount = 1;
        [Tooltip("A reference to the subtree.")]
        [SerializeField] private Subtree m_Subtree;

        private GameObject m_GameObject;
        private World m_World;
        private Entity m_Entity;
        private Dictionary<int, int> m_NodeIndexByRuntimeIndex = new Dictionary<int, int>();
        private static Dictionary<Entity, BehaviorTree> s_BehaviorTreeByEntity = new Dictionary<Entity, BehaviorTree>();
        private static EventNodeComparer s_EventNodeComparer = new EventNodeComparer();
        private bool m_SubtreeOverride;

        public string Name { get => m_GraphName; set => m_GraphName = value; }
        public int Index { get => m_Index; set => m_Index = value; }
        public int UniqueID { get => m_Data.UniqueID; }
        public UnityEngine.Object Parent { get { 
                if (this == null) { return null; }
                return Application.isPlaying && m_GameObject != null ? m_GameObject : gameObject;
            } }
        private BehaviorTreeData Data { get => !Application.isPlaying && m_Subtree != null ? m_Subtree.Data : m_Data; }
        public bool StartWhenEnabled { get => m_StartWhenEnabled; set => m_StartWhenEnabled = value; }
        public bool PauseWhenDisabled { get => m_PauseWhenDisabled; set => m_PauseWhenDisabled = value; }
        public UpdateMode UpdateMode { get => m_UpdateMode; set => m_UpdateMode = value; }
        public EvaluationType EvaluationType { get => m_EvaluationType; set => m_EvaluationType = value; }
        public int MaxEvaluationCount { get => m_MaxEvaluationCount; set => m_MaxEvaluationCount = Mathf.Max(1, Mathf.Min(ushort.MaxValue, value)); }
        public IGraph Subgraph
        {
            get => m_Subtree;
            set
            {
                if (m_Subtree == (Subtree)value) {
                    return;
                }

                ClearTree();
                m_Subtree = value as Subtree;
                m_SubtreeOverride = false;
                InheritSubtree(false);
                if (IsActive() && !IsPaused()) {
                    StartBehavior();
                }
#if UNITY_EDITOR
                // Generate a new ID so the graph reloads.
                if (Application.isPlaying) {
                    m_Data.RuntimeUniqueID = Guid.NewGuid().GetHashCode();
                }
#endif
            }
        }

        public ILogicNode[] LogicNodes { get => Data.LogicNodes; set => Data.LogicNodes = value as ITreeLogicNode[]; }
        public ITreeLogicNode[] TreeLogicNodes { get => Data.LogicNodes; set => Data.LogicNodes = value; }
        public IEventNode[] EventNodes { get => Data.EventNodes; set => Data.EventNodes = value; }
        public SharedVariable[] SharedVariables { get => m_Data.SharedVariables; set => m_Data.SharedVariables = value; }
        public SharedVariableGroup[] SharedVariableGroups {
#if UNITY_EDITOR
            get => m_Data.SharedVariableGroups;
            set => m_Data.SharedVariableGroups = value;
#else
            get => null;
            set { }
#endif
        }
        public ushort[] DisabledLogicNodes { get => Data.DisabledLogicNodes; set => Data.DisabledLogicNodes = value; }
        public ushort[] DisabledEventNodes { get => Data.DisabledEventNodes; set => Data.DisabledEventNodes = value; }

        public SharedVariable.SharingScope VariableScope { get => SharedVariable.SharingScope.Graph; }
        public World World { get => m_World; set { m_World = value; } }
        public Entity Entity { get => m_Entity; set { m_Entity = value; } }
        public bool Baked { get; set; }
        public static int BehaviorTreeCount { get => s_BehaviorTreeByEntity.Count; }

        public LogicNodeProperties[] LogicNodeProperties {
#if UNITY_EDITOR
            get => Data.LogicNodeProperties; 
            set => Data.LogicNodeProperties = value;
#else
            get => null;
            set { }
#endif
        }
        public NodeProperties[] EventNodeProperties {
#if UNITY_EDITOR
            get => Data.EventNodeProperties;
            set => Data.EventNodeProperties = value;
#else
            get => null;
            set { }
#endif
        }
        public GroupProperties[] GroupProperties {
#if UNITY_EDITOR
            get => Data.GroupProperties;
            set => Data.GroupProperties = value;
#else
            get => null;
            set { }
#endif
        }
        public TaskStatus Status { get
            {
                if (!Application.isPlaying || m_World == null || m_Entity == null || m_Data == null || m_Data.LogicNodes == null || Data.EventNodes == null) { return TaskStatus.Inactive; }

                // Find the Start event node.
                Start startEventNode = null;
                for (int i = 0; i < m_Data.EventNodes.Length; ++i) {
                    if (m_Data.EventNodes[i].GetType() == typeof(Start)) {
                        startEventNode = m_Data.EventNodes[i] as Start;
                    }
                }
                if (startEventNode == null || startEventNode.ConnectedIndex >= m_Data.LogicNodes.Length) { return TaskStatus.Inactive; }

                // The runtime index will match with the correct Entity TaskComponent.
                var runtimeIndex = m_Data.LogicNodes[startEventNode.ConnectedIndex].RuntimeIndex;
                var taskComponents = m_World.EntityManager.GetBuffer<TaskComponent>(m_Entity);
                if (runtimeIndex >= taskComponents.Length) { return TaskStatus.Inactive; }

                // Retun the status of the task that the Start node is connected to. This is the current tree status.
                return taskComponents[runtimeIndex].Status;
            } }

        // Flow callbacks.
        public Action OnBehaviorTreeStarted;
        public Action<bool> OnBehaviorTreeStopped;
        public Action OnBehaviorTreeDestroyed;

        // Physics callbacks.
        public Action<Collision> OnBehaviorTreeCollisionEnter;
        public Action<Collision> OnBehaviorTreeCollisionExit;
        public Action<Collision2D> OnBehaviorTreeCollisionEnter2D;
        public Action<Collision2D> OnBehaviorTreeCollisionExit2D;
        public Action<Collider> OnBehaviorTreeTriggerEnter;
        public Action<Collider> OnBehaviorTreeTriggerExit;
        public Action<Collider2D> OnBehaviorTreeTriggerEnter2D;
        public Action<Collider2D> OnBehaviorTreeTriggerExit2D;
        public Action<ControllerColliderHit> OnBehaviorTreeControllerColliderHit;

        // Event callbacks.
        public Action<BehaviorTree> OnWillSave;
        public Action<BehaviorTree, bool> OnDidSave;
        public Action<BehaviorTree> OnWillLoad;
        public Action<BehaviorTree, bool> OnDidLoad;

        // Coroutine support.
        private Dictionary<string, List<TaskCoroutine>> m_ActiveTaskCoroutines;

        /// <summary>
        /// Serializes the behavior tree.
        /// </summary>
        public void Serialize()
        {
            Data.Serialize();
            if (m_Subtree != null) {
                m_Data.SerializeSharedVariables();
            }
        }

        /// <summary>
        /// Deserialize the behavior tree.
        /// </summary>
        /// <param name="force">Should the behavior tree be force deserialized?</param>
        /// <returns>True if the tree was deserialized.</returns>
        public bool Deserialize(bool force = false)
        {
            if (this == null) {
                return false;
            }

            // Force the deserialization if the tree hasn't been deserialized yet at runtime.
            if (Application.isPlaying && m_GameObject == null) {
                force = true;
                m_GameObject = gameObject;
            }

            var deserialized = false;
            if (m_Subtree != null) {
                deserialized = InheritSubtree(force);
            } else if (m_Data != null) {
                deserialized = m_Data.Deserialize(this, this, force, Application.isPlaying, Application.isPlaying);
            }

            // Initialize tasks after deserialization. This is only needed at edit time as the tasks are initialized elsewhere at runtime.
            if (!Application.isPlaying && deserialized && m_Data != null && m_Data.LogicNodes != null) {
                for (int i = 0; i < m_Data.LogicNodes.Length; ++i) {
                    if (m_Data.LogicNodes[i] is Task task) {
                        task.Initialize(this, (ushort)i);
                    }
                }
            }

            return deserialized;
        }

        /// <summary>
        /// Inherits the subtree tasks and variables.
        /// </summary>
        /// <param name="force">Should the behavior tree be force deserialized?</param>
        /// <returns>True if the subtree was inherited.</returns>
        private bool InheritSubtree(bool force)
        {
            if (m_Subtree == null) {
                if (Application.isPlaying) {
                    m_Data.EventNodes = null;
                    m_Data.LogicNodes = null;
                    m_Data.SubtreeNodesReferences = null;
                    m_Data.DisabledLogicNodes = null;
                    m_Data.DisabledEventNodes = null;
#if UNITY_EDITOR
                    m_Data.EventNodeProperties = null;
                    m_Data.LogicNodeProperties = null;
                    m_Data.SharedVariableGroups = null;
                    m_Data.GroupProperties = null;
#endif
                }
                return false;
            }

            // The local behavior tree variables should be used.
            m_Data.DeserializeSharedVariables(force);
            if (m_Subtree.DeserializeSharedVariables(force) && Application.isPlaying && !m_SubtreeOverride && m_Data.SharedVariables != null) {
                // Set the binding on the subtree before the tasks are loaded. This is necessary because a new SharedVariable instance may need to be created.
                for (int i = 0; i < m_Data.SharedVariables.Length; ++i) {
                    m_Subtree.Data.OverrideVariableBinding(this, m_Data.SharedVariables[i]);
                }
            }

            if (!m_Subtree.Deserialize(force, false, Application.isPlaying, false)) {
                return false;
            }

            // Copy the deserialized objects at runtime to ensure each object is unique.
            if (Application.isPlaying && !m_SubtreeOverride) {
                m_Data.OverrideData(this, m_Subtree.Data, m_Data.SharedVariables);
                m_GameObject = gameObject;
                m_SubtreeOverride = true;
            }
            return true;
        }

        /// <summary>
        /// Adds the specified node.
        /// </summary>
        /// <param name="node">The node that should be added.</param>
        public void AddNode(ILogicNode node)
        {
            Data.AddNode(node as ITreeLogicNode);
        }

        /// <summary>
        /// Removes the specified node.
        /// </summary>
        /// <param name="node">The node that should be removed.</param>
        /// <returns>True if the node was removed.</returns>
        public bool RemoveNode(ILogicNode node)
        {
            return Data.RemoveNode(node as ITreeLogicNode);
        }

        /// <summary>
        /// Adds the specified event node.
        /// </summary>
        /// <param name="eventNode">The event node that should be added.</param>
        public void AddNode(IEventNode eventNode)
        {
            Data.AddNode(eventNode);
        }

        /// <summary>
        /// Removes the specified event node.
        /// </summary>
        /// <param name="eventNode">The event node that should be removed.</param>
        /// <returns>True if the event node was removed.</returns>
        public bool RemoveNode(IEventNode eventNode)
        {
            return Data.RemoveNode(eventNode);
        }

        /// <summary>
        /// Returns the Node of the specified type.
        /// </summary>
        /// <param name="type">The type of Node that should be retrieved.</typeparam>
        /// <returns>The Node of the specified type (can be null).</returns>
        public ILogicNode GetNode(Type type)
        {
            return Data.GetNode(type);
        }

        /// <summary>
        /// Returns the EventNode of the specified type.
        /// </summary>
        /// <param name="type">The type of EventNode that should be retrieved.</typeparam>
        /// <returns>The EventNode of the specified type (can be null). If the node is found the index will also be returned.</returns>
        public (IEventNode, ushort) GetEventNode(Type type)
        {
            return Data.GetEventNode(type);
        }

        /// <summary>
        /// The component has been enabled.
        /// </summary>
        private void OnEnable()
        {
            if (m_World != null && m_StartWhenEnabled) {
                StartBehavior();
            }
        }

        /// <summary>
        /// The component has started.
        /// </summary>
        private void Start()
        {
            if (m_StartWhenEnabled) {
                StartBehavior();
            }
        }

        /// <summary>
        /// Starts the behavior tree.
        /// </summary>
        /// <returns>True if the behavior tree was started.</returns>
        public bool StartBehavior()
        {
            var world = m_World == null ? World.DefaultGameObjectInjectionWorld : m_World;
            var entity = m_Entity == Entity.Null ? world.EntityManager.CreateEntity() : m_Entity;
            return StartBehavior(world, entity);
        }

        /// <summary>
        /// Starts the behavior tree.
        /// </summary>
        /// <param name="world">The world that should contain the behavior tree.</param>
        /// <param name="entity">The entity that should contain the behavior tree.</param>
        /// <returns>True if the behavior tree was started.</returns>
        public bool StartBehavior(World world, Entity entity)
        {
            return StartBehavior(world, entity, typeof(Start));
        }

        /// <summary>
        /// Starts the behavior tree.
        /// </summary>
        /// <param name="world">The world that contains the entity.</param>
        /// <param name="entity">The entity that should contain the behavior tree.</param>
        /// <param name="startBranchType">The type of branch that should be started.</param>
        /// <returns>True if the behavior tree was started.</returns>
        public bool StartBehavior(World world, Entity entity, Type startBranchType)
        {
            if (world == null) {
                return false;
            }

            if (s_BehaviorTreeByEntity.ContainsKey(entity)) {
                // The behavior tree may be paused.
                if (IsPaused(world, entity)) {
                    world.EntityManager.SetComponentEnabled<EnabledFlag>(entity, true);
                    if (OnBehaviorTreeStarted != null) {
                        OnBehaviorTreeStarted();
                    }
                    // Tasks can implement a pause specific interface.
                    var tasks = Data.LogicNodes;
                    for (int i = 0; i < tasks.Length; ++i) {
                        if (!(tasks[i] is IPausableTask pausableTask)) {
                            continue;
                        }
                        pausableTask.Resume(world, entity);
                    }
                    return true;
                }
                // The tree cannot be started multiple times.
                return false;
            }
            s_BehaviorTreeByEntity.Add(entity, this);

            if (!InitializeTree(world, entity)) {
                return false;
            }

            if (OnBehaviorTreeStarted != null) {
                OnBehaviorTreeStarted();
            }
            return StartBranch(startBranchType);
        }

        /// <summary>
        /// Initializes the tree within DOTS for all of the event tasks.
        /// </summary>
        /// <returns>True if the behavior tree was initialized.</returns>
        internal bool InitializeTree()
        {
            var world = m_World == null ? World.DefaultGameObjectInjectionWorld : m_World;
            var entity = m_Entity == Entity.Null ? world.EntityManager.CreateEntity() : m_Entity;
            return InitializeTree(world, entity);
        }

        /// <summary>
        /// Initializes the tree within DOTS for all of the event tasks.
        /// </summary>
        /// <param name="world">The world that contains the entity.</param>
        /// <param name="entity">The entity that should contain the behavior tree.</param>
        /// <returns>True if the behavior tree was initialized.</returns>
        internal bool InitializeTree(World world, Entity entity)
        {
            if (!Deserialize(false)) {
                enabled = false;
                return false;
            }

            if (m_Data.EventNodes == null || world == null) {
                return false;
            }

            // The tree may be reinitialized with the same world/entity.
            m_World = world;
            m_Entity = entity;

            // The tree may have already been initialized.
            if (world.EntityManager.HasComponent<EvaluateFlag>(entity)) {
                return true;
            }

            // Initialize the branch according to the connected index. This will ensure when the task is referencing other
            // tasks the index will be correct.
            var eventNodes = m_Data.EventNodes;
            HashSet<IEventNode> disabledNodes = null;
            if (m_Data.DisabledEventNodes != null && m_Data.DisabledEventNodes.Length > 0) {
                disabledNodes = new HashSet<IEventNode>();
                for (int i = 0; i < m_Data.DisabledEventNodes.Length; ++i) {
                    disabledNodes.Add(eventNodes[m_Data.DisabledEventNodes[i]]);
                }
            }
#if UNITY_EDITOR
            var eventNodeProperties = Data.EventNodeProperties;
            Array.Sort<IEventNode, NodeProperties>(eventNodes, eventNodeProperties, s_EventNodeComparer);
            Data.EventNodeProperties = eventNodeProperties;
#endif
            Array.Sort(eventNodes, s_EventNodeComparer);
            // The disabled event nodes array only stores the index. Update the index with the sorted value.
            if (disabledNodes != null) {
                var index = 0;
                var disabledEventNodes = m_Data.DisabledEventNodes;
                for (int i = 0; i < eventNodes.Length; ++i) {
                    if (disabledNodes.Contains(eventNodes[i])) {
                        disabledEventNodes[index] = (ushort)i;
                        index++;
                    }
                }
                m_Data.DisabledEventNodes = disabledEventNodes;
            }

            for (int i = 0; i < eventNodes.Length; ++i) {
                InitializeBranch(world, entity, eventNodes[i]);
            }
            return true;
        }

        /// <summary>
        /// Initialize the specified event branch within DOTS.
        /// </summary>
        /// <param name="world">The world that the entity exists in.</param>
        /// <param name="entity">The entity that the branch should be added to.</param>
        /// <param name="eventTask">The task that should be setup.</param>
        private void InitializeBranch(World world, Entity entity, IEventNode eventTask)
        {
            if (Data.LogicNodes == null) {
                return;
            }

            // There must be a starting event node.
            if (eventTask == null || eventTask.ConnectedIndex >= Data.LogicNodes.Length) {
                return;
            }

            if (!world.EntityManager.HasBuffer<TaskComponent>(entity)) {
                var taskComponentBuffer = world.EntityManager.AddBuffer<TaskComponent>(entity);
                taskComponentBuffer.EnsureCapacity(Data.LogicNodes.Length);
            }

            DynamicBuffer<BranchComponent> branchComponents;
            if (world.EntityManager.HasBuffer<BranchComponent>(entity)) {
                branchComponents = world.EntityManager.GetBuffer<BranchComponent>(entity);
            } else {
                branchComponents = world.EntityManager.AddBuffer<BranchComponent>(entity);
            }
            var startBranchIndex = (ushort)branchComponents.Length;
            branchComponents.Add(new BranchComponent() { ActiveIndex = ushort.MaxValue, NextIndex = ushort.MaxValue, LastActiveIndex = ushort.MaxValue });

            ComponentUtility.AddEvaluationComponent(world, entity, m_Data.LogicNodes.Length, m_EvaluationType, m_MaxEvaluationCount);
            world.EntityManager.AddComponent<EnabledFlag>(entity);
            world.EntityManager.AddComponent<EvaluateFlag>(entity);
            // A manual update mode will require the user to call the Tick method.
            if (m_UpdateMode == UpdateMode.Manual) {
                world.EntityManager.SetComponentEnabled<EnabledFlag>(entity, false);
                world.EntityManager.SetComponentEnabled<EvaluateFlag>(entity, false);
            }

            // Get the required parent system groups.
            var traversalTaskSystemGroup = world.GetOrCreateSystemManaged<TraversalTaskSystemGroup>();
            var reevaluateTaskSystemGroup = world.GetOrCreateSystemManaged<ReevaluateTaskSystemGroup>();
            var interruptTaskSystemGroup = world.GetOrCreateSystemManaged<InterruptTaskSystemGroup>();

            var taskComponents = world.EntityManager.GetBuffer<TaskComponent>(entity);
            var taskOffset = (ushort)(eventTask.ConnectedIndex - taskComponents.Length);
            for (int i = eventTask.ConnectedIndex; i < m_Data.LogicNodes.Length; ++i) {
                // Don't initialize tasks that aren't connected to the event node.
                if (i > eventTask.ConnectedIndex && m_Data.LogicNodes[i].ParentIndex == ushort.MaxValue) {
                    break;
                }

                taskComponents = world.EntityManager.GetBuffer<TaskComponent>(entity);

                // Determine the branch index based off of the parent. If the parent is a parallel node then the node will be part of a new branch.
                var branchIndex = startBranchIndex;
                if (m_Data.LogicNodes[i].ParentIndex != ushort.MaxValue) {
                    branchComponents = world.EntityManager.GetBuffer<BranchComponent>(entity);
                    var parentIndex = m_Data.LogicNodes[i].ParentIndex;
                    if (m_Data.LogicNodes[parentIndex] is IParallelNode) {
                        branchIndex = (ushort)branchComponents.Length;
                    } else {
                        branchIndex = taskComponents[parentIndex - taskOffset].BranchIndex;
                    }

                    // A new branch component may need to be added to keep track of the active task index for that branch.
                    if (branchIndex >= branchComponents.Length) {
                        branchComponents.Add(new BranchComponent() { ActiveIndex = ushort.MaxValue, NextIndex = ushort.MaxValue, LastActiveIndex = ushort.MaxValue });
                    }
                }

                // The TaskComponent index will be different from the LogicNode index if the tree has a gap of tasks that are not connected.
                // The RuntimeIndex maps the LogicNode index to the TaskComponent index.
                var node = m_Data.LogicNodes[i];
                node.RuntimeIndex = (ushort)(node.Index - taskOffset);
                m_Data.LogicNodes[i] = node;
                if (!m_NodeIndexByRuntimeIndex.ContainsKey(node.RuntimeIndex)) { // The index will already exist if multiple entities use the same MonoBehaviour.
                    m_NodeIndexByRuntimeIndex.Add(node.RuntimeIndex, node.Index);
                }

                if (m_Data.LogicNodes[i] is IAuthoringTask authoringTask) {
                    authoringTask.AddBufferElement(world, entity, gameObject);
                    taskComponents = world.EntityManager.GetBuffer<TaskComponent>(entity);
                    taskComponents.Add(new TaskComponent {
                        Status = TaskStatus.Inactive,
                        Index = node.RuntimeIndex,
                        ParentIndex = AdjustByIndexOffset(m_Data.LogicNodes[i].ParentIndex, taskOffset),
                        SiblingIndex = AdjustByIndexOffset(m_Data.LogicNodes[i].SiblingIndex, taskOffset),
                        BranchIndex = branchIndex,
                        FlagComponentType = authoringTask.Flag,
                        Disabled = !IsNodeEnabled(true, m_Data.LogicNodes[i].ParentIndex) || !IsNodeEnabled(true, i),
                    });

                    world.EntityManager.AddComponent(entity, authoringTask.Flag);
                    world.EntityManager.SetComponentEnabled(entity, authoringTask.Flag, false);
                    traversalTaskSystemGroup.AddSystemToUpdateList(world.GetOrCreateSystem(authoringTask.SystemType));
                } else if (m_Data.LogicNodes[i] is Task taskObject) {
                    taskObject.AddBufferElement(world, entity, GetHashCode(), node.RuntimeIndex);
                    taskComponents = world.EntityManager.GetBuffer<TaskComponent>(entity);
                    taskComponents.Add(new TaskComponent {
                        Status = TaskStatus.Inactive,
                        Index = node.RuntimeIndex,
                        ParentIndex = AdjustByIndexOffset(m_Data.LogicNodes[i].ParentIndex, taskOffset),
                        SiblingIndex = AdjustByIndexOffset(m_Data.LogicNodes[i].SiblingIndex, taskOffset),
                        BranchIndex = branchIndex,
                        FlagComponentType = typeof(TaskObjectFlag),
                        Disabled = !IsNodeEnabled(true, m_Data.LogicNodes[i].ParentIndex) || !IsNodeEnabled(true, i),
                    });
                    world.EntityManager.AddComponent(entity, typeof(TaskObjectFlag));
                    world.EntityManager.SetComponentEnabled(entity, typeof(TaskObjectFlag), false);
                    traversalTaskSystemGroup.AddSystemToUpdateList(world.GetOrCreateSystem(typeof(TaskObjectSystem)));

                    taskObject.Initialize(this, node.RuntimeIndex);
                } else {
                    Debug.LogError("Error: Unknown Task Type.", this);
                    continue;
                }

                // Conditional tasks can be reevaluated.
                if (m_Data.LogicNodes[i] is IConditional && m_Data.LogicNodes[i].ParentIndex != ushort.MaxValue) {
                    var ReevaluateFlag = new ComponentType();
                    Type reevaluateSystem;
                    if (m_Data.LogicNodes[i] is IAuthoringTask conditionalAuthoringTask) {
                        if (m_Data.LogicNodes[i] is IReevaluateResponder reevaluateTask) {
                            ReevaluateFlag = reevaluateTask.ReevaluateFlag;
                            reevaluateSystem = reevaluateTask.ReevaluateSystemType;
                        } else {
                            Debug.LogWarning($"Warning: The task {m_Data.LogicNodes[i]} doesn't have a separate reevaluation tag. This may lead to unexpected results. It is recommend " +
                                $"that the task implements the IReevaluate interface.");
                            ReevaluateFlag = conditionalAuthoringTask.Flag;
                            reevaluateSystem = conditionalAuthoringTask.SystemType;
                        }
                    } else {
                        ReevaluateFlag = typeof(TaskObjectReevaluateFlag);
                        reevaluateSystem = typeof(TaskObjectReevaluateSystem);
                    }
                    world.EntityManager.AddComponent(entity, ReevaluateFlag);
                    world.EntityManager.SetComponentEnabled(entity, ReevaluateFlag, false);
                    ComponentUtility.AddInterruptComponents(world.EntityManager, entity);
                    reevaluateTaskSystemGroup.AddSystemToUpdateList(world.GetOrCreateSystem(reevaluateSystem));

                    // Ignore any decorator tasks when determining the parent. Composite tasks can only be a conditional abort parent.
                    IComposite parentComposite = null;
                    ushort parentIndex;
                    var compositeParentIndex = m_Data.LogicNodes[i].ParentIndex;
                    do {
                        parentComposite = m_Data.LogicNodes[compositeParentIndex] as IComposite;
                        parentIndex = compositeParentIndex;
                        compositeParentIndex = m_Data.LogicNodes[compositeParentIndex].ParentIndex;
                    } while (compositeParentIndex != ushort.MaxValue && parentComposite == null);

                    var abortParent = parentComposite as IConditionalAbortParent;
                    if (abortParent != null && abortParent.AbortType != ConditionalAbortType.None) {
                        var lowerPriorityLowerIndex = ushort.MaxValue;
                        var lowerPriorityUpperIndex = ushort.MaxValue;
                        // Lower Priority aborts are recursive allowing a nested conditional task to be reevaluated even if the direct
                        // parent's sibling isn't active.
                        if (abortParent.AbortType == ConditionalAbortType.LowerPriority || abortParent.AbortType == ConditionalAbortType.Both) {
                            var parentChildCount = m_Data.GetChildCount(m_Data.LogicNodes[parentIndex], m_Data.LogicNodes);
                            lowerPriorityLowerIndex = AdjustByIndexOffset((ushort)(parentIndex + parentChildCount), taskOffset);
                            var parentTranversalIndex = parentIndex;
                            IConditionalAbortParent parentAbortParent = null;
                            while (parentTranversalIndex != ushort.MaxValue && ((parentAbortParent = m_Data.LogicNodes[parentTranversalIndex] as IConditionalAbortParent) != null || m_Data.LogicNodes[parentTranversalIndex] is IDecorator)) {
                                if (parentAbortParent != null && parentAbortParent.AbortType != ConditionalAbortType.LowerPriority && parentAbortParent.AbortType != ConditionalAbortType.Both) {
                                    break;
                                }
                                parentIndex = parentTranversalIndex;
                                parentTranversalIndex = m_Data.LogicNodes[parentTranversalIndex].ParentIndex;
                            }

                            // The conditional abort can reevaluate up to the rightmost task of the parent.
                            parentTranversalIndex = parentTranversalIndex != ushort.MaxValue ? parentTranversalIndex : parentIndex;
                            parentChildCount = m_Data.GetChildCount(m_Data.LogicNodes[parentTranversalIndex], m_Data.LogicNodes);
                            lowerPriorityUpperIndex = AdjustByIndexOffset((ushort)(parentTranversalIndex + parentChildCount), taskOffset);
                        }
                        var selfPriorityUpperIndex = ushort.MaxValue;
                        if (abortParent.AbortType == ConditionalAbortType.Self || abortParent.AbortType == ConditionalAbortType.Both) {
                            if (m_Data.LogicNodes[parentIndex].SiblingIndex == ushort.MaxValue) {
                                selfPriorityUpperIndex = (ushort)(parentIndex + m_Data.GetChildCount(m_Data.LogicNodes[parentIndex], m_Data.LogicNodes));
                            } else {
                                selfPriorityUpperIndex = (ushort)(m_Data.LogicNodes[parentIndex].SiblingIndex - 1);
                            }
                        }

                        var reevaluateTaskComponents = world.EntityManager.AddBuffer<ReevaluateTaskComponent>(entity);
                        var reevaluateTaskComponent = new ReevaluateTaskComponent()
                        {
                            Index = node.RuntimeIndex,
                            AbortType = abortParent.AbortType,
                            ReevaluateFlagComponentType = ReevaluateFlag,
                            LowerPriorityLowerIndex = lowerPriorityLowerIndex,
                            LowerPriorityUpperIndex = lowerPriorityUpperIndex,
                            SelfPriorityUpperIndex = selfPriorityUpperIndex,
                        };

                        reevaluateTaskComponents.Add(reevaluateTaskComponent);
                    }
                }

                // Add any systems that respond to interrupts.
                if (m_Data.LogicNodes[i] is IInterruptResponder interruptResponder) {
                    interruptTaskSystemGroup.AddSystemToUpdateList(world.GetOrCreateSystem(interruptResponder.InterruptSystemType));
                }
            }

            // The event task may perform its own logic.
            if (eventTask is IEventNodeEntityReceiver entityReceiver) {
                entityReceiver.AddBufferElement(world, entity, gameObject, taskOffset);
            }
            if (eventTask is IEventNodeGameObjectReceiver gameObjectReceiver) {
                gameObjectReceiver.Initialize(this);
            }

            traversalTaskSystemGroup.SortSystems();
        }

        /// <summary>
        /// Adjusts the index by the specified offset.
        /// </summary>
        /// <param name="index">The index that should be adjusted.</param>
        /// <param name="offset">The offset that should be adjusted by.</param>
        /// <returns>THe index by the specified offset.</returns>
        private ushort AdjustByIndexOffset(ushort index, ushort offset)
        {
            if (index == ushort.MaxValue) { return index; }
            return (ushort)(index - offset);
        }

        /// <summary>
        /// Stars the branch with the specified event task type.
        /// </summary>
        /// <param name="eventTaskType">The branch that should be started.</param>
        /// <returns>True if the branch was started.</returns>
        public bool StartBranch(Type eventTaskType)
        {
            if (m_World == null || m_Entity == Entity.Null) {
                return false;
            }

            return StartBranch(m_World, m_Entity, eventTaskType);
        }

        /// <summary>
        /// Stars the branch with the specified event task type.
        /// </summary>
        /// <param name="world">The world that the entity exists in.</param>
        /// <param name="entity">The entity that contains the branch.</param>
        /// <param name="eventTaskType">The branch that should be started.</param>
        /// <returns>True if the branch was started.</returns>
        public bool StartBranch(World world, Entity entity, Type eventTaskType)
        {
            if (!s_BehaviorTreeByEntity.ContainsKey(entity)) {
                return StartBehavior(world, entity, eventTaskType);
            }

            if (m_Data.EventNodes == null || entity.Index == 0 || !Application.isPlaying) {
                return false;
            }

            for (int i = 0; i < m_Data.EventNodes.Length; ++i) {
                if (m_Data.EventNodes[i].GetType() == eventTaskType) {
                    // The branch cannot start if it is disabled.
                    if (!IsNodeEnabled(false, i)) {
                        Debug.LogError($"Error: Unable to start the {eventTaskType.Name} branch because the node is disabled.", this);
                        return false;
                    }

                    return StartBranch(world, entity, m_Data.EventNodes[i]);
                }
            }

            Debug.LogError($"Error: Unable to start the {eventTaskType.Name} branch because it can't be found.", this);
            return false;
        }

        /// <summary>
        /// Starts the branch with the specified event task.
        /// </summary>
        /// <param name="eventTask">The branch that should be started.</param>
        /// <returns>True if the branch was started.</returns>
        public bool StartBranch(IEventNode eventTask)
        {
            if (m_World == null || m_Entity == Entity.Null) {
                return false;
            }

            return StartBranch(m_World, m_Entity, eventTask);
        }

        /// <summary>
        /// Starts the branch with the specified event task.
        /// </summary>
        /// <param name="world">The world that the entity exists in.</param>
        /// <param name="entity">The entity that contains the branch.</param>
        /// <param name="eventTask">The branch that should be started.</param>
        /// <returns>True if the branch was started.</returns>
        public bool StartBranch(World world, Entity entity, IEventNode eventTask)
        {
            if (!Application.isPlaying || entity == Entity.Null) {
                return false;
            }

            // The branch can't be started if it's not connected to any tasks.
            if (eventTask.ConnectedIndex == ushort.MaxValue) {
                Debug.LogError($"Error: Unable to start the {eventTask.GetType().Name} branch because it doesn't have a connecting task.", this);
                return false;
            }

            // The tree needs to be setup before the branch can start.
            if (!world.EntityManager.HasBuffer<BranchComponent>(entity)) {
                InitializeTree();
            }

            var connectedIndex = m_Data.LogicNodes[eventTask.ConnectedIndex].RuntimeIndex;
            return StartBranch(world, entity, connectedIndex, m_UpdateMode == UpdateMode.EveryFrame);
        }

        /// <summary>
        /// Starts the branch. This method is static allowing for alread-baked entities to be able to start the branch.
        /// </summary>
        /// <param name="world">The world that the entity exists in.</param>
        /// <param name="entity">The entity that contains the branch.</param>
        /// <param name="connectedIndex">The index of the starting task.</param>
        /// <param name="startEvaluation">Should the branch start evaluation? If false the tree will manually need to be ticked.</param>
        /// <returns>True if the branch was started.</returns>
        internal static bool StartBranch(World world, Entity entity, ushort connectedIndex, bool startEvaluation)
        {
            var taskComponents = world.EntityManager.GetBuffer<TaskComponent>(entity);
            if (connectedIndex >= taskComponents.Length) {
                Debug.LogError($"Error: Unable to start the branch on entity {entity} because the start index is greater than the task count.");
                return false;
            }

            var startTask = taskComponents[connectedIndex];
            // The branch can't be started twice or if it is disabled.
            if (startTask.Status == TaskStatus.Queued || startTask.Status == TaskStatus.Running || startTask.Disabled) {
                return false;
            }

            var systemGroup = world.GetExistingSystemManaged<BehaviorTreeSystemGroup>();
            if (systemGroup == null) {
                systemGroup = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<BehaviorTreeSystemGroup>();
                if (systemGroup == null) {
                    return false;
                }
            }
            systemGroup.Enabled = true;

            // The branch can start.
            startTask.Status = TaskStatus.Queued;
            taskComponents[connectedIndex] = startTask;

            var activeFlag = taskComponents[connectedIndex].FlagComponentType;
            world.EntityManager.SetComponentEnabled(entity, activeFlag, true);

            var branchComponents = world.EntityManager.GetBuffer<BranchComponent>(entity);
            var branchIndex = taskComponents[connectedIndex].BranchIndex;
            var branchComponent = branchComponents[branchIndex];
            branchComponent.ActiveIndex = branchComponent.NextIndex = connectedIndex;
            branchComponent.LastActiveIndex = ushort.MaxValue;
            branchComponent.ActiveFlagComponentType = activeFlag;
            branchComponents[branchIndex] = branchComponent;

            ComponentUtility.ResetEvaluationComponent(world, entity);

            if (startEvaluation) {
                world.EntityManager.SetComponentEnabled<EnabledFlag>(entity, true);
                world.EntityManager.SetComponentEnabled<EvaluateFlag>(entity, true);
            }
            return true;
        }

        /// <summary>
        /// Returns the behavior tree component specified by the entity.
        /// </summary>
        /// <param name="entity">The entity that should be retrieved.</param>
        /// <returns>The behavior tree component specified by the ID.</returns>
        public static BehaviorTree GetBehaviorTree(Entity entity)
        {
            if (s_BehaviorTreeByEntity.TryGetValue(entity, out var behaviorTree)) {
                return behaviorTree;
            }
            return null;
        }

        /// <summary>
        /// Returns the task at the specified index.
        /// </summary>
        /// <param name="index">The index of the task.</param>
        /// <returns>The task at the specified index.</returns>
        public ITreeLogicNode GetTask(int index)
        {
            Deserialize();

            if (m_Data.LogicNodes == null || index < 0 || index >= m_Data.LogicNodes.Length) {
                return null;
            }
            if (Application.isPlaying && m_NodeIndexByRuntimeIndex.Count > 0) {
                return m_Data.LogicNodes[m_NodeIndexByRuntimeIndex[index]];
            }
            return m_Data.LogicNodes[index];
        }

        /// <summary>
        /// Finds the task with the specified type.
        /// </summary>
        /// <returns>The first task found with the specified type (can be null).</returns>
        public T FindTask<T>() where T : Task
        {
            Deserialize();

            if (m_Data.LogicNodes == null || m_Data.LogicNodes.Length == 0) {
                return null;
            }

            for (int i = 0; i < m_Data.LogicNodes.Length; ++i) {
                if (m_Data.LogicNodes[i] is T task) {
                    return task;
                }
                if (m_Data.LogicNodes[i] is IStackedNode stackedNode) {
                    if (stackedNode.Nodes == null) {
                        continue;
                    }
                    for (int j = 0; j < stackedNode.Nodes.Length; ++j) {
                        if (stackedNode.Nodes[j] is T stackedTask) {
                            return stackedTask;
                        }
                    }
                }

            }

            return null;
        }

        /// <summary>
        /// Finds the tasks with the specified type. This method does not have any allocations.
        /// </summary>
        /// <param name="foundTasks">A pre-initialized array that will contain the found tasks.</param>
        /// <returns>The number of tasks found with the specified type.</returns>
        public int FindTasks<T>(T[] foundTasks) where T : Task
        {
            if (foundTasks == null || foundTasks.Length == 0) {
                return 0;
            }

            Deserialize();
            if (m_Data.LogicNodes == null || m_Data.LogicNodes.Length == 0) {
                return 0;
            }

            var count = 0;
            for (int i = 0; i < m_Data.LogicNodes.Length; ++i) {
                if (m_Data.LogicNodes[i] is T task) {
                    foundTasks[count] = task;
                    count++;
                    if (count == foundTasks.Length) {
                        return count;
                    }
                }
                if (m_Data.LogicNodes[i] is IStackedNode stackedNode) {
                    if (stackedNode.Nodes == null) {
                        continue;
                    }
                    for (int j = 0; j < stackedNode.Nodes.Length; ++j) {
                        if (stackedNode.Nodes[j] is T stackedTask) {
                            foundTasks[count] = stackedTask;
                            count++;
                            if (count == foundTasks.Length) {
                                return count;
                            }
                        }
                    }
                }

            }

            return count;
        }

        /// <summary>
        /// Finds the tasks with the specified type. 
        /// </summary>
        /// <returns>An array containing the found tasks.</returns>
        public T[] FindTasks<T>() where T : Task
        {
            Deserialize();

            if (m_Data.LogicNodes == null || m_Data.LogicNodes.Length == 0) {
                return null;
            }

            // Assume the maximum number of tasks will be found. The array will be resized before returning.
            var foundTasks = new T[m_Data.LogicNodes.Length];
            var count = FindTasks<T>(foundTasks);
            if (foundTasks.Length != count) {
                Array.Resize<T>(ref foundTasks, count);
            }
            return foundTasks;
        }

        /// <summary>
        /// Ticks the behavior tree. The UpdateMode must be set to Manual.
        /// The behavior tree will not be executed instantaneously. It will instead be ticked the next time the DOTS system group is updated.
        /// </summary>
        public void Tick()
        {
            if (m_UpdateMode != UpdateMode.Manual) {
                Debug.LogWarning("Warning: The behavior tree UpdateMode must be set to Manual in order to be ticked manually.", this);
                return;
            }

            if (m_World == null || m_Entity == Entity.Null) {
                Debug.LogWarning("Warning: The behavior tree must be started in order for it to be ticked manually.", this);
                return;
            }

            Tick(m_World, m_Entity);
        }

        /// <summary>
        /// Ticks the behavior tree. 
        /// The behavior tree will not be executed instantaneously. It will instead be ticked the next time the DOTS system group is updated.
        /// </summary>
        /// <param name="world">The world that the entity exists in.</param>
        /// <param name="entity">The entity that contains the behavior tree.</param>
        public static void Tick(World world, Entity entity)
        {
            if (world == null || entity.Index == 0) {
                return;
            }
            world.EntityManager.SetComponentEnabled<EvaluateFlag>(entity, true);
        }

        /// <summary>
        /// Reevaluates the SubtreeReferences by calling the EvaluateSubtrees method.
        /// </summary>
        public void ReevaluateSubtreeReferences()
        {
            if (!m_Data.ReevaluateSubtreeReferences(this, this, ClearTree)) {
                return;
            }

            // Restart the tree.
            InitializeTree();
            if (enabled && m_GameObject.activeSelf) {
                StartBehavior();
            }
        }

        /// <summary>
        /// Stops or pauses the behavior tree.
        /// </summary>
        /// <param name="pause">Should the behavior tree be paused?</param>
        /// <returns>True if the behavior tree was stopped or paused.</returns>
        public bool StopBehavior(bool pause = false)
        {
            return StopBehavior(m_World, m_Entity, pause);
        }

        /// <summary>
        /// Stops or pauses the behavior tree.
        /// </summary>
        /// <param name="world">The world that the entity exists in.</param>
        /// <param name="entity">The entity that contains the behavior tree.</param>
        /// <param name="pause">Should the behavior tree be paused?</param>
        /// <returns>True if the behavior tree was stopped or paused.</returns>
        public bool StopBehavior(World world, Entity entity, bool pause = false)
        {
            if (world == null || !world.IsCreated || entity == Entity.Null) {
                return false;
            }

            // The tree could be stopped after it has been paused.
            if (world.EntityManager.HasComponent<EnabledFlag>(entity)) {
                world.EntityManager.SetComponentEnabled<EnabledFlag>(entity, false);
            }
            if (world.EntityManager.HasComponent<EvaluateFlag>(entity)) {
                world.EntityManager.SetComponentEnabled<EvaluateFlag>(entity, false);
            }

            if (!s_BehaviorTreeByEntity.ContainsKey(entity)) {
                return false;
            }

            // Notify those interested that the behavior tree has been stopped.
            if (OnBehaviorTreeStopped != null) {
                OnBehaviorTreeStopped(pause);
            }

            // Tasks can implement a pause and end specific callback.
            var tasks = Data.LogicNodes;
            for (int i = 0; i < tasks.Length; ++i) {
                if (pause) {
                    if (!(tasks[i] is IPausableTask pausableTask)) {
                        continue;
                    }
                    pausableTask.Pause(world, entity);
                } else if (m_Data.LogicNodes[i] is Task task) {
                    task.OnEnd();
                }
            }

            // Removing the EnabledFlag and EvaluationComponent is sufficient to pause the tree.
            if (pause) {
                return true;
            }

            s_BehaviorTreeByEntity.Remove(entity);

            StopBehavior(world, entity);

            return true;
        }

        /// <summary>
        /// Stops the behavior tree. This method should only be called from an ECS system.
        /// </summary>
        /// <param name="world">The world that the entity exists in.</param>
        /// <param name="entity">The entity that contains the behavior tree.</param>
        public static void StopBehavior(World world, Entity entity)
        {
            if (world == null || entity.Index == 0) {
                return;
            }

            var branchComponents = world.EntityManager.GetBuffer<BranchComponent>(entity);
            var taskComponents = world.EntityManager.GetBuffer<TaskComponent>(entity);
            for (int i = 0; i < branchComponents.Length; ++i) {
                var branchComponent = branchComponents[i];
                if (branchComponent.ActiveIndex == ushort.MaxValue) {
                    continue;
                }

                // Stop all of the active tasks within the branch.
                var taskIndex = branchComponent.ActiveIndex;
                while (taskIndex != ushort.MaxValue) {
                    var taskComponent = taskComponents[taskIndex];
                    taskComponent.Status = TaskStatus.Inactive;
                    taskComponent.Reevaluate = false;
                    taskComponents[taskIndex] = taskComponent;

                    taskIndex = taskComponent.ParentIndex;
                }

                world.EntityManager.SetComponentEnabled(entity, branchComponent.ActiveFlagComponentType, false);
                branchComponent.ActiveIndex = branchComponent.NextIndex = branchComponent.LastActiveIndex = ushort.MaxValue;
                branchComponent.ActiveFlagComponentType = new ComponentType();
                branchComponent.InterruptType = InterruptType.None;
                branchComponent.InterruptIndex = 0;
                branchComponents[i] = branchComponent;
            }

            // Stop all reevaluations.
            if (world.EntityManager.HasBuffer<ReevaluateTaskComponent>(entity)) {
                var reevaluateTaskComponents = world.EntityManager.GetBuffer<ReevaluateTaskComponent>(entity);
                for (int i = 0; i < reevaluateTaskComponents.Length; ++i) {
                    if (reevaluateTaskComponents[i].ReevaluateStatus == ReevaluateStatus.Inactive) {
                        continue;
                    }

                    var reevaluateTaskComponent = reevaluateTaskComponents[i];
                    world.EntityManager.SetComponentEnabled(entity, reevaluateTaskComponent.ReevaluateFlagComponentType, false);
                    reevaluateTaskComponent.ReevaluateStatus = ReevaluateStatus.Inactive;
                    reevaluateTaskComponent.OriginalStatus = TaskStatus.Inactive;

                    reevaluateTaskComponents[i] = reevaluateTaskComponent;
                }
            }
        }

        /// <summary>
        /// Restarts the behavior tree.
        /// </summary>
        /// <returns>True if the behavior tree was restarted.</returns>
        public bool RestartBehavior()
        {
            if (!IsActive()) {
                return false;
            }
            if (!StopBehavior()) {
                return false;
            }
            return StartBehavior();
        }

        /// <summary>
        /// Clears all of the tree components.
        /// </summary>
        /// <param name="world">The world that the entity exists in.</param>
        /// <param name="entity">The entity that contains the behavior tree.</param>
        private void ClearTree()
        {
            ClearTree(m_World, m_Entity);
        }

        /// <summary>
        /// Clears all of the tree components.
        /// </summary>
        /// <param name="world">The world that the entity exists in.</param>
        /// <param name="entity">The entity that contains the behavior tree.</param>
        private void ClearTree(World world, Entity entity)
        {
            if (world == null || entity == Entity.Null || Data.LogicNodes == null) {
                return;
            }

            StopBehavior(world, entity, false);

            world.EntityManager.RemoveComponent<EnabledFlag>(entity);
            world.EntityManager.RemoveComponent<EvaluateFlag>(entity);
            ComponentUtility.RemoveEvaluationComponent(world, entity);

            var branchComponents = world.EntityManager.GetBuffer<BranchComponent>(entity);
            var taskComponents = world.EntityManager.GetBuffer<TaskComponent>(entity);
            branchComponents.Clear();
            taskComponents.Clear();
            if (world.EntityManager.HasBuffer<ReevaluateTaskComponent>(entity)) {
                var reevaluateTaskComponents = world.EntityManager.GetBuffer<ReevaluateTaskComponent>(entity);
                reevaluateTaskComponents.Clear();
            }

            for (int i = 0; i < m_Data.LogicNodes.Length; ++i) {
                if (m_Data.LogicNodes[i] is IAuthoringTask authoringTask) {
                    authoringTask.ClearBufferElement(world, entity);
                    if (world.EntityManager.HasComponent(entity, authoringTask.Flag)) {
                        world.EntityManager.RemoveComponent(entity, authoringTask.Flag);
                    }
                    if (m_Data.LogicNodes[i] is IReevaluateResponder reevaluateTask) {
                        if (world.EntityManager.HasComponent(entity, reevaluateTask.ReevaluateFlag)) {
                            world.EntityManager.RemoveComponent(entity, reevaluateTask.ReevaluateFlag);
                        }
                    }
                } else if (m_Data.LogicNodes[i] is Task monoTask) {
                    monoTask.ClearBufferElement(world, entity);
                    if (m_Data.LogicNodes[i] is IConditional) {
                        if (world.EntityManager.HasComponent(entity, typeof(TaskObjectReevaluateFlag))) {
                            world.EntityManager.RemoveComponent(entity, typeof(TaskObjectReevaluateFlag));
                        }
                        if (world.EntityManager.HasComponent(entity, typeof(InterruptFlag))) {
                            world.EntityManager.RemoveComponent(entity, typeof(InterruptFlag));
                        }
                    }
                    if (world.EntityManager.HasComponent(entity, typeof(TaskObjectFlag))) {
                        world.EntityManager.RemoveComponent(entity, typeof(TaskObjectFlag));
                    }
                }
            }

            for (int i = 0; i < m_Data.EventNodes.Length; ++i) {
                if (m_Data.EventNodes[i] is IEventNodeEntityReceiver entityReceiver) {
                    entityReceiver.ClearBufferElement(world, entity);
                }
            }

            m_NodeIndexByRuntimeIndex.Clear();
        }

        /// <summary>
        /// Returns the SharedVariable with the specified name.
        /// </summary>
        /// <param name="name">The name of the SharedVariable that should be retrieved.</param>
        /// <returns>The SharedVariable with the specified name (can be null).</returns>
        public SharedVariable GetVariable(PropertyName name)
        {
            return GetVariable(name, SharedVariable.SharingScope.Graph);
        }

        /// <summary>
        /// Returns the SharedVariable with the specified name and scope.
        /// </summary>
        /// <param name="name">The name of the SharedVariable that should be retrieved.</param>
        /// <param name="scope">The scope of the SharedVariable that should be retrieved.</param>
        /// <returns>The SharedVariable with the specified name (can be null).</returns>
        public SharedVariable GetVariable(PropertyName name, SharedVariable.SharingScope scope)
        {
            Deserialize();

            return m_Data.GetVariable(this, name, scope);
        }

        /// <summary>
        /// Returns the SharedVariable of the specified name.
        /// </summary>
        /// <param name="name">The name of the SharedVariable that should be retrieved.</param>
        /// <returns>The SharedVariable with the specified name (can be null).</returns>
        public SharedVariable<T> GetVariable<T>(PropertyName name)
        {
            return GetVariable<T>(name, SharedVariable.SharingScope.Graph);
        }

        /// <summary>
        /// Returns the SharedVariable of the specified name.
        /// </summary>
        /// <param name="name">The name of the SharedVariable that should be retrieved.</param>
        /// <param name="scope">The scope of the SharedVariable that should be retrieved.</param>
        /// <returns>The SharedVariable with the specified name (can be null).</returns>
        public SharedVariable<T> GetVariable<T>(PropertyName name, SharedVariable.SharingScope scope)
        {
            Deserialize();

            return m_Data.GetVariable<T>(this, name, scope);
        }

        /// <summary>
        /// Sets the value of the SharedVariable.
        /// </summary>
        /// <typeparam name="T">The type of SharedVarible.</typeparam>
        /// <param name="name">The name of the SharedVariable.</param>
        /// <param name="value">The value of the SharedVariable.</param>
        /// <returns>True if the value was set.</returns>
        public bool SetVariableValue<T>(PropertyName name, T value)
        {
            return SetVariableValue<T>(name, value, SharedVariable.SharingScope.Graph);
        }

        /// <summary>
        /// Sets the value of the SharedVariable.
        /// </summary>
        /// <typeparam name="T">The type of SharedVarible.</typeparam>
        /// <param name="name">The name of the SharedVariable.</param>
        /// <param name="value">The value of the SharedVariable.</param>
        /// <param name="scope">The scope of the SharedVariable that should be set.</typeparam>
        /// <returns>True if the value was set.</returns>
        public bool SetVariableValue<T>(PropertyName name, T value, SharedVariable.SharingScope scope)
        {
            Deserialize();

            return m_Data.SetVariableValue<T>(this, name, value, scope);
        }

        /// <summary>
        /// Gets the behavior tree save data.
        /// </summary>
        /// <param name="variableSaveScope">Specifies which variables should be saved. Graph variables will automatically be saved.</param>
        /// <returns>The save data if the behavior tree can be saved.</returns>
        public SaveManager.SaveData? Save(SaveManager.VariableSaveScope variableSaveScope = 0)
        {
            if (OnWillSave != null) {
                OnWillSave(this);
            }
            var saveData = SaveManager.Save(new BehaviorTree[] { this }, variableSaveScope);
            if (OnDidSave != null) {
                OnDidSave(this, saveData.HasValue);
            }
            return saveData;
        }

        /// <summary>
        /// Saves the behavior tree at the specified file path.
        /// </summary>
        /// <param name="filePath">The file path to save the behavior tree at. The file will be replaced if it already exists.</param>
        /// <param name="variableSaveScope">Specifies which variables should be saved. Graph variables will automatically be saved.</param>
        /// <returns>True if the behavior tree was successfully saved.</returns>
        public bool Save(string filePath, SaveManager.VariableSaveScope variableSaveScope = 0)
        {
            if (OnWillSave != null) {
                OnWillSave(this);
            }
            var success = SaveManager.Save(new BehaviorTree[] { this }, filePath, variableSaveScope);
            if (OnDidSave != null) {
                OnDidSave(this, success);
            }
            return success;
        }

        /// <summary>
        /// Loads the behavior tree from the specified file path.
        /// </summary>
        /// <param name="filePath">The file path to load the behavior tree at.</param>
        /// <param name="afterVariablesRestored">Optional callback after the graph variables have been restored.</param>
        /// <returns>True if the behavior tree was successfully loaded.</returns>
        public bool Load(string filePath, Action<BehaviorTree> afterVariablesRestored = null)
        {
            if (OnWillLoad != null) {
                OnWillLoad(this);
            }
            var success = SaveManager.Load(new BehaviorTree[] { this }, filePath, afterVariablesRestored);
            if (OnDidLoad != null) { 
                OnDidLoad(this, success);
            }
            return success;
        }

        /// <summary>
        /// Loads the behavior tree from the specified save data.
        /// </summary>
        /// <param name="saveData">The data associated with the behavior tree.</param>
        /// <param name="afterVariablesRestored">Optional callback after the graph variables have been restored.</param>
        /// <returns>True if the behavior tree was successfully loaded.</returns>
        public bool Load(SaveManager.SaveData saveData, Action<BehaviorTree> afterVariablesRestored = null)
        {
            if (OnWillLoad != null) {
                OnWillLoad(this);
            }
            var success = SaveManager.Load(new BehaviorTree[] { this }, saveData, afterVariablesRestored);
            if (OnDidLoad != null) {
                OnDidLoad(this, success);
            }
            return success;
        }

        /// <summary>
        /// Starts the task courtine with the specified name.
        /// </summary>
        /// <param name="task">The task that the coroutine belongs to.</param>
        /// <param name="coroutineName">The name of the coroutine method.</param>
        /// <returns>The created routine (can be null).</returns>
        public Coroutine StartTaskCoroutine(Task task, string coroutineName)
        {
            var method = task.GetType().GetMethod(coroutineName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (method == null) {
                Debug.LogError($"Error: The coroutine {coroutineName} cannot be started due to the method not being found on {task.GetType()}.");
                return null;
            }

            if (m_ActiveTaskCoroutines == null) {
                m_ActiveTaskCoroutines = new Dictionary<string, List<TaskCoroutine>>();
            }
            var taskCoroutine = new TaskCoroutine(this, (IEnumerator)method.Invoke(task, new object[] { }), coroutineName);
            if (!m_ActiveTaskCoroutines.TryGetValue(coroutineName, out var taskCoroutines)) {
                taskCoroutines = new List<TaskCoroutine>();
                m_ActiveTaskCoroutines.Add(coroutineName, taskCoroutines);
            }
            taskCoroutines.Add(taskCoroutine);

            return taskCoroutine.Coroutine;
        }

        /// <summary>
        /// Starts the task courtine with the specified name.
        /// </summary>
        /// <param name="task">The task that the coroutine belongs to.</param>
        /// <param name="coroutineName">The name of the coroutine method.</param>
        /// <param name="value">The input parameter to the coroutine.</param>
        /// <returns>The created routine (can be null).</returns>
        public Coroutine StartTaskCoroutine(Task task, string coroutineName, object value)
        {
            var method = task.GetType().GetMethod(coroutineName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (method == null) {
                Debug.LogError($"Error: The coroutine {coroutineName} cannot be started due to the method not being found on {task.GetType()}.");
                return null;
            }

            if (m_ActiveTaskCoroutines == null) {
                m_ActiveTaskCoroutines = new Dictionary<string, List<TaskCoroutine>>();
            }
            var taskCoroutine = new TaskCoroutine(this, (IEnumerator)method.Invoke(task, new object[] { value }), coroutineName);
            if (!m_ActiveTaskCoroutines.TryGetValue(coroutineName, out var taskCoroutines)) {
                taskCoroutines = new List<TaskCoroutine>();
                m_ActiveTaskCoroutines.Add(coroutineName, taskCoroutines);
            }
            taskCoroutines.Add(taskCoroutine);

            return taskCoroutine.Coroutine;
        }

        /// <summary>
        /// Stops the task courtine with the specified name.
        /// </summary>
        /// <param name="coroutineName">The name of the coroutine method.</param>
        public void StopTaskCoroutine(string coroutineName)
        {
            if (!m_ActiveTaskCoroutines.TryGetValue(coroutineName, out var taskCoroutines)) {
                return;
            }

            for (int i = 0; i < taskCoroutines.Count; ++i) {
                taskCoroutines[i].Stop();
            }
        }

        /// <summary>
        /// Stops all of the task coroutines.
        /// </summary>
        public void StopAllTaskCoroutines()
        {
            if (m_ActiveTaskCoroutines == null) {
                return;
            }

            foreach (var entry in m_ActiveTaskCoroutines) {
                var taskCoroutines = entry.Value;
                for (int i = 0; i < taskCoroutines.Count; ++i) {
                    taskCoroutines[i].Stop();
                }
            }
        }
        
        /// <summary>
        /// The TaskCoroutine has ended.
        /// </summary>
        /// <param name="taskCoroutine">The coroutine that has ended.</param>
        /// <param name="coroutineName">The name of the coroutine.</param>
        public void TaskCoroutineEnded(TaskCoroutine taskCoroutine, string coroutineName)
        {
            if (!m_ActiveTaskCoroutines.TryGetValue(coroutineName, out var taskCoroutines)) {
                return;
            }

            taskCoroutines.Remove(taskCoroutine);
            if (taskCoroutines.Count == 0) {
                m_ActiveTaskCoroutines.Remove(coroutineName);
            }
        }

        /// <summary>
        /// OnCollisionEnter callback.
        /// </summary>
        /// <param name="collision">The resulting collision.</param>
        private void OnCollisionEnter(Collision collision)
        {
            if (OnBehaviorTreeCollisionEnter != null) {
                OnBehaviorTreeCollisionEnter(collision);
            }
        }

        /// <summary>
        /// OnCollisionExit callback.
        /// </summary>
        /// <param name="collision">The resulting collision.</param>
        private void OnCollisionExit(Collision collision)
        {
            if (OnBehaviorTreeCollisionExit != null) {
                OnBehaviorTreeCollisionExit(collision);
            }
        }

        /// <summary>
        /// OnCollisionEnter2D callback.
        /// </summary>
        /// <param name="collision">The resulting collision.</param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (OnBehaviorTreeCollisionEnter2D != null) {
                OnBehaviorTreeCollisionEnter2D(collision);
            }
        }

        /// <summary>
        /// OnCollisionExit2D callback.
        /// </summary>
        /// <param name="collision">The resulting collision.</param>
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (OnBehaviorTreeCollisionExit2D != null) {
                OnBehaviorTreeCollisionExit2D(collision);
            }
        }

        /// <summary>
        /// OnTriggerEnter callback.
        /// </summary>
        /// <param name="other">The overlapping collider.</param>
        private void OnTriggerEnter(Collider other)
        {
            if (OnBehaviorTreeTriggerEnter != null) {
                OnBehaviorTreeTriggerEnter(other);
            }
        }

        /// <summary>
        /// OnTriggerExit callback.
        /// </summary>
        /// <param name="other">The collider that is no longer overlapping.</param>
        private void OnTriggerExit(Collider other)
        {
            if (OnBehaviorTreeTriggerExit != null) {
                OnBehaviorTreeTriggerExit(other);
            }
        }

        /// <summary>
        /// OnTriggerEnter2D callback.
        /// </summary>
        /// <param name="other">The overlapping collider.</param>
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (OnBehaviorTreeTriggerEnter2D != null) {
                OnBehaviorTreeTriggerEnter2D(other);
            }
        }

        /// <summary>
        /// OnTriggerExit2D callback.
        /// </summary>
        /// <param name="other">The collider that is no longer overlapping.</param>
        private void OnTriggerExit2D(Collider2D other)
        {
            if (OnBehaviorTreeTriggerExit2D != null) {
                OnBehaviorTreeTriggerExit2D(other);
            }
        }

        /// <summary>
        /// OnControllerColliderHit callback.
        /// </summary>
        /// <param name="hit">The hit result.</param>
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (OnBehaviorTreeControllerColliderHit != null) {
                OnBehaviorTreeControllerColliderHit(hit);
            }
        }

#if UNITY_EDITOR
        /// <summary>
        /// OnDrawGizmos callback.
        /// </summary>
        private void OnDrawGizmos()
        {
            if (!enabled) {
                return;
            }

            if (m_Data != null && m_Data.LogicNodes != null) {
                for (int i = 0; i < m_Data.LogicNodes.Length; ++i) {
                    if (m_Data.LogicNodes[i] is Task task) {
                        task.OnDrawGizmos(this);
                    }
                }
            }
        }

        /// <summary>
        /// OnDrawGizmos callback.
        /// </summary>
        private void OnDrawGizmosSelected()
        {
            if (!enabled) {
                return;
            }

            if (m_Data != null && m_Data.LogicNodes != null) {
                for (int i = 0; i < m_Data.LogicNodes.Length; ++i) {
                    if (m_Data.LogicNodes[i] is Task task) {
                        task.OnDrawGizmosSelected(this);
                    }
                }
            }
        }
#endif

        /// <summary>
        /// The behavior tree has been disabled.
        /// </summary>
        private void OnDisable()
        {
            if (m_Entity == Entity.Null) {
                return;
            }

            StopBehavior(m_World, m_Entity, m_PauseWhenDisabled);
        }

        /// <summary>
        /// The behavior tree has been destroyed.
        /// </summary>
        private void OnDestroy()
        {
            if (m_Entity == Entity.Null) {
                return;
            }

            if (OnBehaviorTreeDestroyed != null) {
                OnBehaviorTreeDestroyed();
            }
            StopBehavior(m_World, m_Entity, false);
            m_GameObject = null;
        }

        /// <summary>
        /// Is the node with the specified index enabled?
        /// </summary>
        /// <param name="logicNode">Is the node a LogicNode?</param>
        /// <param name="index">The index of the node.</param>
        /// <returns>True if the node with the specified index is enabled.</returns>
        public bool IsNodeEnabled(bool logicNode, int index)
        {
            return Data.IsNodeEnabled(logicNode, index);
        }

        /// <summary>
        /// Is the node with the specified index active?
        /// </summary>
        /// <param name="logicNode">Is the node a LogicNode?</param>
        /// <param name="index">The index of the node.</param>
        /// <returns>True if the node with the specified index is active.</returns>
        public bool IsNodeActive(bool logicNode, int index)
        {
            if (!Application.isPlaying || m_Entity == Entity.Null) {
                return false;
            }

            var taskComponents = m_World.EntityManager.GetBuffer<TaskComponent>(m_Entity);
            var logicNodeIndex = index;
            if (!logicNode && m_Data.EventNodes != null && index < m_Data.EventNodes.Length) {
                // Find the logic node that the event node is connected to.
                logicNodeIndex = m_Data.EventNodes[index].ConnectedIndex;
            }
            if (logicNodeIndex >= taskComponents.Length) {
                return false;
            }
            var taskComponent = taskComponents[logicNodeIndex];
            return taskComponent.Status == TaskStatus.Running;
        }

        /// <summary>
        /// Returns true if the behavior tree is active.
        /// </summary>
        /// <returns>True if the behavior tree is active.</returns>
        public bool IsActive()
        {
            return IsActive(m_Entity);
        }

        /// <summary>
        /// Returns true if the behavior tree is active.
        /// </summary>
        /// <param name="entity">The entity that contains the behavior tree.</param>
        /// <returns>True if the behavior tree is active.</returns>
        public bool IsActive(Entity entity)
        {
            if (entity == Entity.Null) {
                return false;
            }
            return s_BehaviorTreeByEntity.ContainsKey(entity);
        }

        /// <summary>
        /// Returns true if the behavior tree is paused.
        /// </summary>
        /// <returns>True if the behavior tree is paused.</returns>
        public bool IsPaused()
        {
            return IsPaused(m_World, m_Entity);
        }

        /// <summary>
        /// Returns true if the behavior tree is paused.
        /// </summary>
        /// <param name="world">The world that the entity exists in.</param>
        /// <param name="entity">The entity that contains the behavior tree.</param>
        /// <returns>True if the behavior tree is paused.</returns>
        public bool IsPaused(World world, Entity entity)
        {
            if (!IsActive(entity)) {
                return false;
            }
            return world.EntityManager.HasComponent<EnabledFlag>(entity) && !world.EntityManager.IsComponentEnabled<EnabledFlag>(entity);
        }

        /// <summary>
        /// Copies the graph onto the current graph.
        /// </summary>
        /// <param name="other">The graph that should be copied.</param>
        public void Clone(IGraph other)
        {
            m_Data = new BehaviorTreeData();
            m_Data.EventNodes = other.EventNodes;
            m_Data.LogicNodes = other.LogicNodes as ITreeLogicNode[];
            m_Data.SharedVariables = other.SharedVariables;
            m_Data.DisabledLogicNodes = other.DisabledLogicNodes;
            m_Data.DisabledEventNodes = other.DisabledEventNodes;

#if UNITY_EDITOR
            m_Data.EventNodeProperties = other.EventNodeProperties;
            m_Data.LogicNodeProperties = other.LogicNodeProperties;
            m_Data.GroupProperties = other.GroupProperties;
            m_Data.SharedVariableGroups = other.SharedVariableGroups;
#endif

            m_Data.Serialize();
        }

        /// <summary>
        /// Overrides ToString.
        /// </summary>
        /// <returns>The desired string value.</returns>
        public override string ToString()
        {
            return $"{m_GraphName} (Index {m_Index})";
        }

        /// <summary>
        /// Returns the hashcode of the graph.
        /// </summary>
        /// <returns>The hashcode of the graph.</returns>
        public override int GetHashCode()
        {
            if (m_Subtree != null) {
                return m_Subtree.GetHashCode();
            }
            return base.GetHashCode();
        }

        /// <summary>
        /// Callback when the domain should be reloaded.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Reinitialize()
        {
            s_BehaviorTreeByEntity = new Dictionary<Entity, BehaviorTree>();
        }

        /// <summary>
        /// Enables the baked behavior tree system.
        /// </summary>
        /// <param name="world">The world that the system has been added to.</param>
        public static void EnableBakedBehaviorTreeSystem(World world)
        {
            if (world == null) {
                return;
            }

            EnableBakedBehaviorTreeSystem(world.Unmanaged);
        }

        /// <summary>
        /// Enables the baked behavior tree system.
        /// </summary>
        /// <param name="world">The unmanged world that the system has been added to.</param>
        public static void EnableBakedBehaviorTreeSystem(WorldUnmanaged world)
        {
            world.GetExistingSystemState<StartBakedBehaviorTreeSystem>().Enabled = true;
        }

        /// <summary>
        /// Converts the behavior tree to a DOTS entity.
        /// </summary>
        public class BehaviorTreeBaker : Baker<BehaviorTree>
        {
            private static MethodInfo s_GetTypeOfSystemMethod;

            /// <summary>
            /// Bakes the behavior tree to the DOTS entity.
            /// </summary>
            /// <param name="behaviorTree">The authoring behavior tree.</param>
            public override void Bake(BehaviorTree behaviorTree)
            {
                if (!behaviorTree.StartWhenEnabled || !behaviorTree.enabled) {
                    return;
                }

                var entity = GetEntity(behaviorTree, TransformUsageFlags.Dynamic);
                var worlds = World.All;
                for (int i = 0; i < worlds.Count; ++i) {
                    if (worlds[i].EntityManager.Exists(entity)) {
                        if (!behaviorTree.InitializeTree(worlds[i], entity)) {
                            continue;
                        }

                        var connectedIndex = GetStartTaskConnectedIndex(behaviorTree);
                        if (connectedIndex == -1 || connectedIndex == ushort.MaxValue) {
                            return;
                        }

                        var taskComponents = worlds[i].EntityManager.GetBuffer<TaskComponent>(entity);
                        var tagStableTypeHash = new ulong[taskComponents.Length];
                        for (int j = 0; j < taskComponents.Length; ++j) {
                            tagStableTypeHash[j] = TypeManager.GetTypeInfo(taskComponents[j].FlagComponentType.TypeIndex).StableTypeHash;
                        }
                        ulong[] ReevaluateFlagStableTypeHash = null;
                        if (worlds[i].EntityManager.HasBuffer<ReevaluateTaskComponent>(entity)) {
                            var reevaluateTaskComponents = worlds[i].EntityManager.GetBuffer<ReevaluateTaskComponent>(entity);
                            ReevaluateFlagStableTypeHash = new ulong[reevaluateTaskComponents.Length];
                            for (int j = 0; j < reevaluateTaskComponents.Length; ++j) {
                                ReevaluateFlagStableTypeHash[j] = TypeManager.GetTypeInfo(reevaluateTaskComponents[j].ReevaluateFlagComponentType.TypeIndex).StableTypeHash;
                            }
                        }

                        AddComponentObject<BakedBehaviorTree>(entity, new BakedBehaviorTree
                        {
                            StartEventConnectedIndex = connectedIndex,
                            StartEvaluation = behaviorTree.UpdateMode == UpdateMode.EveryFrame,
                            ReevaluateTaskSystems = GetTaskSystems<ReevaluateTaskSystemGroup>(worlds[i]),
                            InterruptTaskSystems = GetTaskSystems<InterruptTaskSystemGroup>(worlds[i]),
                            TraversalTaskSystems = GetTaskSystems<TraversalTaskSystemGroup>(worlds[i]),
                            TagStableTypeHashes = tagStableTypeHash,
                            ReevaluateFlagStableTypeHashes = ReevaluateFlagStableTypeHash,
                        });
                        behaviorTree.Baked = true;
                    }
                }
            }

            /// <summary>
            /// Returns the index of the node connection for the start event task.
            /// </summary>
            /// <param name="behaviorTree">The interested behavior tree.</param>
            /// <returns>The index of the node connection for the start event task.</returns>
            private int GetStartTaskConnectedIndex(BehaviorTree behaviorTree)
            {
                // The behavior tree has to first be initialized.
                if (behaviorTree.World == null || behaviorTree.Entity == Entity.Null) {
                    Debug.LogError($"Error: Unable to retrieve the connected index on behavior tree {behaviorTree}. The behavior tree has to first be initialized.");
                    return -1;
                }

                var data = behaviorTree.Data;
                for (int i = 0; i < data.EventNodes.Length; ++i) {
                    if (data.EventNodes[i].GetType() == typeof(Start)) {
                        // The connected index may not be valid.
                        if (data.EventNodes[i].ConnectedIndex == ushort.MaxValue) {
                            return ushort.MaxValue;
                        }

                        // The branch cannot start if it is disabled.
                        if (!behaviorTree.IsNodeEnabled(false, i)) {
                            return -1;
                        }

                        return data.LogicNodes[data.EventNodes[i].ConnectedIndex].RuntimeIndex;
                    }
                }
                return -1;
            }

            /// <summary>
            /// Returns all of the system type indicies within the systems of the specified type.
            /// </summary>
            /// <param name="world">The world that the systems were added to.</param>
            /// <returns>The system type indicies within the systems of the specified type (can be null).</returns>
            private string[] GetTaskSystems<T>(World world) where T : ComponentSystemGroup
            {
                var systemGroup = world.GetExistingSystemManaged<T>();
                if (systemGroup == null) {
                    return null;
                }

                var systems = systemGroup.GetAllSystems();
                if (systems.Length == 0) {
                    systems.Dispose();
                    return null;
                }

                // Use reflection to call WorldUnmanaged.GetTypeOfSystem. This method is only called during baking at edit time so reflection is ok, though it would be better
                // if this method was eventually made public.
                if (s_GetTypeOfSystemMethod == null) {
                    s_GetTypeOfSystemMethod = typeof(WorldUnmanaged).GetMethod("GetTypeOfSystem", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                    if (s_GetTypeOfSystemMethod == null) {
                        Debug.LogError("Error: Unable to find WorldUnmanaged.GetTypeOfSystem. Please email support@opsive.com with your Unity version and Entity package version.");
                        return null;
                    }
                }

                var systemTypes = new string[systems.Length];
                for (int i = 0; i < systems.Length; ++i) {
                    var systemTypeIndex = TypeManager.GetSystemTypeIndex((Type)s_GetTypeOfSystemMethod.Invoke(world.Unmanaged, new object[] { systems[i] }));
                    systemTypes[i] = systemTypeIndex.ToString();
                }
                systems.Dispose();
                return systemTypes;
            }
        }
    }
}
#endif