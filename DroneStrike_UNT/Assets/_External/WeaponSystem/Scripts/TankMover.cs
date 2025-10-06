using UnityEngine;

namespace HWRWeaponSystem
{
    public class TankMover : MonoBehaviour
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

            transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * TurnSpeed * Time.deltaTime, 0));
            transform.position += transform.forward * Input.GetAxis("Vertical") * Speed * Time.deltaTime;
        }
    }
}