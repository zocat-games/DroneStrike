using System;
using System.Collections.Generic;
using System.Linq;
using Opsive.UltimateInventorySystem.Core;
using UnityEngine;

namespace Zocat
{
    public static class ItemExtensions
    {
        public static void SetValue<T>(this ItemType itemType, AttributeType attributeType, T value)
        {
            AttributeServer.Instance.SetItemValue(itemType, attributeType, value);
        }

        public static void SetValue<T>(this CategoryType categoryType, AttributeType attributeType, T value)
        {
            var list = categoryType.ItemTypeList();
            foreach (var item in list) item.SetValue(attributeType, value);
        }

        /*--------------------------UNIT------------------------------------------------------------*/
        public static string Name(this ItemType itemType)
        {
            return AttributeServer.Instance.GetItemValue<string>(itemType, AttributeType.Name);
        }

        public static Sprite Icon(this ItemType itemType)
        {
            return AttributeServer.Instance.GetItemValue<Sprite>(itemType, AttributeType.Icon);
        }

        public static int Index(this ItemType itemType)
        {
            return AttributeServer.Instance.GetItemValue<int>(itemType, AttributeType.Index);
        }

        public static int CategoryIndex(this ItemType itemType)
        {
            return AttributeServer.Instance.GetItemValue<int>(itemType, AttributeType.CategoryIndex);
        }

        public static int Level(this ItemType itemType)
        {
            return AttributeServer.Instance.GetItemValue<int>(itemType, AttributeType.Level);
        }


        public static int Amount(this ItemType itemType)
        {
            return AttributeServer.Instance.GetItemValue<int>(itemType, AttributeType.Amount);
        }

        public static bool Unlocked(this ItemType itemType)
        {
            return AttributeServer.Instance.GetItemValue<bool>(itemType, AttributeType.Unlocked);
        }


        public static bool Purchased(this ItemType itemType)
        {
            return AttributeServer.Instance.GetItemValue<bool>(itemType, AttributeType.Purchased);
        }

        public static bool Equipped(this ItemType itemType)
        {
            return AttributeServer.Instance.GetItemValue<bool>(itemType, AttributeType.Equipped);
        }

        public static int Durability(this ItemType itemType)
        {
            return AttributeServer.Instance.GetItemValue<int>(itemType, AttributeType.Durability);
        }

        public static string Description(this ItemType itemType)
        {
            return AttributeServer.Instance.GetItemValue<string>(itemType, AttributeType.Description);
        }

        public static int Price(this ItemType itemType)
        {
            return AttributeServer.Instance.GetItemValue<int>(itemType, AttributeType.Price);
        }

        public static int UpgradeFee(this ItemType itemType)
        {
            var value = itemType.Price() * ConfigManager.UpgradeMul;
            return value.ToInt();
        }

        public static int RepairFee(this ItemType itemType)
        {
            var value = itemType.Price() * ConfigManager.RepairMul;
            return value.ToInt();
        }

        public static bool Upgradable(this ItemType itemType)
        {
            return itemType.FromThisCategory(CategoryType.__Upgradable);
        }

        public static bool RapidFire(this ItemType itemType)
        {
            return AttributeServer.Instance.GetItemValue<bool>(itemType, AttributeType.RapidFire);
        }

        public static void SetCategoryEquipping(this ItemType itemType)
        {
            itemType.Category().SetValue(AttributeType.Equipped, false);
            itemType.SetValue(AttributeType.Equipped, true);
        }

        public static AudioClip AudioClip(this ItemType itemType)
        {
            return AttributeServer.Instance.GetItemValue<AudioClip>(itemType, AttributeType.AudioClip);
        }


        /*--------------------------------UPG------------------------------------------------------*/
        public static Vector2 DamageMin(this ItemType itemType)
        {
            return AttributeServer.Instance.GetItemValue<Vector2>(itemType, AttributeType.DamageMin);
        }

        public static Vector2 EffectiveRangeMin(this ItemType itemType)
        {
            return AttributeServer.Instance.GetItemValue<Vector2>(itemType, AttributeType.EffectiveRangeMin);
        }

        public static Vector2 MagazineSizeMin(this ItemType itemType)
        {
            return AttributeServer.Instance.GetItemValue<Vector2>(itemType, AttributeType.MagazineSizeMin);
        }

        public static Vector2 ReloadSpeedMin(this ItemType itemType)
        {
            return AttributeServer.Instance.GetItemValue<Vector2>(itemType, AttributeType.ReloadSpeedMin);
        }

        public static float ReloadDuration(this ItemType itemType)
        {
            var speed = itemType.ReloadSpeedMin();
            return speed.y - speed.x;
        }

        /*--------------------------------------------------------------------------------------*/
        /*--------------------------------------------------------------------------------------*/
        public static Vector2 BonusHealthMin(this ItemType itemType)
        {
            return AttributeServer.Instance.GetItemValue<Vector2>(itemType, AttributeType.BonusHealthMin);
        }

        public static Vector2 DefenseMin(this ItemType itemType)
        {
            return AttributeServer.Instance.GetItemValue<Vector2>(itemType, AttributeType.DefenseMin);
        }

