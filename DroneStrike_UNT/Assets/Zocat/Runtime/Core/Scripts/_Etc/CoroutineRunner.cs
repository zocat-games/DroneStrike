using System.Collections;
using UnityEngine;

namespace Zocat
{
    using UnityEngine;
    using System.Collections;

    public class CoroutineRunner : MonoBehaviour
    {
        private static CoroutineRunner instance;

        public static CoroutineRunner Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject obj = new GameObject("CoroutineRunner");
                    instance = obj.AddComponent<CoroutineRunner>();
                    DontDestroyOnLoad(obj); // Sahne değişse bile çalışmaya devam etsin
                }

                return instance;
            }
        }

        public void StartRoutine(IEnumerator coroutine)
        {
            StartCoroutine(coroutine);
        }
    }
}