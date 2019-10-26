using TMPro;
using UnityEngine;

public class WeaponIndicator : MonoBehaviour {
    public TextMeshProUGUI weaponName;
    public TextMeshProUGUI weaponBullets;

    public void UpdateWeaponIndicator (BaseWeapon weapon) {
        weaponName.text = string.Format ("{0}", weapon.Name);
        weaponBullets.text = string.Format ("{0}",
            weapon.Bullets == BaseWeapon.INFINITE_BULLETS ? "Infinite" :
            weapon.Bullets.ToString ());
    }
}