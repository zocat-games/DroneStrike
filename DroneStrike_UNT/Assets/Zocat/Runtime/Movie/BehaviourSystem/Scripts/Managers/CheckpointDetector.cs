using Opsive.Shared.Events;

namespace Zocat
{
    public class CheckpointDetector : InstanceBehaviour
    {
        public int _checkPointIndex;
        private bool _reached;
        private bool _started;
        private bool countable;
        private HeroMain HeroMain;
        public CheckPoint CurrentCheckPoint => LevelManager.CurrentLevel.ScenarioPath.CheckPoints[_checkPointIndex];
        public CheckPoint NextCheckPoint => LevelManager.CurrentLevel.ScenarioPath.CheckPoints[_checkPointIndex + 1];

        public ScenarioPath ScenarioPath { get; set; }

        private void Awake()
        {
            HeroMain = GetComponent<HeroMain>();
            EventHandler.RegisterEvent(EventManager.AfterCreateLevel, AfterCreateLevel);
        }

        private void AfterCreateLevel()
        {
            LevelManager.CurrentLevel.Sectors[_checkPointIndex].CreateEnemies();
        }

        public void StartEntering()
        {
            HeroMain.Mover.SetTarget(NextCheckPoint.transform);
            LevelManager.CurrentLevel.Sectors[_checkPointIndex + 1].CreateEnemies();
            EventHandler.ExecuteEvent(EventManager.EnteringStarted);
        }

        private void OnStayingStarted()
        {
            EventHandler.ExecuteEvent(EventManager.StayingStarted);
        }

        public void SetCheckIndex(int index)
        {
            _checkPointIndex = index;
            OnStayingStarted();
        }

        // public void OnFinishSector()
        // {
        //     StartEntering();
        // }
    }
}