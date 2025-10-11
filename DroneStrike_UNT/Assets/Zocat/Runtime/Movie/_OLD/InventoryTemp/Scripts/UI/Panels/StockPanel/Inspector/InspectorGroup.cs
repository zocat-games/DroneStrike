using TMPro;
using UnityEngine.UI;

namespace Zocat
{
    public class InspectorGroup : InstanceBehaviour
    {
        public TextMeshProUGUI Description;
        public ContentSizeFitter ContentSizeFitter;
        public ProgressGroup ProgressGroup;
        public SliderGroup SliderGroup;
        public BuyGroup BuyGroup;

        public void Show(ItemType itemType)
        {
            HideAll();
            SetDescriptoion(itemType);
            if (itemType.FromThisCategory(CategoryType.__Upgradable))
            {
                if (itemType.Purchased())
                {
                    BuyGroup.SetActive(false);
                    ProgressGroup.SetVisuals(itemType);
                    SliderGroup.SetVisuals(itemType);
                }
                else
                {
                    BuyGroup.SetVisual(itemType);
                    ProgressGroup.SetActive(false);
                    SliderGroup.SetActive(false);
                }
            }

            else
            {
                SliderGroup.SetVisuals(itemType);
                if (itemType.Amount() < ConfigManager.DisposableMax) BuyGroup.SetVisual(itemType);
                ProgressGroup.SetActive(false);
            }

            /*--------------------------------------------------------------------------------------*/
            ContentSizeFitter.Refresh();
        }

        private void SetDescriptoion(ItemType itemType)
        {
            Description.SetActive(true);
            Description.text = itemType.Description().Bold() == string.Empty ? itemType.Category().ToString().Bold() : $"{Description.text = itemType.Name().ToString().Bold()}\n{itemType.Description()}";
        }

        private void HideAll()
        {
            Description.SetActive(false);
            ProgressGroup.SetActive(false);
            SliderGroup.SetActive(false);
            BuyGroup.SetActive(false);
        }
    }
}