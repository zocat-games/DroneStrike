using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Zocat
{
    public class DraggingManager : InstanceBehaviour
    {
        /*--------------------------------------------------------------------------------------*/
        public bool IsDown;
        public bool IsDragging;
        private float _xDistance, _yDistance;
        private int _PartIndex;
        private Vector2 _downPoint, _exitPoint;


        /*--------------------------------------------------------------------------------------*/
        public void OnPointerDown(PointerEventData pointerEventData, int index)
        {
            _downPoint = pointerEventData.position;
            _PartIndex = index;
            IsDown = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _exitPoint = eventData.position;
            _xDistance = MathF.Abs(_downPoint.x - _exitPoint.x);
            _yDistance = MathF.Abs(_downPoint.y - _exitPoint.y);
            if (eventData.pointerCurrentRaycast.gameObject?.tag == "Thumbnail") return;
            if (_xDistance / 3 > _yDistance) return;
            if (!IsDown) return;
            IsDragging = true;
            StartDragging();
        }

        /*--------------------------------------------------------------------------------------*/

        private void Update()
        {
            // if (IsDragging) UpdateDragging();
        }

        void FingerUp()
        {
            IsDown = false;
            IsDragging = false;
        }
        /*--------------------------------------------------------------------------------------*/

        private void StartDragging()
        {
        }

        private void UpdateDragging()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition + new Vector3(0, 200, 0));
            if (Physics.Raycast(ray, out hit, 100, 1))
            {
            }
        }

        public void StopDragging()
        {
        }

        public void CompleteDragging()
        {
            IsDown = false;
        }
    }
}