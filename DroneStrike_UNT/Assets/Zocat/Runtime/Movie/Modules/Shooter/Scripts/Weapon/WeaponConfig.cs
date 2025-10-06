using UnityEngine;

namespace Zocat
{
    public class WeaponConfig : InstanceBehaviour
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
        public GameObject Owner;

        // private void Awake()
        // {
        //     EventHandler.RegisterEvent(EventManager.AfterCreateLevel, AfterCreateLevel);
        // }

        // private void AfterCreateLevel()
        // {
        // ItemType = Category.EquippedItemType();
        // DamageAmount = ItemCalculator.GetVector2Value(ItemType, AttributeType.DamageMin).x;
        // }
    }
}