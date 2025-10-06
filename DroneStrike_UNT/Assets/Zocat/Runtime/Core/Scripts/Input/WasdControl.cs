using UnityEngine;

namespace Zocat
{
    public static class WasdControl
    {
        private static float hor;
        private static float ver;

        public static Vector2 Out()
        {
            // var hor = 0f;
            // var ver = 0f;
            if (Input.GetKey(KeyCode.W)) ver = 1;
            if (Input.GetKey(KeyCode.S)) ver = -1;
            if (Input.GetKey(KeyCode.A)) hor = -1;
            if (Input.GetKey(KeyCode.D)) hor = 1;


            return new Vector2(hor, ver);
        }
    }
}