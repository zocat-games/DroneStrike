using UnityEditor;
using UnityEngine;
using Zocat;

[InitializeOnLoad]
public class IconUtility
{
    static IconUtility()
    {
        HierarchyIconDrawer.RegisterRule(go => go.GetComponent<CheckPoint>() != null, GetIcon(IconType.sv_icon_dot2_sml), "Has Checkpoint");
        HierarchyIconDrawer.RegisterRule(go => go.GetComponent<WeaponHolder>() != null, GetIcon(IconType.sv_icon_dot1_sml), "Has WeaponHolder");
    }

    public static Texture2D GetIcon(IconType iconType)
    {
        return EditorGUIUtility.IconContent(iconType.ToString()).image as Texture2D;
    }
}