﻿#if GRAPH_DESIGNER
/// ---------------------------------------------
/// Behavior Designer
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------
namespace Opsive.BehaviorDesigner.Runtime.Tasks.Composites
{
    using Opsive.BehaviorDesigner.Runtime.Components;
    using Opsive.BehaviorDesigner.Runtime.Utility;
    using Opsive.GraphDesigner.Runtime;
    using Unity.Burst;
    using Unity.Collections;
    using Unity.Entities;
    using Unity.Jobs;
    using Unity.Mathematics;
    using UnityEngine;

    /// <summary>
    /// A node representation of the parallel task.
    /// </summary>
    [NodeIcon("f612c025389b22640b1b6df88f4502e7", "8a4a401bcfb527a48a08351efaf92e14")]
    [Opsive.Shared.Utility.Description("Similar to the sequence task, the parallel task will run each child task until a child task returns failure. " +
                     "The parallel task will run all of its children tasks simultaneously versus running each task one at a time. " +
                     "Like the sequence class, the parallel task will return success once all of its children tasks have return success. " +
                     "If one tasks returns failure the parallel task will end all of the child tasks and return failure.")]
    public class Parallel : ECSCompositeTask<ParallelTaskSystem, ParallelComponent>, IParentNode, IParallelNode
    {
        public override ComponentType Flag { get => typeof(ParallelFlag); }

        /// <summary>
        /// Adds the IBufferElementData to the entity.
        /// </summary>
        /// <param name="world">The world that the entity exists in.</param>
        /// <param name="entity">The entity that the IBufferElementData should be assigned to.</param>
        /// <param name="gameObject">The GameObject that the entity is attached to.</param>
        /// <returns>The index of the element within the buffer.</returns>
        public override int AddBufferElement(World world, Entity entity, GameObject gameObject)
        {
            var index = base.AddBufferElement(world, entity, gameObject);
            ComponentUtility.AddInterruptComponents(world.EntityManager, entity);
            return index;
        }

        /// <summary>
        /// Returns a new TBufferElement for use by the system.
        /// </summary>
        /// <returns>A new TBufferElement for use by the system.</returns>
        public override ParallelComponent GetBufferElement()
        {
            return new ParallelComponent()
            {
                Index = RuntimeIndex
            };
        }
    }

    /// <summary>
    /// The DOTS data structure for the Parallel class.
    /// </summary>
    public struct ParallelComponent : IBufferElementData
    {
        [Tooltip("The index of the node.")]
        [SerializeField] ushort m_Index;

        public ushort Index { get => m_Index; set => m_Index = value; }
    }

    /// <summary>
    /// A DOTS tag indicating when a Parallel node is active.
    /// </summary>
    public struct ParallelFlag : IComponentData, IEnableableComponent { }

    /// <summary>
    /// Runs the Parallel logic.
    /// </summary>
    [DisableAutoCreation]
    public partial struct ParallelTaskSystem : ISystem
    {
        private EntityQuery m_Query;
        private EntityCommandBuffer m_EntityCommandBuffer;
        private JobHandle m_Dependency;

        /// <summary>
        /// Builds the query.
        /// </summary>
        /// <param name="state">THe current SystemState.</param>
        private void OnCreate(ref SystemState state)
        {
            m_Query = SystemAPI.QueryBuilder().WithAllRW<BranchComponent>().WithAllRW<TaskComponent>().WithAllRW<ParallelComponent>().WithAll<ParallelFlag, EvaluateFlag>().Build();
        }

        /// <summary>
        /// Creates the job.
        /// </summary>
        /// <param name="state">The current state of the system.</param>
        [BurstCompile]
        private void OnUpdate(ref SystemState state)
        {
            m_Dependency.Complete();
            if (m_EntityCommandBuffer.IsCreated) {
                m_EntityCommandBuffer.Playback(state.EntityManager);
                m_EntityCommandBuffer.Dispose();
            }

            m_EntityCommandBuffer = new EntityCommandBuffer(Allocator.TempJob);
            state.Dependency = new ParallelJob()
            {
                EntityCommandBuffer = m_EntityCommandBuffer.AsParallelWriter()
            }.ScheduleParallel(m_Query, state.Dependency);
            m_Dependency = state.Dependency;
        }

        /// <summary>
        /// The system has been destroyed.
        /// </summary>
        /// <param name="state">The current state of the system.</param>
        public void OnDestroy(ref SystemState state)
        {
            if (m_EntityCommandBuffer.IsCreated) {
                m_EntityCommandBuffer.Playback(state.EntityManager);
                m_EntityCommandBuffer.Dispose();
            }
        }

        /// <summary>
        /// Job which executes the task logic.
        /// </summary>
        [BurstCompile]
        private partial struct ParallelJob : IJobEntity
        {
            [Tooltip("CommandBuffer which sets the component data.")]
            public EntityCommandBuffer.ParallelWriter EntityCommandBuffer;

