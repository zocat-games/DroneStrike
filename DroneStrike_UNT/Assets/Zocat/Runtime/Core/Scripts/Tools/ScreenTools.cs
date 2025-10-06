using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScreenTools
{
    public static Vector2 MouseToCanvas => new Vector2(Input.mousePosition.x - (Screen.width / 2), Input.mousePosition.y - (Screen.height / 2));
}