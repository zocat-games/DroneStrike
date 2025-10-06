using System.Collections.Generic;
using System.Linq;
using Opsive.Shared.Events;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Zocat
{
    public class LevelBase : InstanceBehaviour
    {
        public Transform CenterPoint;
        public ScenarioPath ScenarioPath;
        public List<Sector> Sectors;
        public int _sectorCounter;
        public Sector CurrentSector => Sectors[HeroManager.HeroMain.CheckpointDetector._checkPointIndex];

        public void CreateSectorEnemies()
        {
            // CurrentSector.CreateEnemies();
        }

        public void FinishSector()
        {
            InputManager.MouseRotationEnabled = false;
            if (HeroManager.HeroMain.CheckpointDetector._checkPointIndex == Sectors.Count - 1)
            {
                ScenarioManager.LevelFinished = true;
                IsoHelper.Log("level bitti");
                EventHandler.ExecuteEvent(EventManager.AfterCompleteLevel);
            }
        }

        // public void AlarmSector()
        // {
        //     CurrentSector.Alarm();
        // }

        /*--------------------------------------------------------------------------------------*/
        // [Button(ButtonSizes.Medium)]
        // public void SetVariables()
        // {
        //     Sectors.Clear();
        //     Sectors = GetComponentsInChildren<Sector>().ToList();
        //     Sectors.RenameByIndex("Sector");
        //     foreach (var sector in Sectors) sector.SetVariables();
        //     ScenarioPath.SetVariables();
        //     /*--------------------------------------------------------------------------------------*/
        //     for (var i = 0; i < ScenarioPath.CheckPoints.Count; i++)
        //     {
        //         var item = ScenarioPath.CheckPoints[i];
        //         item.transform.LookY(Sectors[i].transform);
        //         item.Target = Sectors[i].transform;
        //     }
        // }

        // [Button(ButtonSizes.Medium)]
        // public void LookCheckPoints()
        // {
        //     for (var i = 0; i < ScenarioPath.CheckPoints.Count; i++)
        //     {
        //         var item = ScenarioPath.CheckPoints[i];
        //         item.transform.LookY(Sectors[i].transform);
        //     }
        // }
    }
}