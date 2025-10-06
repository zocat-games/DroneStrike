using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngineInternal;
using UnityTools.EditorUtility;
using Zocat;
using Object = UnityEngine.Object;

public class SingletonGenerator : EditorWindow
{
    public bool CreateGameObject;
    private readonly bool MonoSingleton = true;
    private string className = "NewComponent";
    private GameObject go;

    private void OnGUI()
    {
        GUILayout.Label("...", EditorStyles.boldLabel);
        className = EditorGUILayout.TextField("Name", className);
        // MonoSingleton = EditorGUILayout.Toggle("MonoSingleton", MonoSingleton);
        CreateGameObject = EditorGUILayout.Toggle("CreateGameObject", CreateGameObject);
        /*--------------------------------------------------------------------------------------*/
        if (GUILayout.Button("Create"))
        {
            GUIUtility.systemCopyBuffer = className;
            if (string.IsNullOrWhiteSpace(className))
            {
                Debug.LogError("Can'not be empty.");
                return;
            }

            var targetFolder = GetSelectedFolderPath();
            CreateScriptFile(className, targetFolder);
            AssetDatabase.Refresh();

            if (CreateGameObject)
                EditorApplication.delayCall += () => { CreateGameObjectWithComponent(className, targetFolder); };
        }

        if (GUILayout.Button("Add Component") && CreateGameObject)
        {
            APIUpdaterRuntimeServices.AddComponent(go, "Assets/Zocat/Editor/ClassCreator.cs (38,13)", className);
            IsoHelper.Log("Component added");
        }
    }

    private Type FindComponentType(string typeName)
    {
        // Search through all loaded assemblies
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            var type = assembly.GetType(typeName);
            if (type != null && type.IsSubclassOf(typeof(Component))) return type;
        }

        return null;
    }

    [MenuItem("Tools/Zocat/Singleton Generator")]
    public static void ShowWindow()
    {
        GetWindow<SingletonGenerator>("Singleton");
    }

    private string GetSelectedFolderPath()
    {
        var defaultPath = "Assets/Scripts/Generated";

        var obj = Selection.activeObject;
        var path = AssetDatabase.GetAssetPath(obj);

        if (string.IsNullOrEmpty(path))
            return defaultPath;

        if (File.Exists(path))
            path = Path.GetDirectoryName(path);

        if (Directory.Exists(path))
            return path;

        return defaultPath;
    }

    private void CreateScriptFile(string name, string folderPath)
    {
        // var tempPath = "Assets/Zocat/Runtime/Movie/Scripts/Managers";
        var tempPath = EditorTools.GetSelectedPathOrFallback();
        var filePath = Path.Combine(tempPath, name + ".cs");

        /*--------------------------------------------------------------------------------------*/
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        if (File.Exists(filePath))
        {
            Debug.LogWarning($"{name}.cs zaten var.");
            return;
        }

        var folder = AssetDatabase.LoadAssetAtPath<Object>(tempPath);
        Selection.activeObject = folder;
        /*--------------------------------------------------------------------------------------*/
//         string instanceTemplate =
//             $@"using UnityEngine;
// namespace Zocat
// {{
// public class {name} : InstanceBehaviour
//     {{
//
//     }}
// }}
// ";

        var monoSingletonTemplate =
            $@"using UnityEngine;
namespace Zocat
{{
public class {name} : MonoSingleton<{name}>
    {{

    }}
}}
";
        var selectedTemplate = monoSingletonTemplate;
        File.WriteAllText(filePath, selectedTemplate);
        if (MonoSingleton)
        {
            var textToAdd = TextToAdd(name);
            Add(textToAdd);
        }

        Debug.Log($"{className} created.");
        AssetDatabase.Refresh();
    }

    private void CreateGameObjectWithComponent(string name, string folderPath)
    {
        go = new GameObject(name);
        // go.transform.parent = GameObject.Find("GameManager").transform;
        var relativePath = folderPath.Replace(Application.dataPath, "Assets");
        var scriptPath = Path.Combine(relativePath, name + ".cs").Replace("\\", "/");

        var script = (MonoScript)AssetDatabase.LoadAssetAtPath(scriptPath, typeof(MonoScript));
        if (script != null)
        {
            var scriptClass = script.GetClass();
            if (scriptClass != null && scriptClass.IsSubclassOf(typeof(MonoBehaviour)))
                go.AddComponent(scriptClass);
            else
                Debug.LogWarning("Script sınıfı henüz derlenmedi. Elle component ekleyin.");
        }
    }

    public void Add(string coming)
    {
        var filePath = "Assets/Zocat/Runtime/Core/Scripts/Core/InstanceBehaviour.cs";
        if (File.Exists(filePath))
        {
            var content = File.ReadAllText(filePath);

            // Son '}' karakterinden önce kodu yerleştir
            var lastBrace = content.LastIndexOf('}'); // namespace '}'
            var classBrace = content.LastIndexOf('}', lastBrace - 1); // class '}'

            if (classBrace != -1)
            {
                content = content.Insert(classBrace, coming);
                File.WriteAllText(filePath, content);
                AssetDatabase.Refresh();
                Debug.Log("Kod class'ın içine eklendi!");
            }
            else
            {
                Debug.LogError("Dosyada '}' bulunamadı!");
            }
        }
        else
        {
            Debug.LogError("Dosya bulunamadı: " + filePath);
        }
    }

    private string TextToAdd(string className)
    {
        return $"public {className} {className} => {className}.Instance;\n";
    }
}

public static class ComponentAdder
{
    public static void AddComponentByName(GameObject go, string className)
    {
        // Assembly-CSharp içinde arıyoruz
        var type = Type.GetType(className + ",Assembly-CSharp");

        if (type == null)
        {
            Debug.LogError($"Type not found: {className}");
            return;
        }

        if (!typeof(Component).IsAssignableFrom(type))
        {
            Debug.LogError($"Type is not a Component: {className}");
            return;
        }

        go.AddComponent(type);
        Debug.Log($"Component added: {className}");
    }
}