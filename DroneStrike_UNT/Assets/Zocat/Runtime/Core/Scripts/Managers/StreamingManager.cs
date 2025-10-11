using EventHandler = Opsive.Shared.Events.EventHandler;

namespace Zocat
{
    public class StreamingManager : MonoSingleton<StreamingManager>
    {
        public void ShowUi()
        {
            UiManager.HideAll();
            // UiManager.ShowPanel(PanelType.Chest);
            EventHandler.ExecuteEvent(EventManager.CurrencyChanged);
        }

        public void CreateLevel()
        {
            LevelManager.CreateCurrentMap();
            // UiManager.ShowActionPanels();
            EventHandler.ExecuteEvent(EventManager.AfterCreateLevel);
        }

        public void ExitLevel()
        {
            EventHandler.ExecuteEvent(EventManager.ExitLevel);
            // UiManager.ShowPanelOnSideBar(PanelType.Home);
            LevelManager.DestroyCurrentMap();
        }

        /*--------------------------------------------------------------------------------------*/
    }
}