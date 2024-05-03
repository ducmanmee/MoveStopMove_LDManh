using UnityEngine;
using UnityEngine.UI;

public class WeaponDisplay : MonoBehaviour
{
    [SerializeField] private Text weaponName;
    [SerializeField] private int weaponDamage;
    public void DisplayWeapon(Weapon weapon)
    {
        weaponName.text = weapon.weaponName;
        weaponDamage = weapon.damage;
    }
}
