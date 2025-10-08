using System.Collections.Generic;
using System.Linq;

namespace Zocat
{
    public class EquipmentPanel : SerializedUIPanel
    {
        public SlotGroup SlotGroup;
        public CategoryGroup CategoryGroup;
        public Dictionary<CategoryType, string> SlotNames;


        public override void Initialize()
        {
            base.Initialize();
            SlotGroup.Initialize();
            CategoryGroup.Initialize();
        }

        public override void Show()
        {
            base.Show();
            SlotGroup.Show();
            // CategorySection.Show();
        }


        public override void Hide()
        {
            base.Hide();
            if (!CategoryType.MachineGun.ItemTypeList().Any(item => item.Equipped()))
            {
                var heighest = CategoryType.MachineGun.HeighestIndexPurchased();
                heighest.SetCategoryEquipping();
            }

            if (!CategoryType.Unguided.ItemTypeList().Any(item => item.Equipped()))
            {
                var heighest = CategoryType.Unguided.HeighestIndexPurchased();
                heighest.SetCategoryEquipping();
            }

            if (!CategoryType.Guided.ItemTypeList().Any(item => item.Equipped()))
            {
                var heighest = CategoryType.Guided.HeighestIndexPurchased();
                heighest.SetCategoryEquipping();
            }
        }
    }
}