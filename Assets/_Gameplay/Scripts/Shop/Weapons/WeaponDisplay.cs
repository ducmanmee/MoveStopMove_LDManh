using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDisplay : Singleton<WeaponDisplay>
{
    [SerializeField] private Text weaponName;
    [SerializeField] private int weaponDamage;
    [SerializeField] private Text priceWeapon;
    [SerializeField] private Image lockWeapon;

    [SerializeField] private Transform weaponHolder;
    private GameObject currentWeaponToDisplay;

    public void DisplayWeapon(Weapon weapon, int index)
    {
        weaponName.text = weapon.weaponName;
        weaponDamage = weapon.damage;
        SetStateDisplay(weapon, index);    


        if(weaponHolder.childCount > 0)
        {
            Destroy(weaponHolder.GetChild(0).gameObject);
        }
        currentWeaponToDisplay = Instantiate(weapon.weaponModel, weaponHolder.position, weaponHolder.rotation, weaponHolder);
    }

    public void ResetWeaponInShop()
    {
        if(GameManager.ins.GetGameState() is ShopState)
        {
            currentWeaponToDisplay.SetActive(true);
            WeaponShop.ins.OpenShop();
        }
        else
        {
            currentWeaponToDisplay.SetActive(false);
        }
    }
    
    public void SetStateDisplay(Weapon weapon, int index)
    {
        if (DataManager.ins.playerData.status_Weapon[index])
        {
            lockWeapon.enabled = false;
            if (Player.ins.WeaponToUse == index)
            {
                priceWeapon.text = "Equip";
            }
            else
            {
                priceWeapon.text = "Select";
            }
        }
        else
        {
            lockWeapon.enabled = true;
            priceWeapon.text = weapon.priceWeapon.ToString();
        }
    }    
}
