using System.Collections.Generic;
using Sirenix.Utilities;

namespace Zocat
{
    public class SideBarPanel : SerializedUIPanel
    {
        public Dictionary<PanelType, SideBarButton> SideBarButtons;

        public override void Initialize()
        {
            base.Initialize();
            SideBarButtons.ForEach(_ => _.Value.Initialize());
        }

        public void SetVisuals(PanelType panelType)
        {
            SideBarButtons.ForEach(_ => _.Value.Hightlight(false));
            SideBarButtons[panelType].Hightlight(true);
        }
    }
}