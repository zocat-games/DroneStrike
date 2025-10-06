using UnityEngine;

namespace Zocat
{
    public class LookYTest : InstanceBehaviour
    {
        public Transform target;

        private void Update()
        {
            // if (target == null) return;
            //
            // // Hedef yönü al, Y bileşenini sıfırla
            // var dir = target.position - transform.position;
            // dir.y = 0f;
            //
            // if (dir.sqrMagnitude > 0.0001f) // aynı noktadaysa bakmaya çalışma
            //     transform.eulerAngles = Quaternion.LookRotation(dir).eulerAngles;
            transform.LookY(target);
        }
    }
}