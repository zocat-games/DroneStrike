using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Zocat
{
    public class ItemServer : MonoSingleton<ItemServer>
    {
        public bool SetListAtStart;
        public Dictionary<CategoryType, List<ItemType>> CategoryItemLists;

        protected override void Awake()
        {
            base.Awake();
            if (SetListAtStart)
            {
                CategoryItemLists.Clear();
                SetList();
            }
        }

        private void SetList()
        {
            foreach (CategoryType item in Enum.GetValues(typeof(CategoryType)))
            {
                var items = item.ItemTypeList();
                CategoryItemLists.Add(item, items);
            }
        }


        [Button(ButtonSizes.Medium)]
        public void Test()
        {
            // foreach (CategoryType item in Enum.GetValues(typeof(ItemType)))
            // {
            //     CategoryItemLists.Add(item, new List<ItemType>());
            // }

            IsoHelper.Log(ItemType.ps_0_SmithWesson.Durability());
        }
    }
}