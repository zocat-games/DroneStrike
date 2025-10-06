using System.Collections;
using Opsive.Shared.Events;
using UnityEngine;

namespace Zocat
{
    using UnityEngine;

    public class InputDetector : MonoBehaviour
    {
        private bool inputDown;


        void Update()
        {
            if (Application.isMobilePlatform)
            {
                if (Input.touchCount == 1)
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                    {
                        EventHandler.ExecuteEvent(EventManager.UserInput, touch.position);
                    }
                }
            }
            else
            {
                if (Input.GetMouseButtonUp(0))
                {
                    EventHandler.ExecuteEvent(EventManager.UserInput, (Vector2)Input.mousePosition);
                }
            }
        }
    }
}