            /// <summary>
            /// Executes the parallel logic.
            /// </summary>
            /// <param name="entity">The entity that is being acted upon.</param>
            /// <param name="entityIndex">The index of the entity.</param>
            /// <param name="parallelComponents">An array of ParallelComponents.</param>
            /// <param name="taskComponents">An array of TaskComponents.</param>
            /// <param name="branchComponents">An array of BranchComponents.</param>
            [BurstCompile]
            public void Execute(Entity entity, [EntityIndexInQuery] int entityIndex, ref DynamicBuffer<ParallelComponent> parallelComponents, ref DynamicBuffer<TaskComponent> taskComponents, ref DynamicBuffer<BranchComponent> branchComponents)
            {
                for (int i = 0; i < parallelComponents.Length; ++i) {
                    var parallelComponent = parallelComponents[i];
                    var taskComponent = taskComponents[parallelComponent.Index];
                    var branchComponent = branchComponents[taskComponent.BranchIndex];

                    // Do not continue if there will be an interrupt.
                    if (branchComponent.InterruptType != InterruptType.None) {
                        continue;
                    }

                    ushort childIndex;
                    TaskComponent childTaskComponent;
                    if (taskComponent.Status == TaskStatus.Queued) {
                        taskComponent.Status = TaskStatus.Running;
                        taskComponents[taskComponent.Index] = taskComponent;

                        childIndex = (ushort)(parallelComponent.Index + 1);
                        while (childIndex != ushort.MaxValue) {
                            childTaskComponent = taskComponents[childIndex];
                            childTaskComponent.Status = TaskStatus.Queued;
                            taskComponents[childIndex] = childTaskComponent;

                            var childBranchComponent = branchComponents[childTaskComponent.BranchIndex];
                            childBranchComponent.NextIndex = childTaskComponent.Index;
                            branchComponents[childTaskComponent.BranchIndex] = childBranchComponent;

                            childIndex = taskComponents[childIndex].SiblingIndex;
                        }
                    } else if (taskComponent.Status != TaskStatus.Running) {
                        continue;
                    }

                    var childrenFailure = false;
                    var childrenRunning = false;
                    childIndex = (ushort)(parallelComponent.Index + 1);
                    while (childIndex != ushort.MaxValue) {
                        childTaskComponent = taskComponents[childIndex];
                        if (childTaskComponent.Status == TaskStatus.Queued || childTaskComponent.Status == TaskStatus.Running) {
                            childrenRunning = true;
                        } else if (childTaskComponent.Status == TaskStatus.Failure) {
                            childrenFailure = true;

                            var childBranchComponent = branchComponents[childTaskComponent.BranchIndex];
                            childBranchComponent.NextIndex = ushort.MaxValue;
                            branchComponents[childTaskComponent.BranchIndex] = childBranchComponent;
                            break;
                        } else if (childTaskComponent.Status == TaskStatus.Success) {
                            var childBranchComponent = branchComponents[childTaskComponent.BranchIndex];
                            if (childBranchComponent.ActiveIndex != ushort.MaxValue) {
                                childBranchComponent.NextIndex = ushort.MaxValue;
                                branchComponents[childTaskComponent.BranchIndex] = childBranchComponent;
                            }
                        }
                        childIndex = taskComponents[childIndex].SiblingIndex;
                    }

                    // If a single child fails then all tasks should be stopped.
                    if (childrenFailure) {
                        var maxChildIndex = taskComponent.Index + TraversalUtility.GetChildCount(taskComponent.Index, ref taskComponents);
                        for (ushort j = (ushort)(taskComponent.Index + 1); j <= maxChildIndex; ++j) {
                            childTaskComponent = taskComponents[j];
                            if (childTaskComponent.Status == TaskStatus.Running || childTaskComponent.Status == TaskStatus.Queued) {
                                childTaskComponent.Status = TaskStatus.Failure;
                                taskComponents[j] = childTaskComponent;

                                branchComponent = branchComponents[childTaskComponent.BranchIndex];
                                EntityCommandBuffer.SetComponentEnabled<InterruptedFlag>(entityIndex, entity, true);
                                if (branchComponent.ActiveIndex == childTaskComponent.Index) {
                                    branchComponent.NextIndex = ushort.MaxValue;
                                    branchComponents[childTaskComponent.BranchIndex] = branchComponent;
                                }
                            }
                        }

                        branchComponent.NextIndex = taskComponent.ParentIndex;
                        branchComponents[taskComponent.BranchIndex] = branchComponent;
                        taskComponent.Status = TaskStatus.Failure;
                        taskComponents[taskComponent.Index] = taskComponent;

                        continue;
                    }

                    if (childrenRunning) {
                        continue;
                    }

                    // No more children are running. Resume the parent task.
                    taskComponent.Status = TaskStatus.Success;
                    taskComponents[taskComponent.Index] = taskComponent;

                    branchComponent.NextIndex = taskComponent.ParentIndex;
                    branchComponents[taskComponent.BranchIndex] = branchComponent;
                }
            }
        }
    }
}
#endif