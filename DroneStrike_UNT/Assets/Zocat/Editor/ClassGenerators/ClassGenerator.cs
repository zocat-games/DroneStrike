using System;
using System.Collections;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Threading.Tasks;
using UnityTools.EditorUtility;
using Zocat;
using Object = UnityEngine.Object;

public class ClassGenerator : EditorWindow
{
    private enum ScriptType
    {
        UIPanel,
        InstanceBehaviour
    }

    private ScriptType selectedType = ScriptType.UIPanel;
    private string className = "NewClass";

    [MenuItem("Tools/Zocat/Class Generator")]
    public static void ShowWindow()
    {
        GetWindow<ClassGenerator>("Class");
    }

    private void OnGUI()
    {
        GUILayout.Label("Class", EditorStyles.boldLabel);

        className = EditorGUILayout.TextField("Class Name", className);

        GUILayout.Space(8);
        GUILayout.Label("Class Type:");

        // --- Vertical radio buttons ---
        DrawRadioButton("UIPanel", ScriptType.UIPanel);
        DrawRadioButton("InstanceBehaviour", ScriptType.InstanceBehaviour);

        GUILayout.Space(16);

        if (GUILayout.Button("Generate Class"))
            CreateScript();
        var e = Event.current;
        if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Return)
        {
            CreateScript();
            e.Use();
        }
    }

    private void DrawRadioButton(string label, ScriptType type)
    {
        bool isSelected = (selectedType == type);
        bool pressed = GUILayout.Toggle(isSelected, label, EditorStyles.radioButton);
        if (pressed && !isSelected)
        {
            selectedType = type;
            GUI.FocusControl(null); // focus kilitlenmesin
        }
    }

    private void CreateScript()
    {
        if (string.IsNullOrWhiteSpace(className))
        {
            EditorUtility.DisplayDialog("Hata", "Class name boş olamaz.", "Tamam");
            return;
        }

        string folderPath = EditorTools.GetSelectedPathOrFallback();
        string path = Path.Combine(folderPath, className + ".cs");

        if (File.Exists(path) &&
            !EditorUtility.DisplayDialog("Üzerine yazılsın mı?",
                $"{className}.cs mevcut. Üzerine yazılsın?", "Evet", "Hayır"))
            return;

        string template = selectedType == ScriptType.UIPanel
            ? $@"namespace Zocat
{{
    public class {className} : UIPanel
    {{
        
    }}
}}"
            : $@"namespace Zocat
{{
    public class {className} : InstanceBehaviour
    {{
        
    }}
}}";

        File.WriteAllText(path, template);
        IsoHelper.Log($"Class oluşturuldu:\n{path}");
        AssetDatabase.Refresh();
        // AsyncTools.RunAsync(AssetDatabase.Refresh, 2);
    }

    // Project penceresinde seçili klasör yoksa Assets'e yazar
    private string GetSelectedPathOrFallback()
    {
        string path = "Assets";
        foreach (Object obj in Selection.GetFiltered(typeof(Object), SelectionMode.Assets))
        {
            var p = AssetDatabase.GetAssetPath(obj);
            if (string.IsNullOrEmpty(p)) continue;

            if (File.Exists(p)) path = Path.GetDirectoryName(p);
            else path = p; // zaten klasör
            break;
        }

        return path;
    }
}