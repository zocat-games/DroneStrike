using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;

namespace Zocat
{
    public class TestGenerator : EditorWindow
    {
        private bool generateClass;
        private bool generateScene;
        private string testName = "NewScript";

        [MenuItem("Tools/Zocat/Test Generator")]
        public static void ShowWindow()
        {
            GetWindow<TestGenerator>("Test");
        }

        void OnGUI()
        {
            GUILayout.Label("Create MonoBehaviour Script & Scene", EditorStyles.boldLabel);

            testName = EditorGUILayout.TextField("Test Name", testName);
            generateClass = EditorGUILayout.Toggle("Generate Class", generateClass);
            generateScene = EditorGUILayout.Toggle("Generate Scene", generateScene);

            if (GUILayout.Button("Create Folder"))
            {
                CreateFolderAndAssets(testName);
            }
        }

        void CreateFolderAndAssets(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Debug.LogError("Name cannot be empty.");
                return;
            }

            string basePath = "Assets/Zocat/Runtime/Movie/_Etc/_Test";
            string fullFolderPath = Path.Combine(basePath, name);
            string scriptPath = Path.Combine(fullFolderPath, name + ".cs");
            string scenePath = Path.Combine(fullFolderPath, name + ".unity");

            CreateFolderIfNotExists(basePath);

            if (!AssetDatabase.IsValidFolder(fullFolderPath))
            {
                AssetDatabase.CreateFolder(basePath, name);
            }

            // === Script oluşturma ===
            if (generateClass)
            {
                if (File.Exists(scriptPath))
                {
                    Debug.LogWarning("Script already exists at: " + scriptPath);
                }
                else
                {
                    string scriptContent = $@"using UnityEngine;
namespace Zocat
{{
    public class {name} : InstanceBehaviour
    {{
    
    }}
}}";
                    File.WriteAllText(scriptPath, scriptContent);
                    Debug.Log($"Created script at: {scriptPath}");
                }
            }

            // === Scene oluşturma ===
            if (generateScene)
            {
                if (File.Exists(scenePath))
                {
                    Debug.LogWarning("Scene already exists at: " + scenePath);
                }
                else
                {
                    var newScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
                    EditorSceneManager.SaveScene(newScene, scenePath);
                    Debug.Log($"Created scene at: {scenePath}");
                }
            }

            AssetDatabase.Refresh();

            // Eğer script varsa otomatik seç
            if (generateClass && File.Exists(scriptPath))
            {
                Object scriptAsset = AssetDatabase.LoadAssetAtPath<MonoScript>(scriptPath);
                Selection.activeObject = scriptAsset;
            }
            else if (generateScene && File.Exists(scenePath))
            {
                Object sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);
                Selection.activeObject = sceneAsset;
            }
        }

        // Helper: klasör yolunu adım adım oluştur
        void CreateFolderIfNotExists(string path)
        {
            string[] parts = path.Split('/');
            string current = parts[0];

            for (int i = 1; i < parts.Length; i++)
            {
                string next = parts[i];
                string combined = current + "/" + next;
                if (!AssetDatabase.IsValidFolder(combined))
                {
                    AssetDatabase.CreateFolder(current, next);
                }

                current = combined;
            }
        }
    }
}