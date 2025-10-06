using Sirenix.OdinInspector;
using UnityEngine;

namespace Zocat
{
    [CreateAssetMenu(fileName = "CustomScriptable", menuName = "Zocat/CustomScriptable", order = 1)]
    public class CustomScriptable : SerializedScriptableObject
    {
        public GameManager GameManager => GameManager.Instance;
        public string InstanceID => GetInstanceID().ToString();
        public string SaveId(string plus) => InstanceID + plus;
        protected float _Timer;
        protected byte _Counter;
    }
}