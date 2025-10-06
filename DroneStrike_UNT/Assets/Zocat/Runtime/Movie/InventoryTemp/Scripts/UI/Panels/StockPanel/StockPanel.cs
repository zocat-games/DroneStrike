using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace Zocat
{
    public class StockPanel : SerializedUIPanel
    {
        public ShowcaseGroup ShowcaseGroup;
        public InspectorGroup InspectorGroup;
        public Dictionary<AttributeType, AttributeRefSO> AttributeRefSos;

        /*--------------------------------------------------------------------------------------*/

        public Dictionary<CategoryType, List<AttributeType>> CategoryAttributeList;
        public CategoryType CategoryType { get; set; }
        public bool CategoryDurable => CategoryType is CategoryType._Gun or CategoryType._Armor;

        /*--------------------------------------------------------------------------------------*/
        public override void Initialize()
        {
            base.Initialize();
            ShowcaseGroup.ShowcaseTabGroup.Initialize();
        }

        public override void Show()
        {
            base.Show();
            ProgressManager.Check();
            ShowcaseGroup.ShowcaseTabGroup.StockTabButtons[CategoryType._Gun].Click();
        }
        /*--------------------------------------------------------------------------------------*/

        public void OnClickTabButton(CategoryType categoryType)
        {
            CategoryType = categoryType;
            ShowcaseGroup.Showcase.Show(categoryType);
        }

        public void OnItemClick(ItemType itemType)
        {
            ShowcaseGroup.Showcase.Displays.ForEach(_ => _.SetHighlight(false));
            ShowcaseGroup.Showcase.Displays.FirstOrDefault(_ => _.ItemType == itemType).SetHighlight(true);
            InspectorGroup.Show(itemType);
            WeaponStudio.Show(itemType);
        }

        public void RefreshPanel(ItemType itemType)
        {
            OnClickTabButton(CategoryType);
            OnItemClick(itemType);
        }

        /*--------------------------------------------------------------------------------------*/
        //
        [Button(ButtonSizes.Medium)]
        public void Test(ItemType itemType)
        {
            IsoHelper.Log(itemType.Price());
        }
    }
}