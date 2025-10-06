using UnityEngine;

namespace HWRWeaponSystem
{
    public class Damageable : MonoBehaviour
    {
        public GameObject Effect;
        public int HP = 100;
        [HideInInspector]
        public GameObject LatestHit;
        private ObjectPool objPool;

        private void Awake()
        {
            objPool = GetComponent<ObjectPool>();
        }

        public virtual void ApplyDamage(DamagePack damage)
        {
            if (HP < 0)
                return;

            LatestHit = damage.Owner;
            HP -= damage.Damage;
            if (HP <= 0) Dead();
        }

        public virtual void ApplyDamage(int damage)
        {
            if (HP < 0)
                return;

            HP -= damage;
            if (HP <= 0) Dead();
        }

        public void Dead()
        {
            if (Effect)
            {
                if (WeaponSystem.Pool != null && Effect.GetComponent<ObjectPool>())
                    WeaponSystem.Pool.Instantiate(Effect, transform.position, transform.rotation);
                else
                    Instantiate(Effect, transform.position, transform.rotation);
            }

            if (objPool != null)
                objPool.Destroying();
            else
                Destroy(gameObject);

            gameObject.SendMessage("OnDead", SendMessageOptions.DontRequireReceiver);
        }
    }
}