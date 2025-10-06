using UnityEngine;

namespace HWRWeaponSystem
{
    public class TankHUD : MonoBehaviour
    {
        // private int currentWeapon;
        // private LauncherController launcherManager;
        //
        // private void Start()
        // {
        //     launcherManager = GetComponent<LauncherController>();
        // }
        //
        // private void Update()
        // {
        //     if (!launcherManager)
        //         return;
        //     if (Input.GetAxis("Mouse ScrollWheel") < 0)
        //     {
        //         // back
        //         launcherManager.CurrentWeapon -= 1;
        //         if (launcherManager.CurrentWeapon < 0)
        //             launcherManager.CurrentWeapon = launcherManager.WeaponLists.Length - 1;
        //     }
        //
        //     if (Input.GetAxis("Mouse ScrollWheel") > 0)
        //     {
        //         launcherManager.CurrentWeapon += 1;
        //         if (launcherManager.CurrentWeapon >= launcherManager.WeaponLists.Length) launcherManager.CurrentWeapon = 0;
        //     }
        //
        //     currentWeapon = launcherManager.CurrentWeapon;
        // }

        // private void OnGUI()
        // {
        //     if (!weaponManager || currentWeapon > weaponManager.WeaponLists.Length)
        //         return;
        //
        //     GUI.skin.label.fontSize = 15;
        //     GUI.Label(new Rect(20, 20, 300, 30), "Weapon Index " + currentWeapon);
        //     GUI.Label(new Rect(20, Screen.height - 95, 300, 30), "Esc back to mainmenu");
        //     GUI.Label(new Rect(20, Screen.height - 50, 300, 30), "Scroll Mouse to Change weapons");
        //     GUI.Label(new Rect(20, Screen.height - 70, 300, 30), "W A S D to Move");
        //     if (!weaponManager.WeaponLists[currentWeapon].InfinityAmmo)
        //         GUI.Label(new Rect(20, 70, 300, 50), "Ammo " + weaponManager.WeaponLists[currentWeapon].Ammo + " / " + weaponManager.WeaponLists[currentWeapon].AmmoMax);
        //     else
        //         GUI.Label(new Rect(20, 70, 300, 50), "Infinity ammo");
        //
        //     GUI.skin.label.fontSize = 25;
        //     GUI.Label(new Rect(20, 40, 300, 50), "" + weaponManager.WeaponLists[currentWeapon].name);
        // }
    }
}