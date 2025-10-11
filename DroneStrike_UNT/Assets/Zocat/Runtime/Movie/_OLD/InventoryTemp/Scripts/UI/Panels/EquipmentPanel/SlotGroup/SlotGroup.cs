using System.Collections.Generic;
using Sirenix.Utilities;

namespace Zocat
{
    public class SlotGroup : SerializedInstance
    {
        public Dictionary<CategoryType, SlotDisplayBase> SlotDisplays;


        public void Initialize()
        {
            SlotDisplays.ForEach(_ => _.Value.Initialize());
        }

        public void Show()
        {
            foreach (var item in SlotDisplays) item.Value.SetVisuals();
            OnClickSlot(CategoryType.MachineGun);
        }

        public void OnClickSlot(CategoryType category)
        {
            SlotDisplays.ForEach(_ => _.Value.SetHighlighted(false));
            SlotDisplays[category].SetHighlighted(true);
            // UiManager.EquipmentPanel.CategoryGroup.Show(category);
        }

        public void OnClickCategory(CategoryType category)
        {
            foreach (var item in SlotDisplays) item.Value.SetVisuals();
        }
    }
}