using UnityEngine;

namespace Zocat
{
    using UnityEditor;
    using UnityEngine;

    public class PasteShortcutWindow : EditorWindow
    {
        [MenuItem("Tools/Trigger Paste")]
        static void Open()
        {
            GetWindow<PasteShortcutWindow>("Paste Trigger");
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Paste (Ctrl/Cmd + V)"))
            {
                EditorApplication.ExecuteMenuItem("Edit/Paste");
                Debug.Log("Unity paste komutu tetiklendi.");
            }
        }
    }
}