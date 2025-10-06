using Opsive.Shared.Utility;
using Opsive.UltimateCharacterController.Traits;
// using Opsive.UltimateCharacterController.Traits;
// using Opsive.UltimateCharacterController.Traits.Damage;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Zocat
{
    public class HealthTest : InstanceBehaviour
    {
        public Health Health;
        public TextMeshProUGUI Tmp;

        private void Awake()
        {
            Health.OnDamageEvent.AddListener(OnDamage);
        }

        private void OnDamage(float fl, Vector3 al, Vector3 vel, GameObject att)
        {
            Tmp.text = Health.Value.ToString();
        }


        // public GameObject Enemy;
        // public Collider head;
        //
        // [Button(ButtonSizes.Medium)]
        // public void DamageHead()
        // {
        //     var pooledDamageData = GenericObjectPool.Get<DamageData>();
        //     pooledDamageData.SetDamage(20, Vector3.down, Vector3.down, 1, 1, 1, gameObject, null, head);
        //     Enemy.GetComponent<Health>().Damage(pooledDamageData);
        // }
        //
        // [Button(ButtonSizes.Medium)]
        // public void DamageBody()
        // {
        //     Enemy.GetComponent<Health>().Damage(10);
        // }
        //
        // public void ShowDamage(float damage, Vector3 ali, Vector3 veli, GameObject source)
        // {
        //     IsoHelper.Log(damage, source);
        // }
        //
        // public void OnDeath()
        // {
        //     IsoHelper.Log("öldü");
        // }
    }
}