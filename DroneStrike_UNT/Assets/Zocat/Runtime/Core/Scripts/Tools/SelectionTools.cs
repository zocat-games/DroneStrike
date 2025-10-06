using System.Collections;
using UnityEngine;

namespace Zocat
{
    public static class SelectionTools
    {
        public static T[] GetComponentsInChildrenExcludeSelf<T>(this GameObject gameObject) where T : Component
        {
            T[] childRenderers = gameObject.GetComponentsInChildren<T>();
            childRenderers = System.Array.FindAll(childRenderers, r => r.gameObject != gameObject);
            return childRenderers;
        }
    }
}