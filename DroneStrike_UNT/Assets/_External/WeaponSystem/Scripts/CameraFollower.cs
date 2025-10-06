using UnityEngine;

namespace HWRWeaponSystem
{
    public class CameraFollower : MonoBehaviour
    {
        public GameObject Target;
        public Vector3 Offset;


        private void Update()
        {
            if (Target)
            {
                transform.position = Vector3.Lerp(transform.position, Target.transform.position + Offset, Time.deltaTime * 10);
                transform.position += CameraEffects.Shaker.ShakeMagnitude * 0.2f;
            }
        }
    }
}