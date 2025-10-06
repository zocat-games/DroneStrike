using System.IO;
using UnityEditor;
using UnityEngine;

public class AppendCodeToClass : EditorWindow
{
    private string filePath;

    private void OnGUI()
    {
        GUILayout.Label("Class'ın Dibine Kod Ekle", EditorStyles.boldLabel);

        filePath = EditorGUILayout.TextField("Dosya Yolu:", filePath);
        // codeToAppend = EditorGUILayout.TextArea(codeToAppend, GUILayout.Height(80));

        if (GUILayout.Button("Ekle")) Add("//Ali\n");
        // if (File.Exists(filePath))
        // {
        //     string content = File.ReadAllText(filePath);
        //
        //     // Son '}' karakterinden önce kodu yerleştir
        //     int lastBrace = content.LastIndexOf('}'); // namespace '}'
        //     int classBrace = content.LastIndexOf('}', lastBrace - 1); // class '}'
        //
        //     if (classBrace != -1)
        //     {
        //         content = content.Insert(classBrace, codeToAppend);
        //         File.WriteAllText(filePath, content);
        //         AssetDatabase.Refresh();
        //         Debug.Log("Kod class'ın içine eklendi!");
        //     }
        //     else
        //     {
        //         Debug.LogError("Dosyada '}' bulunamadı!");
        //     }
        // }
        // else
        // {
        //     Debug.LogError("Dosya bulunamadı: " + filePath);
        // }
    }
    // private string codeToAppend = "\n    // Yeni eklenen kod\n    public void TestMethod() { Debug.Log(\"Hello\"); }\n";


    [MenuItem("Tools/Append Code To Class")]
    public static void ShowWindow()
    {
        GetWindow<AppendCodeToClass>("Append Code");
    }

    public static void Add(string coming)
    {
        var filePath = "Assets/Zocat/Editor/_Etc/Ali.cs";
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
}