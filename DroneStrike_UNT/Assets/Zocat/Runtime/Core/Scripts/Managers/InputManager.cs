using Opsive.Shared.Events;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Zocat
{
    public class InputManager : MonoSingleton<InputManager>
    {
        public bool MouseRotationEnabled;
        public bool Staying;

        private void Awake()
        {
            EventHandler.RegisterEvent(EventManager.StayingStarted, OnStayingStarted);
            EventHandler.RegisterEvent(EventManager.EnteringStarted, OnEnteringStarted);
            EventHandler.RegisterEvent(EventManager.AfterCreateLevel, AfterCreateLevel);
            EventHandler.RegisterEvent(EventManager.ExitLevel, OnExitLevel);
            EventHandler.RegisterEvent(EventManager.AfterCompleteLevel, OnExitLevel);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                MouseRotationEnabled = false;
                ShowCursor();
            }

            if (Staying && Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && UiManager.GameSectionType == GameSectionType.Action)
            {
                MouseRotationEnabled = true;
                HideCursor();
            }
        }

        private void AfterCreateLevel()
        {
            MouseRotationEnabled = false;
            HideCursor();
        }

        private void OnExitLevel()
        {
            MouseRotationEnabled = false;
            ShowCursor();
        }

        public void ShowCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void HideCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void OnStayingStarted()
        {
            Staying = true;
            InputManager.MouseRotationEnabled = true;
        }

        private void OnEnteringStarted()
        {
            Staying = false;
            InputManager.MouseRotationEnabled = false;
        }
    }
}