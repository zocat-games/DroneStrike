using TMPro;
using UnityEngine.UI;

namespace Zocat
{
    public class ItemDisplayBase : InstanceBehaviour
    {
        public ItemTitle ItemTitle;
        public FillRatio FillRatio;
        public CustomButton CustomButton;
        public Image Icon;
        public Image Lock;
        public TextMeshProUGUI Text;
        public ItemType ItemType { get; set; }
        public Image _image { get; set; }

        public bool AvailableToGo { get; set; } = true;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        private void Start()
        {
            CustomButton.InitializeClick(Click);
        }

        protected virtual void ResetDisplay()
        {
            ItemTitle.SetActive(true);
            FillRatio.SetActive(false);
            Icon.SetActive(true);
            Lock.SetActive(false);
            Text.SetActive(false);
        }

        public virtual void SetVisuals(ItemType itemType)
        {
            ResetDisplay();
            ItemType = itemType;
            Icon.sprite = itemType.Icon();
            Icon.material = itemType.Unlocked() ? null : InventoryDepot.White;
            ItemTitle.Title.text = itemType.Name();
            FillRatio.SetActive(ItemType.Unlocked());
            Text.SetActive(ItemType.Unlocked());
            Lock.SetActive(!ItemType.Unlocked());
        }

        public void Click()
        {
            if (!ItemType.Unlocked()) return;
            UiManager.StockPanel.OnItemClick(ItemType);
        }

        public void SetHighlight(bool selected)
        {
            _image.color = selected ? InventoryDepot.ColorsDic[ColorType.ItemButton1] : InventoryDepot.ColorsDic[ColorType.ItemButton0];
        }
    }
}