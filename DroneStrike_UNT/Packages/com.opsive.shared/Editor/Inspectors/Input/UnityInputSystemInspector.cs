﻿#if ENABLE_INPUT_SYSTEM
/// ---------------------------------------------
/// Opsive Shared
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------
namespace Opsive.Shared.Editor.Inspectors.Input
{
    using Opsive.Shared.Input.InputSystem;
    using UnityEditor;

    /// <summary>
    /// Shows a custom inspector for the UnityInputSystem.
    /// </summary>
    [CustomEditor(typeof(UnityInputSystem))]
    public class UnityInputSystemInspector : PlayerInputInspector
    {
        /// <summary>
        /// Draws all of the fields related to input.
        /// </summary>
        protected override void DrawInputFields()
        {
            EditorGUILayout.PropertyField(PropertyFromName("m_ForceInput"));
            EditorGUILayout.PropertyField(PropertyFromName("m_DisableCursor"));
            var disableCursorProperty = PropertyFromName("m_EnableCursorWithEscape");
            EditorGUILayout.PropertyField(disableCursorProperty);
            if (disableCursorProperty.boolValue) {
                EditorGUILayout.PropertyField(PropertyFromName("m_PreventLookVectorChanges"));
            }
        }
    }
}
#endif