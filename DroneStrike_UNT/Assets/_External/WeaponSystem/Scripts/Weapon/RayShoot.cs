using UnityEngine;

namespace HWRWeaponSystem
{
    public class RayShoot : BulletBase
    {
        public int Range = 10000;
        public Vector3 AimPoint;
        public GameObject Explosion;
        public float LifeTime = 1;
        public LineRenderer Trail;
        private bool actived;
        private ObjectPool objPool;

        private void Awake()
        {
            objPool = GetComponent<ObjectPool>();
        }

        private void Start()
        {
            ShootRay();
            actived = true;
        }

        private void Update()
        {
        }

        private void OnEnable()
        {
            if (objPool)
                objPool.SetDestroy(LifeTime);

            if (actived)
                ShootRay();
        }

        private void ShootRay()
        {
            if (GetComponent<Collider>())
            {
                Physics.IgnoreCollision(GetComponent<Collider>(), Owner.GetComponent<Collider>());
                if (Owner.transform.root)
                    foreach (var col in Owner.transform.root.GetComponentsInChildren<Collider>())
                        Physics.IgnoreCollision(GetComponent<Collider>(), col);
            }

            RaycastHit hit;
            GameObject explosion = null;
            if (Physics.Raycast(transform.position, transform.forward, out hit, Range))
            {
                AimPoint = hit.point;
                if (Explosion != null)
                {
                    if (WeaponSystem.Pool != null)
                        explosion = WeaponSystem.Pool.Instantiate(Explosion, AimPoint, transform.rotation);
                    else
                        explosion = Instantiate(Explosion, AimPoint, transform.rotation);
                }

                var damagePack = new DamagePack();
                damagePack.Damage = Damage;
                damagePack.Owner = Owner;
                hit.collider.gameObject.SendMessage("ApplyDamage", damagePack, SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                AimPoint = transform.position + transform.forward * Range;
                if (Explosion != null)
                {
                    if (WeaponSystem.Pool != null)
                        explosion = WeaponSystem.Pool.Instantiate(Explosion, AimPoint, transform.rotation);
                    else
                        explosion = Instantiate(Explosion, AimPoint, transform.rotation);
                }
            }

            if (explosion)
            {
                explosion.transform.forward = transform.forward;
                var dmg = explosion.GetComponent<BulletBase>();
                if (dmg) dmg.TargetTag = TargetTag;
            }

            if (Trail)
            {
                Trail.SetPosition(0, transform.position);
                Trail.SetPosition(1, AimPoint);
            }

            if (WeaponSystem.Pool == null) Destroy(gameObject, LifeTime);
        }
    }
}