using UnityEngine;

namespace HWRWeaponSystem
{
    public class WeaponBase : MonoBehaviour
    {
        // [HideInInspector]
        public GameObject Owner;
        [HideInInspector]
        public GameObject Target;
        [HideInInspector]
        public ObjectPool objectPool;
        [Header("Properties")]
        public string[] TargetTag = new string[1] { "Enemy" };
        public string[] IgnoreTag;
        public bool RigidbodyProjectile;
        public Vector3 TorqueSpeedAxis;
        public GameObject TorqueObject;
    }
}