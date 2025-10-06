using UnityEngine;

namespace HWRWeaponSystem
{
    public class AILook : MonoBehaviour
    {
        public string[] TargetTag = new string[1] { "Enemy" };
        public float Distance = 1000;
        private int indexWeapon;
        private LauncherController launcher;
        private GameObject target;
        private float timeAIattack;

        private void Start()
        {
            launcher = GetComponent<LauncherController>();
        }

        private void Update()
        {
            if (target)
            {
                var targetlook = Quaternion.LookRotation(target.transform.position - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetlook, Time.deltaTime * 3);

                var dir = (target.transform.position - transform.position).normalized;
                var direction = Vector3.Dot(dir, transform.forward);

                // if (direction > 0.9f)
                //     if (launcher)
                //         launcher.LaunchWeapon(indexWeapon);

                // AI attack the target for a while (3 sec)
                if (Time.time > timeAIattack + 3) target = null;
                // AI forget this target and try to looking new target
            }
            else
            {
                for (var t = 0; t < TargetTag.Length; t++)
                {
                    // AI find target only in TargetTag list
                    var collector = WeaponSystem.Finder.FindTargetTag(TargetTag[t]);
                    if (collector != null)
                    {
                        var objs = collector.Targets;
                        var distance = Distance;
                        for (var i = 0; i < objs.Length; i++)
                            if (objs[i] != null)
                            {
                                var dis = Vector3.Distance(objs[i].transform.position, transform.position);

                                if (distance > dis)
                                {
                                    // Select closer target
                                    distance = dis;
                                    target = objs[i];
                                    // if (launcher) indexWeapon = Random.Range(0, launcher.WeaponLists.Length);
                                    timeAIattack = Time.time;
                                }
                            }
                    }
                }
            }
        }
    }
}