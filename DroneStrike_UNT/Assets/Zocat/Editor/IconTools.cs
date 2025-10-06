using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Zocat
{
#if UNITY_EDITOR
    public static class IconTools
    {
        public static void SetIcon(GameObject gameObject, IconType iconType)
        {
            var icon = EditorGUIUtility.IconContent(iconType.ToString()).image as Texture2D;
            EditorGUIUtility.SetIconForObject(gameObject, icon);
            EditorUtility.SetDirty(gameObject);
        }

        public static void SetHierarchyIcon(GameObject go)
        {
            // var icon = GetBuiltinIcon(iconType.ToString());
            // if (go == null || icon == null) return;
            // var bindingFlags = BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.NonPublic;
            // var method = typeof(EditorGUIUtility).GetMethod("SetIconForObject", bindingFlags);
            // method.Invoke(null, new object[] { go, icon });
            // EditorApplication.RepaintHierarchyWindow();
            if (go == null)
            {
                Debug.LogError("SetHierarchyIcon: GameObject is null!");
                return;
            }

            var iconName = "sv_label_0";
            var tex = EditorGUIUtility.IconContent(iconName).image as Texture2D;
            if (tex == null)
            {
                Debug.LogError($"SetHierarchyIcon: Icon '{iconName}' not found!");
                return;
            }

            var method = typeof(EditorGUIUtility).GetMethod(
                "SetIconForObject",
                BindingFlags.Static | BindingFlags.NonPublic
            );

            if (method == null)
            {
                Debug.LogError("SetHierarchyIcon: Reflection failed, method not found!");
                return;
            }

            method.Invoke(null, new object[] { go, tex });
            EditorApplication.RepaintHierarchyWindow();
        }

        public static Texture2D GetBuiltinIcon(string name)
        {
            return EditorGUIUtility.IconContent(name).image as Texture2D;
        }

        // public static void SetIcon(GameObject gameObject, IconType iconType)
        // {
        //     if (iconType == IconType.None) iconType = IconType.tranp;
        //     var icon = EditorGUIUtility.IconContent(iconType.ToString()).image as Texture2D;
        //     if (iconType != IconType.None) EditorGUIUtility.SetIconForObject(gameObject, icon);
        //     EditorUtility.SetDirty(gameObject);
        // }
    }

#endif
    public enum IconType
    {
        None,
        sv_icon_dot15_pix16_gizmo,
        sv_icon_dot1_sml,
        sv_icon_dot4_sml,
        sv_icon_dot7_sml,
        sv_icon_dot5_pix16_gizmo,
        sv_icon_dot11_pix16_gizmo,
        sv_icon_dot12_sml,
        sv_icon_dot15_sml,
        sv_icon_dot9_pix16_gizmo,
        sv_icon_name6,
        sv_icon_name3,
        sv_icon_name4,
        sv_icon_name0,
        sv_icon_name1,
        sv_icon_name2,
        sv_icon_name5,
        sv_icon_name7,
        sv_icon_dot1_pix16_gizmo,
        sv_icon_dot8_pix16_gizmo,
        sv_icon_dot2_pix16_gizmo,
        sv_icon_dot6_pix16_gizmo,
        sv_icon_dot0_sml,
        sv_icon_dot3_sml,
        sv_icon_dot6_sml,
        sv_icon_dot9_sml,
        sv_icon_dot11_sml,
        sv_icon_dot14_sml,
        sv_label_0,
        sv_label_1,
        sv_label_2,
        sv_label_3,
        sv_label_5,
        sv_label_6,
        sv_label_7,
        sv_icon_none,
        sv_icon_dot14_pix16_gizmo,
        sv_icon_dot7_pix16_gizmo,
        sv_icon_dot3_pix16_gizmo,
        sv_icon_dot0_pix16_gizmo,
        sv_icon_dot2_sml,
        sv_icon_dot5_sml,
        sv_icon_dot8_sml,
        sv_icon_dot10_pix16_gizmo,
        sv_icon_dot12_pix16_gizmo,
        sv_icon_dot10_sml,
        sv_icon_dot13_sml,
        sv_icon_dot4_pix16_gizmo,
        sv_label_4,
        sv_icon_dot13_pix16_gizmo,
        tranp
    }
    // public enum IconType
    // {
    //     sv_icon_dot15_pix16_gizmo,
    //     sv_icon_dot1_sml,
    //     sv_icon_dot4_sml,
    //     sv_icon_dot7_sml,
    //     sv_icon_dot5_pix16_gizmo,
    //     sv_icon_dot11_pix16_gizmo,
    //     sv_icon_dot12_sml,
    //     sv_icon_dot15_sml,
    //     sv_icon_dot9_pix16_gizmo,
    //     sv_icon_name6,
    //     sv_icon_name3,
    //     sv_icon_name4,
    //     sv_icon_name0,
    //     sv_icon_name1,
    //     sv_icon_name2,
    //     sv_icon_name5,
    //     sv_icon_name7,
    //     sv_icon_dot1_pix16_gizmo,
    //     sv_icon_dot8_pix16_gizmo,
    //     sv_icon_dot2_pix16_gizmo,
    //     sv_icon_dot6_pix16_gizmo,
    //     sv_icon_dot0_sml,
    //     sv_icon_dot3_sml,
    //     sv_icon_dot6_sml,
    //     sv_icon_dot9_sml,
    //     sv_icon_dot11_sml,
    //     sv_icon_dot14_sml,
    //     sv_label_0,
    //     sv_label_1,
    //     sv_label_2,
    //     sv_label_3,
    //     sv_label_5,
    //     sv_label_6,
    //     sv_label_7,
    //     sv_icon_none,
    //     sv_icon_dot14_pix16_gizmo,
    //     sv_icon_dot7_pix16_gizmo,
    //     sv_icon_dot3_pix16_gizmo,
    //     sv_icon_dot0_pix16_gizmo,
    //     sv_icon_dot2_sml,
    //     sv_icon_dot5_sml,
    //     sv_icon_dot8_sml,
    //     sv_icon_dot10_pix16_gizmo,
    //     sv_icon_dot12_pix16_gizmo,
    //     sv_icon_dot10_sml,
    //     sv_icon_dot13_sml,
    //     sv_icon_dot4_pix16_gizmo,
    //     sv_label_4,
    //     sv_icon_dot13_pix16_gizmo,
    // }
}