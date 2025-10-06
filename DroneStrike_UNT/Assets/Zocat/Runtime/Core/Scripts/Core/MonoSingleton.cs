using UnityEngine;
using Zocat;

public abstract class MonoSingleton<T> : SerializedInstance where T : MonoSingleton<T>
{
    private static T m_Instance;

    public static T Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindFirstObjectByType(typeof(T)) as T;
                if (m_Instance == null)
                {
                    Debug.LogWarning("No instance of " + typeof(T) + ", a temporary one is created.");

                    m_Instance = new GameObject("Temp Instance of " + typeof(T), typeof(T)).GetComponent<T>();

                    if (m_Instance == null) Debug.LogError("Problem during the creation of " + typeof(T));
                }
            }

            return m_Instance;
        }
    }

    protected virtual void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this as T;
        }
        else if (m_Instance != this)
        {
            Debug.LogError("Another instance of " + GetType() + " is already exist! Destroying self...");
            DestroyImmediate(this);
        }
    }


    /// Make sure the instance isn't referenced anymore when the user quit, just in case.
    private void OnApplicationQuit()
    {
        m_Instance = null;
    }
}