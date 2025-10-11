using Opsive.Shared.Events;

namespace Zocat
{
    public class WeaponPanel : UIPanel
    {
        public WeaponsUiGroup WeaponsUiGroup;

        public override void Initialize()
        {
            base.Initialize();
            WeaponsUiGroup.Initialize();
            EventHandler.RegisterEvent(EventManager.EnteringStarted, OnEnteringStarted);
            EventHandler.RegisterEvent(EventManager.StayingStarted, OnStayingStarted);
        }

        private void OnEnteringStarted()
        {
            Hide();
        }

        private void OnStayingStarted()
        {
            Show();
        }

        /*--------------------------------------------------------------------------------------*/
    }
}