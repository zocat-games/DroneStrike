using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Zocat
{
    public class LookAtCamera : InstanceBehaviour
    {
        public Transform target;

        [Button(ButtonSizes.Medium)]
        public void Test()
        {
            var dif = target.position - transform.position;
            transform.rotation = Quaternion.LookRotation(dif);
        }
    }
}