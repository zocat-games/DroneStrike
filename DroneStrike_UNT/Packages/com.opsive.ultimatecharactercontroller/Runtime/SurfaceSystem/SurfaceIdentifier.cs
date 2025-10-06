/// ---------------------------------------------
/// Ultimate Character Controller
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

using UnityEngine;

namespace Opsive.UltimateCharacterController.SurfaceSystem
{
    /// <summary>
    ///     The SurfaceIdentifier can be added to GameObjects and identifies the type of surface the object belongs to.
    /// </summary>
    public class SurfaceIdentifier : MonoBehaviour
    {
        [Tooltip("The SurfaceType of the object.")]
        [SerializeField] protected SurfaceType m_SurfaceType;
        [Tooltip("Are decals allowed on this object?")]
        [SerializeField] protected bool m_AllowDecals;

        public SurfaceType SurfaceType
        {
            get => m_SurfaceType;
            set => m_SurfaceType = value;
        }

        public bool AllowDecals
        {
            get => m_AllowDecals;
            set => m_AllowDecals = value;
        }
    }
}