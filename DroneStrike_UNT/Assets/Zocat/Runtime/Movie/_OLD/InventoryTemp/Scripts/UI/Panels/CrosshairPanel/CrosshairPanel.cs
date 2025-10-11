using Opsive.Shared.Events;

namespace Zocat
{
    public class CrosshairPanel : UIPanel
    {
        private bool _showing;

        public override void Initialize()
        {
            base.Initialize();
            EventHandler.RegisterEvent(EventManager.ZoomIn, OnZoomIn);
            EventHandler.RegisterEvent(EventManager.ZoomOut, OnZoomOut);
            EventHandler.RegisterEvent<bool>(EventManager.Reload, OnReload);
        }

        private void OnReload(bool reloading)
        {
            if (reloading && _showing) Hide();
            if (!reloading && _showing) Show();
        }

        private void OnZoomOut()
        {
            Hide();
            _showing = false;
        }

        private void OnZoomIn()
        {
            Show();
            _showing = true;
        }
    }
}