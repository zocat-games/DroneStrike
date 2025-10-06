using System;
using System.Collections;
using UnityEngine;

namespace Zocat
{
    public class RayTriggerBase : InstanceBehaviour
    {
        private Action _triggerAction;

        public void Initialize(Action triggerAction)
        {
            _triggerAction = triggerAction;
        }

        public virtual void OnTriggered()
        {
            _triggerAction?.Invoke();
        }
    }
}