using System;
using UnityEngine;

namespace Zocat
{
    public class CustomObject : MonoBehaviour
    {
        public GameManager GameManager => GameManager.Instance;
        public string InstanceID => GetInstanceID().ToString();

        protected float _Timer;
        protected byte _Counter;
    }
}