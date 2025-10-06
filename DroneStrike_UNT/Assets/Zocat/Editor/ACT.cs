using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;

public static class ACT
{
    public static void DrawIcon(GameObject gameObject, int idx)
    {
        var largeIcons = GetTextures("sv_label_", string.Empty, 0, 8);
        var icon = largeIcons[idx];
        var egu = typeof(EditorGUIUtility);
        var flags = BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.NonPublic;
        var args = new object[] { gameObject, icon.image };
        var setIcon = egu.GetMethod("SetIconForObject", flags, null, new Type[] { typeof(UnityEngine.Object), typeof(Texture2D) }, null);
        setIcon.Invoke(null, args);
    }

    private static GUIContent[] GetTextures(string baseName, string postFix, int startIndex, int count)
    {
        GUIContent[] array = new GUIContent[count];
        for (int i = 0; i < count; i++)
        {
            array[i] = EditorGUIUtility.IconContent(baseName + (startIndex + i) + postFix);
        }

        return array;
    }

    /****************************************************************************************/

    // [MenuItem("GameObject/Set Active Recursively")]
    // public static void ToggleActive()
    // {
    //     if (Selection.activeTransform == null)
    //         return;
    //
    //     ToggleActiveRecursively(Selection.activeTransform, !Selection.activeGameObject.activeSelf);
    // }
    //
    // static void ToggleActiveRecursively(Transform trans, bool active)
    // {
    //     trans.gameObject.SetActive(active);
    //     foreach (Transform child in trans)
    //         ToggleActiveRecursively(child, active);
    // }
    //
    // [MenuItem("Example/Rotate Green +45 _g")]
    // static void RotateGreenPlus45()
    // {
    //     GameObject obj = Selection.activeGameObject;
    //     obj.transform.Rotate(Vector3.up * 45);
    // }
    //
    // [MenuItem("Example/Rotate Green +45 _g", true)]
    // static bool ValidatePlus45()
    // {
    //     return Selection.activeGameObject != null;
    // }
    //
    // [MenuItem("Example/Rotate green -45 #g")]
    // static void RotateGreenMinus45()
    // {
    //     GameObject obj = Selection.activeGameObject;
    //     obj.transform.Rotate(Vector3.down * 45);
    // }
    //
    // [MenuItem("Example/Rotate green -45 #g", true)]
    // static bool ValidateMinus45()
    // {
    //     return Selection.activeGameObject != null;
    // }
}