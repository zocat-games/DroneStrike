using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Zocat
{
    public class SceneHelpers : InstanceBehaviour
    {
#if UNITY_EDITOR
        public static GameObject[] selections => Selection.gameObjects;
        public static GameObject _selected => Selection.activeGameObject;
#endif
        /*--------------------------------------------------------------------------------------*/
        [ListDrawerSettings(ShowPaging = false)]
        [BoxGroup("Rename")] public List<GameObject> GameObjects;
        [BoxGroup("Rename")] public string TargetName;

        [Button(ButtonSizes.Medium)]
        public void Rename()
        {
            for (var i = 0; i < GameObjects.Count; i++)
            {
                var item = GameObjects[i];
                item.name = $"{TargetName}{i}";
            }
        }

        /*--------------------------------------------------------------------------------------*/
    }
}