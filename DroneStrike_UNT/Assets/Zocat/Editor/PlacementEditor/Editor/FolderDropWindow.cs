using System.Collections;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

namespace Zocat
{
    using UnityEngine;
    using UnityEditor;

    public class FolderDropWindow : OdinEditorWindow
    {
        [Button(ButtonSizes.Medium)]
        public void Do()
        {
            GetSelectedFolderPath();
        }

        [MenuItem("Tools/Get Selected Folder Path")]
        public static void GetSelectedFolderPath()
        {
            Object selected = Selection.activeObject;

            if (selected == null)
            {
                Debug.LogWarning("Hiçbir şey seçili değil.");
                return;
            }

            string path = AssetDatabase.GetAssetPath(selected);

            if (AssetDatabase.IsValidFolder(path))
            {
                Debug.Log("Seçilen klasörün path'i: " + path);
            }
            else
            {
                Debug.LogWarning("Seçili nesne bir klasör değil: " + path);
            }
        }
    }
}