using Opsive.Shared.Game;
using UnityEngine;

namespace HWRWeaponSystem
{
    public class Bullet : BulletBase
    {
        public bool Explosive;
        public float DamageRadius = 20;
        public bool RayChecker;
        public float ExplosionRadius = 20;
        public float ExplosionForce = 1000;
        public bool HitedActive = true;
        public float TimeActive;
        private ObjectPool objPool;
        private Vector3 prevpos;
        private float timetemp;

        private void Awake()
        {
            objPool = gameObject.GetCachedComponent<ObjectPool>();
        }

        private void Start()
        {
            if (!Owner || !Owner.GetCachedComponent<Collider>())
                return;

            timetemp = Time.time;
        }

        private void Update()
        {
            if (objPool && !objPool.Active) return;

            var mag = Vector3.Distance(transform.position, prevpos);
            if (RayChecker)
            {
                var hits = Physics.RaycastAll(transform.position + -transform.forward * mag, transform.forward, mag);
                foreach (var hit in hits)
                    if (hit.collider.transform.root != transform.root && (Owner == null || hit.collider.transform.root != Owner.transform.root))
                    {
                        Active(hit.point);
                        break;
                    }
            }

            prevpos = transform.position;

            if (!HitedActive || TimeActive > 0)
                if (Time.time >= timetemp + TimeActive)
                    Active(transform.position);
        }


        private void OnEnable()
        {
            prevpos = transform.position - transform.forward;
            timetemp = Time.time;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (objPool && !objPool.Active && WeaponSystem.Pool != null) return;

            if (HitedActive)
                if (IsThisDamageable(collision.gameObject) && collision.gameObject.tag != gameObject.tag)
                {
                    if (!Explosive)
                        NormalDamage(collision);
                    Active(transform.position);
                }
        }


        public void Active(Vector3 position)
        {
            if (Effect)
            {
                if (WeaponSystem.Pool != null)
                {
                    WeaponSystem.Pool.Instantiate(Effect, transform.position, transform.rotation, 3);
                }
                else
                {
                    var obj = Instantiate(Effect, transform.position, transform.rotation);
                    Destroy(obj, 3);
                }
            }

            if (Explosive)
                ExplosionDamage();

            if (objPool)
                objPool.OnDestroyed();
            else
                Destroy(gameObject);
        }

        private void ExplosionDamage()
        {
            var hitColliders = Physics.OverlapSphere(transform.position, ExplosionRadius);
            for (var i = 0; i < hitColliders.Length; i++)
            {
                var hit = hitColliders[i];
                if (!hit)
                    continue;

                if (hit.GetComponent<Rigidbody>())
                    hit.GetComponent<Rigidbody>().AddExplosionForce(ExplosionForce, transform.position, ExplosionRadius, 3.0f);
            }

            var dmhitColliders = Physics.OverlapSphere(transform.position, DamageRadius);

            for (var i = 0; i < dmhitColliders.Length; i++)
            {
                var hit = dmhitColliders[i];

                if (!hit)
                    continue;

                if (IsThisDamageable(hit.gameObject) && (Owner == null || (Owner != null && hit.gameObject != Owner.gameObject)))
                {
                    var damagePack = new DamagePack();
                    damagePack.Damage = Damage;
                    damagePack.Owner = Owner;
                    hit.gameObject.SendMessage("ApplyDamage", damagePack, SendMessageOptions.DontRequireReceiver);
                }
            }
        }

        private void NormalDamage(Collision collision)
        {
            var damagePack = new DamagePack();
            damagePack.Damage = Damage;
            damagePack.Owner = Owner;
            collision.gameObject.SendMessage("ApplyDamage", damagePack, SendMessageOptions.DontRequireReceiver);
        }
    }
}