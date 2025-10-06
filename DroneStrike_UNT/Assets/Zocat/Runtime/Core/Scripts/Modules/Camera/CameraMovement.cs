using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Zocat
{
    public class CameraMovement : CameraBase
    {
        // public Transform Target;
        // public Transform CameraParent;
        // private Vector2 lastTouchPosition;
        // private bool isDragging;
        // public float dragSpeed = 2f;
        // // private float moveLerp = 10f;
        //
        // void Update()
        // {
        //     if (!InputManager.Enabled || EventSystem.current.IsPointerOverGameObject()) return;
        //     /*--------------------------------------------------------------------------------------*/
        //     if (Input.touchCount == 1)
        //     {
        //         Touch touch = Input.GetTouch(0);
        //
        //         if (touch.phase == TouchPhase.Began)
        //         {
        //             lastTouchPosition = touch.position;
        //             isDragging = true;
        //         }
        //         else if (touch.phase == TouchPhase.Moved && isDragging)
        //         {
        //             Vector2 delta = touch.position - lastTouchPosition;
        //             MoveTarget(delta);
        //             lastTouchPosition = touch.position;
        //         }
        //         else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
        //         {
        //             isDragging = false;
        //         }
        //     }
        //     /*--------------------------------------------------------------------------------------*/
        //     else if (Input.GetMouseButtonDown(0))
        //     {
        //         lastTouchPosition = Input.mousePosition;
        //         isDragging = true;
        //     }
        //     else if (Input.GetMouseButton(0) && isDragging)
        //     {
        //         Vector2 delta = (Vector2)Input.mousePosition - lastTouchPosition;
        //         MoveTarget(delta);
        //         lastTouchPosition = Input.mousePosition;
        //     }
        //     else if (Input.GetMouseButtonUp(0))
        //     {
        //         isDragging = false;
        //     }
        //
        //     /*--------------------------------------------------------------------------------------*/
        //     MoveTarget(WasdControl.Out() * -10f);
        // }
        //
        // private void MoveTarget(Vector2 delta)
        // {
        //     Vector3 move = new Vector3(-delta.x * dragSpeed, 0, -delta.y * dragSpeed);
        //     cameraManager.Target.transform.Translate(move, Space.Self);
        // }
        //
        // private void FixedUpdate()
        // {
        //     CameraParent.position = Vector3.Lerp(CameraParent.position, cameraManager.Target.position, Time.deltaTime * 10);
        // }
    }
}