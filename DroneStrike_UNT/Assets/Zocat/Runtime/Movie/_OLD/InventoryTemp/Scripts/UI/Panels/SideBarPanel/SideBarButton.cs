using UnityEngine.UI;

namespace Zocat
{
    public class SideBarButton : InstanceBehaviour
    {
        public PanelType PanelType;
        public CustomButton Button;
        private Image _image;

        public void Initialize()
        {
            Button.InitializeClick(Click);
            _image = transform.Find("Icon").GetComponent<Image>();
        }

        private void Click()
        {
            // UiManager.ShowPanelOnSideBar(PanelType);
        }

        public void Hightlight(bool clicked)
        {
            _image.color = clicked ? InventoryDepot.ColorsDic[ColorType.ItemButton1] : InventoryDepot.ColorsDic[ColorType.ItemButton0];
        }
    }
}