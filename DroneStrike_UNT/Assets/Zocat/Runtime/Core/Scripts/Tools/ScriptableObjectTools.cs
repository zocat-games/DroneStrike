using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Zocat
{
#if UNITY_EDITOR
    public static class ScriptableObjectTools
    {
        public static void Rename(ScriptableObject obj, string newName)
        {
            string assetPath = AssetDatabase.GetAssetPath(obj);
            string newFileName = newName + ".asset";
            string newPath = Path.Combine(Path.GetDirectoryName(assetPath), newFileName);

            if (assetPath != newPath)
            {
                AssetDatabase.RenameAsset(assetPath, newName);
                AssetDatabase.SaveAssets();
            }
        }


        public static string GetScriptableObjectGUID(ScriptableObject obj)
        {
            string assetPath = AssetDatabase.GetAssetPath(obj);
            if (obj == null)
            {
                Debug.LogError("ScriptableObject is null!");
                return null;
            }

            if (string.IsNullOrEmpty(assetPath))
            {
                Debug.LogError("Object is not an asset or not saved in the project!");
                return null;
            }

            string guid = AssetDatabase.AssetPathToGUID(assetPath);
            Debug.Log($"GUID: {guid}");
            return guid;
        }
    }
#endif
}