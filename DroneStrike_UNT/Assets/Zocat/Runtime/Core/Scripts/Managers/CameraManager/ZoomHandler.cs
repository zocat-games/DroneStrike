using DG.Tweening;
using UnityEngine;
using EventHandler = Opsive.Shared.Events.EventHandler;

namespace Zocat
{
    public class ZoomHandler : SerializedInstance
    {
        private readonly float _zoomSpeed = 3;
        private readonly float minToggleTime = .5f;
        private readonly int ZoomOutFov = 60;
        private Camera _camera;
        private float _prevFire2Time;
        private bool _staying;
        private bool _zooming;
        private float currentFov;
        private float ZoomInFov;
        private Tweener zoomTween;

        private void Awake()
        {
            // EventHandler.RegisterEvent<CategoryType>(EventManager.WeaponChanged, SetZoomValue);
            // EventHandler.RegisterEvent(EventManager.StayingStarted, OnStayingStarted);
            // EventHandler.RegisterEvent(EventManager.EnteringStarted, OnEnteringStarted);
            _camera = CameraManager.ActionCamera;
            // currentFov = ZoomOutFov;
        }

        private void Update()
        {
            // _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, currentFov, Time.deltaTime * _zoomSpeed);
            // _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, currentFov, Time.deltaTime * _zoomSpeed);
        }

        private void SetZoomValue()
        {
            // ZoomInFov = HeroWeaponManager.HeroWeaponSetter.WeaponConfigs[categoryType].ZoomAmount;
            // ZoomTween(ZoomInFov);
        }

        // private void OnStayingStarted()
        // {
        //     currentFov = ZoomInFov;
        //     EventHandler.ExecuteEvent(EventManager.ZoomIn);
        //     ZoomTween(currentFov);
        // }
        //
        // private void OnEnteringStarted()
        // {
        //     currentFov = ZoomOutFov;
        //     EventHandler.ExecuteEvent(EventManager.ZoomOut);
        //     ZoomTween(currentFov);
        // }
        //
        //
        // private void ZoomTween(float value)
        // {
        //     // return;
        //     zoomTween?.Kill();
        //     zoomTween = _camera.DOFieldOfView(value, 1).SetEase(Ease.OutQuad);
        // }
        /*--------------------------------------------------------------------------------------*/
    }
}