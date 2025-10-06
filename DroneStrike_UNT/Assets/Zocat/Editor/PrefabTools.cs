using UnityEditor;
using UnityEngine;

namespace Zocat
{
    public static class PrefabTools
    {
        private const string MenuAssetPath = "Tools/Zocat/";

        [MenuItem(MenuAssetPath + "Select Prefab Asset _F3", false)]
        private static void SelectPrefabAsset()
        {
            var ali = PrefabUtility.GetCorrespondingObjectFromSource(Selection.activeGameObject);
            Selection.activeGameObject = ali;
        }

        [MenuItem(MenuAssetPath + "Apply Selected Prefab Overrides _F4")]
        public static void ApplyAllOverrides()
        {
            var selected = Selection.activeGameObject;
            if (selected == null)
            {
                Debug.LogWarning("Lütfen bir prefab instance seçin.");
                return;
            }

            // Prefab instance mı?
            if (PrefabUtility.IsPartOfPrefabInstance(selected))
            {
                var prefabRoot = PrefabUtility.GetOutermostPrefabInstanceRoot(selected);
                var prefabAsset = PrefabUtility.GetCorrespondingObjectFromSource(prefabRoot);

                if (prefabAsset != null)
                {
                    PrefabUtility.ApplyPrefabInstance(
                        prefabRoot,
                        InteractionMode.UserAction
                    );

                    Debug.Log($"Tüm override'lar uygulandı: {prefabAsset.name}");
                }
            }
            else
            {
                Debug.LogWarning("Seçili nesne bir prefab instance değil.");
            }
        }

        [MenuItem(MenuAssetPath + "Unpack Prefab _F5", false)]
        private static void UnpackPrefab()
        {
            // var list = Selection.gameObjects;
            //
            // foreach (var item in list) Unpack(item);
            //
            // void Unpack(GameObject selectedObject)
            // {
            //     if (selectedObject == null)
            //     {
            //         Debug.LogWarning("No GameObject selected!");
            //         return;
            //     }
            //
            //     if (PrefabUtility.IsPartOfAnyPrefab(selectedObject))
            //     {
            //         // Undo kaydı ekle
            //         Undo.RegisterCompleteObjectUndo(selectedObject, "Unpack Prefab");
            //
            //         // Prefab bağlantısını kopar
            //         PrefabUtility.UnpackPrefabInstance(selectedObject, PrefabUnpackMode.Completely, InteractionMode.UserAction);
            //
            //         Debug.Log("Prefab unpacked: " + selectedObject.name);
            //     }
            //     else
            //     {
            //         Debug.LogWarning("Selected object is not a prefab instance!");
            //     }
            // }
            /*--------------------------------------------------------------------------------------*/
            var prefabRoot = PrefabUtility.GetOutermostPrefabInstanceRoot(Selection.activeGameObject);

            if (prefabRoot != null)
            {
                // Normal unpack (iç içe prefab bağlantılarını korur)
                PrefabUtility.UnpackPrefabInstance(
                    prefabRoot,
                    PrefabUnpackMode.OutermostRoot,
                    InteractionMode.AutomatedAction
                );

                Debug.Log($"{prefabRoot.name} prefab'ı unpack edildi.");
            }
            else
            {
                Debug.LogWarning("Bu obje bir prefab instance değil.");
            }
        }

        // public static void SavePrefabInEditMode(GameObject gameObject)
        // {
        //     IsoHelper.Log(gameObject);
        //     var prefabStage = UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage();
        //     if (prefabStage != null)
        //     {
        //         // Şu anda düzenlenen prefab asset yolu
        //         string assetPath = prefabStage.prefabAssetPath;
        //
        //         // Root nesneyi al (prefab içindeki en üst obje)
        //         GameObject root = prefabStage.prefabContentsRoot;
        //
        //         // Değişiklikleri asset'e kaydet
        //         PrefabUtility.SaveAsPrefabAsset(root, assetPath, out bool success);
        //
        //         if (success)
        //             Debug.Log($"Prefab kaydedildi: {assetPath}");
        //         else
        //             Debug.LogError("Prefab kaydedilemedi!");
        //     }
        // }
    }
}