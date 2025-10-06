namespace HWRWeaponSystem
{
    // public class MoverMissile : WeaponBase
    // {
    //     public float Damping = 3;
    //     public float Speed = 80;
    //     public float SpeedMax = 80;
    //     public float SpeedMult = 1;
    //     public Vector3 Noise = new(20, 20, 20);
    //     public float TargetLockDirection = 0.5f;
    //     public int DistanceLock = 70;
    //     public int DurationLock = 40;
    //     public bool Seeker;
    //     public float LifeTime = 5.0f;
    //     private bool locked;
    //     private Rigidbody rigidBody;
    //     private float speedTemp;
    //     private float timeCount;
    //     private int timetorock;
    //
    //     private void Awake()
    //     {
    //         speedTemp = Speed;
    //         objectPool = GetComponent<ObjectPool>();
    //         rigidBody = GetComponent<Rigidbody>();
    //         if (objectPool && WeaponSystem.Pool != null)
    //             objectPool.LifeTime = LifeTime;
    //     }
    //
    //     private void Start()
    //     {
    //         if (objectPool && WeaponSystem.Pool != null)
    //         {
    //             objectPool.LifeTime = LifeTime;
    //             objectPool.SetDestroy(LifeTime);
    //         }
    //         else
    //         {
    //             Destroy(gameObject, LifeTime);
    //         }
    //
    //         timeCount = Time.time;
    //     }
    //
    //     private void Update()
    //     {
    //         if (WeaponSystem.Pool != null && objectPool != null && !objectPool.Active)
    //             return;
    //
    //         if (Time.time >= timeCount + LifeTime - 0.5f)
    //             if (GetComponent<Bullet>())
    //             {
    //                 GetComponent<Bullet>().Active(transform.position);
    //                 timeCount = Time.time;
    //             }
    //
    //         if (Target)
    //         {
    //             var rotation = Quaternion.LookRotation(Target.transform.position - transform.transform.position);
    //             transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * Damping);
    //             var dir = (Target.transform.position - transform.position).normalized;
    //             var direction = Vector3.Dot(dir, transform.forward);
    //             if (direction < TargetLockDirection) Target = null;
    //         }
    //
    //         if (Seeker)
    //         {
    //             if (timetorock > DurationLock)
    //             {
    //                 if (!locked && !Target)
    //                 {
    //                     float distance = int.MaxValue;
    //                     for (var t = 0; t < TargetTag.Length; t++)
    //                     {
    //                         var collector = WeaponSystem.Finder.FindTargetTag(TargetTag[t]);
    //                         if (collector != null)
    //                         {
    //                             var objs = collector.Targets;
    //
    //                             for (var i = 0; i < objs.Length; i++)
    //                                 if (objs[i])
    //                                 {
    //                                     var dir = (objs[i].transform.position - transform.position).normalized;
    //                                     var direction = Vector3.Dot(dir, transform.forward);
    //                                     var dis = Vector3.Distance(objs[i].transform.position, transform.position);
    //                                     if (direction >= TargetLockDirection)
    //                                         if (DistanceLock > dis)
    //                                         {
    //                                             if (distance > dis)
    //                                             {
    //                                                 distance = dis;
    //                                                 Target = objs[i];
    //                                             }
    //
    //                                             locked = true;
    //                                         }
    //                                 }
    //                         }
    //                     }
    //                 }
    //
    //                 timetorock = 0;
    //             }
    //             else
    //             {
    //                 timetorock += 1;
    //             }
    //
    //             if (Target)
    //             {
    //             }
    //             else
    //             {
    //                 locked = false;
    //             }
    //         }
    //     }
    //
    //     private void FixedUpdate()
    //     {
    //         rigidBody.linearVelocity = new Vector3(transform.forward.x * Speed * Time.fixedDeltaTime, transform.forward.y * Speed * Time.fixedDeltaTime, transform.forward.z * Speed * Time.fixedDeltaTime);
    //         rigidBody.linearVelocity += new Vector3(Random.Range(-Noise.x, Noise.x), Random.Range(-Noise.y, Noise.y), Random.Range(-Noise.z, Noise.z));
    //
    //         if (Speed < SpeedMax) Speed += SpeedMult * Time.fixedDeltaTime;
    //     }
    //
    //     public void OnEnable()
    //     {
    //         Speed = speedTemp;
    //         Target = null;
    //         timeCount = Time.time;
    //         if (objectPool)
    //         {
    //             objectPool.LifeTime = LifeTime;
    //             objectPool.SetDestroy(LifeTime);
    //         }
    //     }
    // }
}