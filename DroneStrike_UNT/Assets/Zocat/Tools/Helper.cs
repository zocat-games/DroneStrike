using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Zocat
{
    public static class Helper
    {
#if UNITY_EDITOR
        public static GameObject[] Selections => Selection.gameObjects;
        public static GameObject Selected => Selection.activeGameObject;
#endif
    }
}