using UnityEngine;

namespace HWRWeaponSystem
{
    public class TankMover2D : MonoBehaviour
    {
        public float Speed = 20;
        public float TurnSpeed = 100;
        public LauncherController launcher;

        private void Start()
        {
            launcher = transform.GetComponentInChildren(typeof(LauncherController)).GetComponent<LauncherController>();
        }

        private void Update()
        {
            // if (Input.GetButton("Fire1"))
            //     if (launcher)
            //         launcher.LaunchWeapon();

            transform.position += Vector3.right * Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        }
    }
}