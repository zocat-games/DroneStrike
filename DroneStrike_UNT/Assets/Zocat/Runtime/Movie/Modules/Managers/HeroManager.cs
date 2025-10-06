using Opsive.Shared.Events;

namespace Zocat
{
    public class HeroManager : MonoSingleton<HeroManager>
    {
        public HeroMain HeroMain;

        private void Awake()
        {
            HeroMain.SetActive(false);
            EventHandler.RegisterEvent(EventManager.ExitLevel, OnExitLevel);
            EventHandler.RegisterEvent(EventManager.AfterCreateLevel, AfterCreateLevel);
        }

        private void AfterCreateLevel()
        {
            HeroMain.AfterCreateLevel();
        }

        private void OnExitLevel()
        {
            HeroMain.gameObject.SetActive(false);
        }
    }
}