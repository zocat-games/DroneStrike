using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Zocat
{
    public static class CloneTools
    {
        public static T Instantiate<T>(string path, Transform parent = null) where T : Component
        {
            GameObject newObj;
            if (parent == null) newObj = GameObject.Instantiate(Resources.Load(path), parent) as GameObject;
            else newObj = GameObject.Instantiate(Resources.Load(path), parent.transform.position, Quaternion.identity, parent) as GameObject;
            newObj.transform.localPosition = Vector3.zero;
            return newObj.GetComponent<T>();
        }


        public static T Instantiate<T>(string path, Vector3 pos, Transform parent = null) where T : Component
        {
            var newObj = GameObject.Instantiate(Resources.Load(path), pos, Quaternion.identity, parent) as GameObject;
            if (parent != null) newObj.transform.parent = parent;
            return newObj.GetComponent<T>();
        }

        public static T InstantiateWithRefObject<T>(string path, Transform refObject) where T : Component
        {
            var newObj = GameObject.Instantiate(Resources.Load(path), refObject.position, refObject.rotation) as GameObject;
            return newObj.GetComponent<T>();
        }


        public static T InstantiateRandom<T>(string path, Transform parent = null) where T : Component
        {
            var objects = Resources.LoadAll(path, typeof(GameObject));
            GameObject randomPrefab = objects[UnityEngine.Random.Range(0, objects.Length)] as GameObject;
            var newObj = GameObject.Instantiate(randomPrefab, parent);
            newObj.transform.localPosition = Vector3.zero;
            return newObj.GetComponent<T>();
        }

        public static T InstantiatePrefab<T>(T source, Transform parent = null) where T : Component
        {
            var newObj = GameObject.Instantiate(source.gameObject, parent);
            return newObj.GetComponent<T>();
        }

        /*--------------------------------------------------------------------------------------*/

        public static void DestroyImmediate(this GameObject gameObject)
        {
            if (gameObject != null) GameObject.DestroyImmediate(gameObject);
        }

#if UNITY_EDITOR
        public static GameObject InstantiatePrefab(string path)
        {
            GameObject prefab = Resources.Load(path) as GameObject;
            return (GameObject)PrefabUtility.InstantiatePrefab(prefab);
        }

        public static T InstantiatePrefab<T>(string path) where T : Component
        {
            GameObject prefab = Resources.Load(path) as GameObject;
            var tmp = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            return tmp.GetComponent<T>();
        }
#endif
    }
}