using Opsive.Shared.Events;
using UnityEngine;

namespace Zocat
{
    public class SceneEtcManager : MonoSingleton<SceneEtcManager>
    {
        public GameObject HideMain;

        private void Awake()
        {
            EventHandler.RegisterEvent(EventManager.AfterCreateLevel, Hide);
        }

        private void Hide()
        {
            HideMain.SetActive(false);
        }
    }
}