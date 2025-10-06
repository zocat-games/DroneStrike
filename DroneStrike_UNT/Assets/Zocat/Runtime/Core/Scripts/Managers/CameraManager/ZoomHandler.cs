using DG.Tweening;
using UnityEngine;
using EventHandler = Opsive.Shared.Events.EventHandler;

namespace Zocat
{
    public class ZoomHandler : SerializedInstance
    {
        private Tweener zoomTween;
        private int[] _zoomValues = { 40, 30, 15 };
        private int _currentIndex;

        void Start()
        {
            ApplyZoomValue(_zoomValues[_currentIndex]);
        }

        private void Update()
        {
            if (UiManager.GameSectionType == GameSectionType.Ui) return;
            Scroll();
        }

        private void Scroll()
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            if (scroll > 0f)
            {
                _currentIndex--;
                if (_currentIndex < 0)
                    _currentIndex = 0;
                OnZoomChanged(_zoomValues[_currentIndex]);
            }
            else if (scroll < 0f)
            {
                _currentIndex++;
                if (_currentIndex >= _zoomValues.Length)
                    _currentIndex = _zoomValues.Length - 1;
                OnZoomChanged(_zoomValues[_currentIndex]);
            }
        }

        void OnZoomChanged(int newZoom)
        {
            Debug.Log("Zoom değeri değişti: " + newZoom);
            ApplyZoomValue(newZoom);
        }

        void ApplyZoomValue(int value)
        {
            CameraManager.ActionCamera.fieldOfView = value;
        }

        /*--------------------------------------------------------------------------------------*/
    }
}