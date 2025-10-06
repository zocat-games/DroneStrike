using System.Collections.Generic;

namespace Zocat
{
    public class UIManager : MonoSingleton<UIManager>
    {
        public TopBarPanel TopBarPanel;
        public MessagePanel MessagePanel;
        public DisplayDepotPanel DisplayDepotPanel;
        /*--------------------------------------------------------------------------------------*/
        public SideBarPanel SideBarPanel;
        public MapPanel MapPanel;
        public EquipmentPanel EquipmentPanel;
        public StockPanel StockPanel;
        public ChestPanel ChestPanel;
        public MarketPanel MarketPanel;
        /*--------------------------------------------------------------------------------------*/
        public GamepadPanel GamepadPanel;
        public WeaponPanel WeaponPanel;
        public CrosshairPanel CrosshairPanel;
        public ReloadPanel ReloadPanel;
        /*--------------------------------------------------------------------------------------*/
        public BackButtonPanel BackButtonPanel;
        public HealthPanel HealthPanel;

        /*--------------------------------------------------------------------------------------*/
        public GameSectionType GameSectionType;
        /*--------------------------------------------------------------------------------------*/
        private List<UIPanel> _UiPanels;
        public Dictionary<PanelType, UIPanel> PanelDic;

        public void Initialize()
        {
            _UiPanels = new List<UIPanel>
            {
                MapPanel, SideBarPanel, EquipmentPanel, StockPanel, DisplayDepotPanel,
                TopBarPanel, MessagePanel, ChestPanel, MarketPanel, GamepadPanel, WeaponPanel,
                CrosshairPanel, ReloadPanel, BackButtonPanel, HealthPanel
            };

            _UiPanels.ForEach(_UiPanel =>
            {
                _UiPanel.SetActive(true);
                _UiPanel.Initialize();
                _UiPanel.Hide();
            });
        }

        private void ShowDefaults()
        {
            GameSectionType = GameSectionType.Ui;
            _UiPanels.ForEach(_ => _.Hide());
            TopBarPanel.Show();
            SideBarPanel.Show();
        }

        public void HideAll()
        {
            _UiPanels.ForEach(_ => _.Hide());
        }

        /*--------------------------------------------------------------------------------------*/
        public void ShowPanelOnSideBar(PanelType panelType)
        {
            ShowDefaults();
            SideBarPanel.SetVisuals(panelType);
            PanelDic[panelType].Show();
        }

        public void ShowPanel(PanelType panelType)
        {
            HideAll();
            PanelDic[panelType].Show();
        }

        public void ShowActionPanels()
        {
            HideAll();
            // GamepadPanel.Show();
            WeaponPanel.Show();
            BackButtonPanel.Show();
            HealthPanel.Show();
            GameSectionType = GameSectionType.Action;
        }
    }

    public enum PanelType
    {
        Map = 0,
        Equipment = 1,
        Stock = 2,
        Chest = 3,
        Market = 4,
        Gamepad = 5
    }

    public enum GameSectionType
    {
        None = 0,
        Ui = 1,
        Action = 2
    }
}