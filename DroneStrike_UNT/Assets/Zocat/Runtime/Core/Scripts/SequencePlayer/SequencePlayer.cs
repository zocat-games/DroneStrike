using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Zocat
{
    public class SequencePlayer : InstanceBehaviour
    {
        public Step[] Steps;
        public bool Loop;
        private int _stepCounter;
        private int _methodCounter;

        private void FixedUpdate()
        {
        }

        private int StepMode
        {
            get => _stepCounter;
            set => _stepCounter = value >= Steps.Length ? 0 : value;
        }
    }
}


// public enum TaskStatus
// {
//     None = 0,
//     Running = 1,
//     Success = 2,
//     Failure = 3,
// }


// [Button(ButtonSizes.Medium)]
// public void Do()
// {
//     Mode = 0;
//     for (var i = 0; i < 3; i++)
//     {
//         Mode++;
//     }
// }