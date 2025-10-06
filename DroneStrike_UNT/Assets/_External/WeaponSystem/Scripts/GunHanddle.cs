using UnityEngine;

namespace HWRWeaponSystem
{
    public class GunHanddle : MonoBehaviour
    {
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
        }
    }
}