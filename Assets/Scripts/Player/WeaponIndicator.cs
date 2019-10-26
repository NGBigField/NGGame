using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponIndicator : MonoBehaviour {
    public TextMeshProUGUI weaponName;
    public TextMeshProUGUI weaponBullets;
    private RawImage weaponImage;

    private void Awake () {
        weaponImage = GetComponent<RawImage> ();
    }

    public void UpdateWeaponIndicator (BaseWeapon weapon) {
        weaponImage.texture = weapon.Icon;
        weaponName.text = string.Format ("{0}", weapon.Name);
        weaponBullets.text = string.Format ("{0}",
            weapon.Bullets == BaseWeapon.INFINITE_BULLETS ? "Infinite" :
            weapon.Bullets.ToString ());
    }
}