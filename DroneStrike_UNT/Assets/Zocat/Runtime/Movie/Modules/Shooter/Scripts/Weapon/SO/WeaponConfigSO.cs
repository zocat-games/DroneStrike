using Opsive.UltimateCharacterController.Objects;
using UnityEngine;

namespace Zocat
{
    [CreateAssetMenu(fileName = "WeaponConfigSO", menuName = "Zocat/WeaponConfigSO", order = 1)]
    public class WeaponConfigSO : CustomScriptable
    {
        public CategoryType Category;
        public ItemType ItemType;
        public float FireRate;
        public float ReloadDuration;
        public int ClipCurrent;
        public int ClipMax;
        public int StockCurrent;
        public bool InfiniteAmmo;
        public float DamageAmount;
        public AudioClip AudioClip;
        public int ZoomAmount;
        public bool RapidFire;
        public Projectile Projectile;
    }
}