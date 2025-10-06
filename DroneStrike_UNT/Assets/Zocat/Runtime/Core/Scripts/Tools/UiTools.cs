using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.EventSystems;

namespace Zocat
{
    public static class UiTools
    {
        // public static Vector2 WorldToCanvasPoint(GameObject fromGameObject)
        // {
        //     RectTransformUtility.ScreenPointToLocalPointInRectangle(GameManager.instance.UiManager.CanvasRect, Camera.main.WorldToScreenPoint(fromGameObject.transform.position), GameManager.instance.UiManager.CameraUi, out var pos);
        //     return pos;
        // }

        public static GameObject FirstRaycastedUIElement
        {
            get
            {
                var eventData = new PointerEventData(EventSystem.current);
                eventData.position = Input.mousePosition;
                var results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventData, results);
                if (results.Any(r => r.gameObject.layer == 5))
                {
                    return results[0].gameObject;
                }

                return null;
            }
        }

        public static bool ClickedContextIsMine()
        {
            var eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            if (results.Any(r => r.gameObject.layer == 5))
            {
                return results[0].gameObject;
            }

            return false;
        }
    }
}