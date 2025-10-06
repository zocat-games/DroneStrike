using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.ShortcutManagement;
using UnityEngine;
using Zocat;
using Object = UnityEngine.Object;

namespace UnityTools.EditorUtility
{
    public static class EditorTools
    {
        private const string MenuAssetPath = "Tools/Zocat/";
        private const int Priority = 2000;
        /*--------------------------------------------------------------------------------------*/

        private static GameObject _selected => Selection.activeGameObject;

        [MenuItem(MenuAssetPath + "Copy Full Path %&c", false, Priority + 2)]
        private static void CopyFullPath()
        {
            var guids = Selection.assetGUIDs;

            var assetPath = Path.Combine(Application.dataPath,
                AssetDatabase.GUIDToAssetPath(guids[0]).Replace("Assets/", string.Empty));
            EditorGUIUtility.systemCopyBuffer = assetPath;
        }

        [MenuItem(MenuAssetPath + "Create Cube #&c", false)]
        private static void CreateCube()
        {
            GameObject.CreatePrimitive(PrimitiveType.Cube);
        }

        [MenuItem(MenuAssetPath + "Reset Position #&w", false)]
        private static void ResetPosition()
        {
            foreach (var _Item in Selection.transforms) _Item.position = Vector3.zero;
        }

        [MenuItem(MenuAssetPath + "Reset Rotation #&e", false)]
        private static void ResetRotation()
        {
            foreach (var _Item in Selection.transforms) _Item.eulerAngles = Vector3.zero;
        }

        [MenuItem(MenuAssetPath + "Reset Scale #&r", false)]
        private static void ResetScale()
        {
            foreach (var _Item in Selection.transforms) _Item.localScale = Vector3.one;
        }


        // [MenuItem(MenuAssetPath + "Create Parent _F10", false)]
        // private static void CreateParent()
        // {
        //     foreach (var _Item in Selection.transforms)
        //     {
        //         var selection = _Item;
        //         var go = new GameObject();
        //         go.transform.parent = selection.parent;
        //         go.transform.position = selection.transform.position;
        //         go.transform.rotation = selection.transform.rotation;
        //         selection.transform.parent = go.transform;
        //         go.name = "Main_" + selection.name;
        //     }
        // }

        [MenuItem(MenuAssetPath + "Exit Prefab _F10", false)]
        private static void ExitPrefabMode()
        {
            StageUtility.GoBackToPreviousStage();
        }


        [MenuItem(MenuAssetPath + "Clear All", false)]
        private static void ClearAllSaves()
        {
            ES3.DeleteFile("SaveFile.es3");
            PlayerPrefs.DeleteAll();
            IsoHelper.Log("All cleared...");
        }

        /*--------------------------------------------------------------------------------------*/
        [Shortcut("Tools/Clear Console", KeyCode.F1, ShortcutModifiers.Action)]
        public static void ClearConsole()
        {
            var logEntries = Type.GetType("UnityEditor.LogEntries, UnityEditor.dll");
            var clearMethod = logEntries.GetMethod("Clear", BindingFlags.Static | BindingFlags.Public);
            clearMethod.Invoke(null, null);
        }


        [Shortcut("Tools/Go To Raycast Ground", KeyCode.F3, ShortcutModifiers.Action)]
        public static void GoToRaycastGround()
        {
            foreach (var obj in Selection.gameObjects)
            {
                var t = obj.transform;

                if (Physics.Raycast(t.position, Vector3.down, out var hit, 1000f))
                {
                    Undo.RecordObject(t, "Snap To Ground");
                    t.position = hit.point;
                    Debug.Log($"{t.name} snapped to {hit.point}");
                }
                else
                {
                    Debug.Log($"{t.name}: no ground detected below!");
                }
            }
        }

        [Shortcut("Tools/Zocat/LocalPosZero", KeyCode.F2, ShortcutModifiers.Action)]
        public static void LocalPosZero()
        {
            foreach (var item in Selection.gameObjects)
                // var rnd = Random.Range(0, 180);
                item.transform.localPosition = Vector3.zero;
            // if (Physics.Raycast(_selected.transform.position, Vector3.down, out var hit))
            // {
            // _selected.transform.pos
            // transformto
            // }
        }

        [Shortcut("Tools/Zocat/Unparent", KeyCode.F7)]
        public static void ParentToLast()
        {
            foreach (var item in Helper.Selections)
                if (item != Helper.Selections.Last())
                    item.transform.parent = Helper.Selections.Last().transform;
        }


        [Shortcut("Tools/Zocat/Refresh Unit Assets", KeyCode.F8)]
        public static void Refresh()
        {
            AssetDatabase.Refresh();
            Debug.Log("Unit assets refreshed!");
        }


        /*--------------------------------------------------------------------------------------*/

        public static string GetSelectedPathOrFallback()
        {
            var path = "Assets";
            foreach (var obj in Selection.GetFiltered(typeof(Object), SelectionMode.Assets))
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
}