        public static Vector2 ExplosiveDefenseMin(this ItemType itemType)
        {
            return AttributeServer.Instance.GetItemValue<Vector2>(itemType, AttributeType.ExplosiveDefenseMin);
        }

        public static Vector2 HeadShotProtectionMin(this ItemType itemType)
        {
            return AttributeServer.Instance.GetItemValue<Vector2>(itemType, AttributeType.HeadShotProtectionMin);
        }

        /*--------------------------------------------------------------------------------------*/
        public static Vector2 Radius(this ItemType itemType)
        {
            return AttributeServer.Instance.GetItemValue<Vector2>(itemType, AttributeType.Radius);
        }

        public static Vector2 HealingAmount(this ItemType itemType)
        {
            return AttributeServer.Instance.GetItemValue<Vector2>(itemType, AttributeType.HealingAmount);
        }

        public static Vector2 ApplicationDuration(this ItemType itemType)
        {
            return AttributeServer.Instance.GetItemValue<Vector2>(itemType, AttributeType.ApplicationDuration);
        }

        public static Vector2 DamageBoost(this ItemType itemType)
        {
            return AttributeServer.Instance.GetItemValue<Vector2>(itemType, AttributeType.DamageBoost);
        }

        public static CategoryType Category(this ItemType itemType)
        {
            var itemDef = AttributeServer.Instance.Items[itemType];
            return Enum.GetValues(typeof(CategoryType)).Cast<CategoryType>().FirstOrDefault(item => itemDef.Category.ToString() == item.ToString());
        }

        public static T GetAttributeValue<T>(this ItemType itemType, AttributeType attributeType)
        {
            return AttributeServer.Instance.GetItemValue<T>(itemType, attributeType);
        }

        /*------------------------------------CATEGORY----------------------------------------------*/
        public static bool FromThisCategory(this ItemType itemType, CategoryType categoryType)
        {
            var category = InventorySystemManager.GetItemCategory(categoryType.ToString());
            var itemDef = AttributeServer.Instance.GetItemDefinition(itemType.ToString());
            return category.InherentlyContains(itemDef);
        }

        public static List<ItemType> ItemTypeList(this CategoryType categoryType)
        {
            var category = InventorySystemManager.GetItemCategory(categoryType.ToString());
            var allMyCategoryItemDefinitions = new ItemDefinition[0];
            category.GetAllChildrenElements(ref allMyCategoryItemDefinitions);
            return (from item in allMyCategoryItemDefinitions where item != null select (ItemType)Enum.Parse(typeof(ItemType), item.name)).ToList();
        }

        public static int Count(this CategoryType categoryType)
        {
            return ItemServer.Instance.CategoryItemLists[categoryType].Count;
        }

        public static int MaxValue(this CategoryType categoryType, AttributeType attributeType)
        {
            var itemTypeList = ItemTypeList(categoryType);
            var values = itemTypeList.Select(item => item.GetAttributeValue<int>(attributeType)).ToList();
            return values.Max();
        }

        public static bool IsParent(this CategoryType categoryType, CategoryType parentCategoryType)
        {
            var category = InventorySystemManager.GetItemCategory(categoryType.ToString());
            var parentCategory = InventorySystemManager.GetItemCategory(parentCategoryType.ToString());
            var itemCategories = new ItemCategory[0];
            category.GetAllParents(ref itemCategories, true);

            return itemCategories.Any(item => item == parentCategory);
        }

        public static bool Upgradable(this CategoryType categoryType)
        {
            return categoryType.IsParent(CategoryType.__Upgradable);
        }

        public static ItemType EquippedItemType(this CategoryType categoryType)
        {
            return categoryType.ItemTypeList().FirstOrDefault(_ => _.Equipped());
        }


        public static ItemType HeighestIndexPurchased(this CategoryType categoryType)
        {
            return ItemTypeList(categoryType)
                .Where(item => item.Purchased())
                .OrderByDescending(item => item.Index())
                .FirstOrDefault();
        }

        public static int CategoryIndex(this CategoryType categoryType)
        {
            switch (categoryType)
            {
                case CategoryType.MachineGun: return 0;
                case CategoryType.Unguided: return 1;
                case CategoryType.Guided: return 2;
            }

            return -1;
        }

        // public static ItemType HeighestIndexItem(this CategoryType categoryType)
        // {
        //     var list = ItemTypeList(categoryType);
        //     var itemType = default(ItemType);
        //     var index = 0;
        //
        //     foreach (var item in list)
        //     {
        //         if (item.Equipped() && item.Index() >= index)
        //             index = item.Index();
        //         itemType = item;
        //     }
        //
        //     return itemType;
        // }

        // public static List<ItemType> ItemTypeListByLock(this CategoryType categoryType, bool unlocked)
        // {
        //     Func<ItemType, bool> func = unlocked ? item => item.Unlocked() : item => !item.Unlocked();
        //     return categoryType.ItemTypeList()
        //         .Where(func)
        //         .ToList();
        // }

        // Name = 0,
        // Icon = 1,
        // Description = 2,
        // Equipped = 3,
        // Index = 4,
        // EffectiveRange = 5,
        // Level = 6,
        // ReloadTimeRange = 7,
        // DamageRange = 8,
        // Activated = 9,
    }
}