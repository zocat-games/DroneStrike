using System.Collections;
using UnityEngine;

namespace Zocat
{
    // using ControlFreak2;

    public class Turntable : InstanceBehaviour
    {
        private float smoothRef;
        private float smoothCalc;
        private float mouseX;
        private bool turnable = true;

        public void SetTurnable(bool _turnable)
        {
            turnable = _turnable;
        }

        private void Update()
        {
            // if (!turnable) return;
            // var rot = transform.localEulerAngles;
            // mouseX = CF2Input.GetAxis("Mouse X");
            // smoothCalc = Mathf.SmoothDamp(smoothCalc, 0, ref smoothRef, 1);
            // if (CF2Input.GetButton("Fire1")) smoothCalc = 0;
            // if (CF2Input.GetButtonUp("Fire1")) smoothCalc = mouseX;
            // transform.localEulerAngles = new Vector3(0, rot.y - (mouseX + smoothCalc), 0);
        }
    }
}