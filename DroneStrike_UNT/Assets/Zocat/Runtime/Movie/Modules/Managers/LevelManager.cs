using Opsive.Shared.Events;
using UnityEngine;

namespace Zocat
{
    public class LevelManager : MonoSingleton<LevelManager>
    {
        public LevelBase CurrentLevel;
        public bool LevelExists;

        private void Awake()
        {
            EventHandler.RegisterEvent(EventManager.AfterCreateLevel, AfterCreateLevel);
            EventHandler.RegisterEvent(EventManager.AfterCompleteLevel, AfterCompleteLevel);
        }

        private void AfterCompleteLevel()
        {
            LevelExists = false;
        }

        private void AfterCreateLevel()
        {
            LevelExists = true;
        }

        public void CreateCurrentMap()
        {
            if (CurrentLevel != null)
            {
                LoopTimerManager.Instance.KillAll();
                DestroyCurrentMap();
            }

            CurrentLevel = CloneTools.Instantiate<LevelBase>($"Levels/Level{LevelIndexManager.CurrentIndex}");
            Time.timeScale = 1;
        }

        public void DestroyCurrentMap()
        {
            Destroy(CurrentLevel.gameObject);
        }
    }
}