using System.Collections.Generic;
using System.Linq;
using Opsive.UltimateInventorySystem.Core;
using Opsive.UltimateInventorySystem.Core.AttributeSystem;
using Opsive.UltimateInventorySystem.Core.InventoryCollections;

namespace Zocat
{
    public class AttributeServer : MonoSingleton<AttributeServer>
    {
        public Inventory Inventory;
        public Dictionary<ItemType, ItemDefinition> Items;

        /*---------------------------------UNITTYPE-----------------------------------------------------*/
        public void SetItemValue<T>(ItemType ItemType, AttributeType attributeType, T value)
        {
            var itemDef = GetItemDefinition(ItemType.ToString());
            var itemInfoInv = Inventory.GetItemInfo(itemDef);
            itemInfoInv.Value.Item.GetAttribute<Attribute<T>>(attributeType.ToString()).SetOverrideValue(value);
        }

        public T GetItemValue<T>(ItemType ItemType, AttributeType attributeType)
        {
            var itemDef = GetItemDefinition(ItemType.ToString());
            var itemInfoInv = Inventory.GetItemInfo(itemDef);
            return itemInfoInv.Value.Item.GetAttribute<Attribute<T>>(attributeType.ToString()).GetValue();
        }

        /*--------------------------------------------------------------------------------------*/

        #region Other

        /*--------------------------------------------------------------------------------------*/
        public ItemDefinition GetItemDefinition(string itemName)
        {
            return (from item in Items where item.Key.ToString() == itemName select item.Value).FirstOrDefault();
        }


        /*--------------------------------------------------------------------------------------*/

        #endregion
    }
}