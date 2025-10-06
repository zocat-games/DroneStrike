using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Zocat
{
    public class IdBase : InstanceBehaviour
    {
        [ReadOnly]
        public string Id;

        private void Reset()
        {
            Id = Guid.NewGuid().ToString();
        }

        public void SetId()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Button(ButtonSizes.Medium)]
        public void GenerateId()
        {
            SetId();
        }
    }
}