using Opsive.UltimateCharacterController.Traits.Damage;
using UnityEngine;

namespace Zocat
{
    [CreateAssetMenu(fileName = "MultiplierDamageProcessor", menuName = "Zocat/Multiplier Damage Processor")]
    public class DamageMultiplierProcessor : DamageProcessor
    {
        [SerializeField] protected float m_Multiplier;

        /// <summary>
        /// Processes the DamageData on the DamageTarget.
        /// </summary>
        /// <param name="target">The object receiving the damage.</param>
        /// <param name="damageData">The damage data to be applied to the target.</param>
        public override void Process(IDamageTarget target, DamageData damageData)
        {
            // IsoHelper.Log(damageData.);
            var item = HeroWeaponManager.Instance.CurrentItemType;
            IsoHelper.Log(ItemCalculator.Instance.GetVector2Value(item, AttributeType.DamageMin).x);
            damageData.Amount *= m_Multiplier;
            target.Damage(damageData);
        }
    }
}