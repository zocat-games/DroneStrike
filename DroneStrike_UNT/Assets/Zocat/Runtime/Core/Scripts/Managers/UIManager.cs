using System.Collections.Generic;

namespace Zocat
{
    public class UIManager : MonoSingleton<UIManager>
    {
        public TopBarPanel TopBarPanel;
        public MessagePanel MessagePanel;
        public HomePanel HomePanel;

        /*--------------------------------------------------------------------------------------*/
        public GameSectionType GameSectionType { get; set; }
        /*--------------------------------------------------------------------------------------*/
        private List<UIPanel> _UiPanels;
        public Dictionary<PanelType, UIPanel> PanelDic;

        public void Initialize()
        {
            _UiPanels = new List<UIPanel>
            {
                TopBarPanel, MessagePanel, HomePanel
            };

            _UiPanels.ForEach(_UiPanel =>
            {
                _UiPanel.SetActive(true);
                _UiPanel.Initialize();
                _UiPanel.Hide();
            });
        }

        public void HideAll()
        {
            _UiPanels.ForEach(_ => _.Hide());
        }

        public void ShowHome()
        {
            HideAll();
            HomePanel.Show();
        }
        


        /*--------------------------------------------------------------------------------------*/
        private void ShowDefaults()
        {
            GameSectionType = GameSectionType.Ui;
            _UiPanels.ForEach(_ => _.Hide());
            TopBarPanel.Show();
        }

        public void ShowPanel(PanelType panelType)
        {
            HideAll();
            PanelDic[panelType].Show();
        }

        public void ShowActionPanels()
        {
            HideAll();
            GameSectionType = GameSectionType.Action;
        }
    }

    public enum PanelType
    {
        Home = 0,
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