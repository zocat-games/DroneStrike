using UnityEngine;

namespace HWRWeaponSystem
{
    public class ChainBullet : BulletBase
    {
        public GameObject ChainObject;
        public int NumberChain = 5;
        public int Distance = 100;
        public float Direction = 0.5f;
        private bool actived;

        private void Awake()
        {
        }

        private void Start()
        {
            chainDamage();
            actived = true;
        }

        private void OnEnable()
        {
            if (actived)
                chainDamage();
        }

        private void chainDamage()
        {
            var count = 0;
            for (var t = 0; t < TargetTag.Length; t++)
            {
                var collector = WeaponSystem.Finder.FindTargetTag(TargetTag[t]);
                if (collector != null)
                {
                    var objs = collector.Targets;
                    float distance = Distance;


                    for (var i = 0; i < objs.Length; i++)
                        if (objs[i] != null)
                        {
                            var dir = (objs[i].transform.position - transform.position).normalized;
                            var direction = Vector3.Dot(dir, transform.forward);
                            var dis = Vector3.Distance(objs[i].transform.position, transform.position);
                            if (dis < distance)
                            {
                                if (direction >= Direction)
                                    if (ChainObject)
                                        if (count <= NumberChain)
                                        {
                                            GameObject chain;
                                            var targetlook = Quaternion.LookRotation(objs[i].transform.position - transform.position);
                                            if (WeaponSystem.Pool != null)
                                                chain = WeaponSystem.Pool.Instantiate(ChainObject, transform.position, targetlook);
                                            else
                                                chain = Instantiate(ChainObject, transform.position, targetlook);

                                            var dmg = chain.GetComponent<BulletBase>();
                                            if (dmg) dmg.TargetTag = TargetTag;
                                            count += 1;
                                        }

                                distance = dis;
                            }
                        }
                }
            }
        }
    }
}