using System.Collections;
using UnityEngine;

namespace Zocat
{
    public interface ISave<T>
    {
        protected void OnEnable();
        protected void OnApplicationPause(bool pause);
        public T Value { get; set; }
    }
}