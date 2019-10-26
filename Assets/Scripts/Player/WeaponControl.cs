using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    public AudioClip weaponSwapSound;

    public WeaponIndicator weaponIndicator;

    private AudioSource audioSource;

    private BaseWeapon equippedWeapon;

    public BaseWeapon EquippedWeapon
    {
        get { return equippedWeapon; }
        set
        {
            if (equippedWeapon) equippedWeapon.enabled = false;

            equippedWeapon = value;
            equippedWeapon.enabled = true;

            if (audioSource) audioSource.PlayOneShot(weaponSwapSound);
        }
    }

    public int EquippedWeaponIndex
    {
        get
        {
            var weapons = GetWeapons();
            var currWeaponIndex = 0;
            foreach (var weapon in weapons)
            {
                if (weapon == equippedWeapon) return currWeaponIndex;
                ++currWeaponIndex;
            }

            return -1;
        }
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        EquippedWeapon = GetComponent<BaseWeapon>();
    }

    private void Update()
    {
        weaponIndicator.UpdateWeaponIndicator(equippedWeapon);
    }

    public BaseWeapon[] GetWeapons()
    {
        return GetComponents<BaseWeapon>();
    }

    public T AddWeapon<T>() where T : BaseWeapon
    {
        var weapon = gameObject.AddComponent<T>();
        weapon.enabled = false;

        return weapon;
    }

    public void NextWeapon()
    {
        var weapons = GetWeapons();
        var currWeaponIndex = EquippedWeaponIndex;

        // Get the next weapon in the array, if it's the end of the array, go tho the beginning
        if (currWeaponIndex + 1 == weapons.Length)
        {
            currWeaponIndex = 0;
        }
        else
            currWeaponIndex += 1;

        EquippedWeapon = weapons[currWeaponIndex];
    }

    public void PreviousWeapon()
    {
        var weapons = GetWeapons();
        var currWeaponIndex = EquippedWeaponIndex;

        // Get the previous weapon in the array, if it's not a valid position, go to the end of the array
        if (currWeaponIndex - 1 < 0)
        {
            currWeaponIndex = weapons.Length - 1;
        }
        else
            currWeaponIndex -= 1;

        EquippedWeapon = weapons[currWeaponIndex];
    }
}