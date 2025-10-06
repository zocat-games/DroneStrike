using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class HierarchyIconDrawer
{
    private static readonly List<Rule> rules = new();

    static HierarchyIconDrawer()
    {
        EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
    }

    public static void RegisterRule(Func<GameObject, bool> condition, Texture2D icon, string tooltip = "")
    {
        if (condition == null || icon == null) return;
        rules.Add(new Rule { Condition = condition, Icon = icon, Tooltip = tooltip });
        EditorApplication.RepaintHierarchyWindow();
    }

    private static void OnHierarchyGUI(int instanceID, Rect selectionRect)
    {
        var go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
        if (go == null || rules.Count == 0) return;

        foreach (var rule in rules)
            if (rule.Condition(go))
            {
                var iconRect = new Rect(selectionRect.xMax - 18, selectionRect.y, 16, 16);
                GUI.Label(iconRect, new GUIContent(rule.Icon, rule.Tooltip));
                break; // tek ikon çiz
            }
    }

    private class Rule
    {
        public Func<GameObject, bool> Condition;
        public Texture2D Icon;
        public string Tooltip;
    }
}