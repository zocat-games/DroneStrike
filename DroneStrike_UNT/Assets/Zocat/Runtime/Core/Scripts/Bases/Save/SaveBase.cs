using System;
using System.Collections;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Zocat
{
    public class SaveBase<T> : IdBase
    {
        protected virtual void OnEnable()
        {
            Value = ES3.Load(Id, default(T));
        }

        protected virtual void OnApplicationPause(bool pauseStatus)
        {
            ES3.Save(Id, Value);
        }


        public T Value { get; set; }
    }
}