using Opsive.Shared.Events;
using UnityEngine;

namespace Zocat
{
    public class GamepadPanel : UIPanel
    {
        public GameObject Fire;
        public GameObject Rotation;

        public override void Initialize()
        {
            base.Initialize();
            Fire.SetActive(false);
            EventHandler.RegisterEvent(EventManager.EnteringStarted, OnEnteringStarted);
            EventHandler.RegisterEvent(EventManager.StayingStarted, OnStayingStarted);
            EventHandler.RegisterEvent(EventManager.ZoomIn, OnZoomIn);
            EventHandler.RegisterEvent(EventManager.ZoomOut, OnZoomOut);
        }

        private void OnZoomOut()
        {
            Fire.SetActive(false);
        }

        private void OnZoomIn()
        {
            Fire.SetActive(true);
        }

        private void OnStayingStarted()
        {
            // if (InputManager.IsMobile) Show();
        }

        private void OnEnteringStarted()
        {
            Hide();
        }
    }
}