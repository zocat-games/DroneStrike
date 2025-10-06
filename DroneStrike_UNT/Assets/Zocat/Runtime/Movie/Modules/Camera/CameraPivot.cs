using DG.Tweening;
using Opsive.Shared.Events;
using UnityEngine;

namespace Zocat
{
    public class CameraPivot : SerializedInstance
    {
        public CameraLook CameraLook;
        public Transform WeaponPoint;

        private void Awake()
        {
            EventHandler.RegisterEvent(EventManager.AfterCreateLevel, AfterCreateLevel);
        }

        private void AfterCreateLevel()
        {
            transform.position = LevelManager.CurrentLevel.CenterPoint.position;
            transform.rotation = LevelManager.CurrentLevel.CenterPoint.rotation;
            CameraLook.Initialize(transform);
        }
    }
}