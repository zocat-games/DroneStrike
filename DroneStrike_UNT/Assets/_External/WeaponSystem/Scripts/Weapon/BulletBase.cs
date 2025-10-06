using Opsive.Shared.Game;
using UnityEngine;

namespace HWRWeaponSystem
{
    public class BulletBase : MonoBehaviour
    {
        public GameObject Effect;
        [HideInInspector]
        public GameObject Owner;
        public int Damage = 20;
        [HideInInspector]
        public ObjectPool objectPool;
        public string[] TargetTag = new string[1] { "Enemy" };
        public string[] IgnoreTag;

        public bool IsThisDamageable(GameObject gob)
        {
            for (var i = 0; i < TargetTag.Length; i++)
                if (TargetTag[i] == gob.tag)
                    return true;

            return false;
        }


        public void IgnoreSelf(GameObject owner)
        {
            if (gameObject.GetCachedComponent<Collider>() && owner)
            {
                if (owner.GetCachedComponent<Collider>())
                    Physics.IgnoreCollision(gameObject.GetCachedComponent<Collider>(), owner.GetCachedComponent<Collider>());

                if (Owner.transform.root)
                    foreach (var col in Owner.transform.root.GetComponentsInChildren<Collider>())
                        Physics.IgnoreCollision(gameObject.GetCachedComponent<Collider>(), col);
            }
        }
    }

    public struct DamagePack
    {
        public int Damage;
        public GameObject Owner;
    }
}