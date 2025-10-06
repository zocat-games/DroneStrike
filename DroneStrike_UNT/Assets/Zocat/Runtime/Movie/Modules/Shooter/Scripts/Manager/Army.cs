using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using EventHandler = Opsive.Shared.Events.EventHandler;

namespace Zocat
{
    public class Army : MonoSingleton<Army>
    {
        public Dictionary<SubEnemyType, List<EnemyBase>> EnemyDic;

        private void Awake()
        {
            // foreach (var list in EnemyDic)
            // foreach (var e in list.Value)
            //     e.SetActive(false);
            OnExitLevel();
            EventHandler.RegisterEvent(EventManager.ExitLevel, OnExitLevel);
        }


        private void OnExitLevel()
        {
            foreach (var e in EnemyDic.SelectMany(list => list.Value))
            {
                e.SetActive(false);
                e.OnTheFront = false;
            }
        }

        public EnemyBase GetEnemy(SubEnemyType subEnemyType, Transform target)
        {
            var enemy = EnemyDic[subEnemyType].FirstOrDefault(_ => !_.OnTheFront);
            if (enemy == null)
            {
                Debug.LogError("No soldier found");
                return null;
            }

            enemy.EnemyEvents.Revive();
            enemy.transform.SetPositionAndRotation(target.position, target.rotation);

            return enemy;
        }


        [Button(ButtonSizes.Medium)]
        public void GelAll()
        {
            var enemyBases = GetComponentsInChildren<EnemyBase>();
            foreach (var item in enemyBases) EnemyDic[item.SubEnemyType].Clear();
            foreach (var item in enemyBases) EnemyDic[item.SubEnemyType].Add(item);
        }
    }


    public static class SubEnemyTypeHelper
    {
        public static SubEnemyType[] GetGroup(int groupValue)
        {
            return Enum.GetValues(typeof(SubEnemyType))
                .Cast<SubEnemyType>()
                .Where(x => (int)x / 100 * 100 == groupValue)
                .ToArray();
        }
    }
}