using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
#endif

namespace Zocat
{
    public class Sector : InstanceBehaviour
    {
        public int Radius;
        public List<EnemyBase> Units;
        public List<UnitPoint> Points;
        private bool _alarmed;


        public void Alarm()
        {
            if (_alarmed) return;
            _alarmed = true;
            Units.ForEach(_ => _.EnemyEvents.Alarm());
        }

        public void CreateEnemies()
        {
            IsoHelper.Log(this, MethodBase.GetCurrentMethod().Name);
            foreach (var item in Points)
            {
                var enemy = Army.GetEnemy(item.SubEnemyType, item.transform);
                Units.Add(enemy);
            }
        }

        #region Defaults

        [Button(ButtonSizes.Medium)]
        public void SetVariables()
        {
            Points = GetComponentsInChildren<UnitPoint>().ToList();
            foreach (var item in Points) item.name = item.SubEnemyType.ToString();
        }

        private void OnDrawGizmos()
        {
            if (!EditorHelper.DrawGimos) return;
#if UNITY_EDITOR
            // Handles.color = Color.cyan;
            Gizmos.color = Color.gray1;
            Gizmos.DrawSphere(transform.position, 0.2f);
            ZocatGizmos.DrawWireCircle(transform.position, Radius, Color.gray1);
#endif
        }

        #endregion
    }
}