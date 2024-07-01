using UnityEngine;
using UnityEngine.UI;

public class WeaponShop : Singleton<WeaponShop>
{
    [SerializeField] private ScriptableObject[] scriptableObjectsWeapon;
    [SerializeField] private WeaponDisplay weaponDisplay;
    [SerializeField] private Text goldText;
    private int currentIndex;
    private int defaultIndex;

    private void Awake()
    {
        defaultIndex = 0; 
        currentIndex = defaultIndex;
        goldText.text = DataManager.ins.dt.gold.ToString(); 
        weaponDisplay.DisplayWeapon((Weapon)scriptableObjectsWeapon[currentIndex], currentIndex);
    }

    public void ChangeScriptObject(int change)
    {
        currentIndex += change;

        if( currentIndex < 0 )
        {
            currentIndex = scriptableObjectsWeapon.Length - 1;
        }
        else if( currentIndex > scriptableObjectsWeapon.Length - 1)
        {
            currentIndex = 0;
        }

        if(weaponDisplay != null)
        {
            weaponDisplay.DisplayWeapon((Weapon)scriptableObjectsWeapon[currentIndex], currentIndex);

        }
    }

    public void SetWeapon()
    {
        DataManager.ins.dt.idWeapon = currentIndex;
        Player.ins.WeaponToUse = DataManager.ins.dt.idWeapon;
        Player.ins.SetupWeapon();
    }  

    public void OpenShop()
    {
        currentIndex = defaultIndex; 
        if (weaponDisplay != null)
        {
            weaponDisplay.DisplayWeapon((Weapon)scriptableObjectsWeapon[currentIndex], currentIndex);
        }
    }

    public void BuyWeapon()
    {
        Weapon weaponToBuy = scriptableObjectsWeapon[currentIndex] as Weapon;
        if (!DataManager.ins.dt.status_Weapon[currentIndex])
        {
            if(DataManager.ins.dt.gold > weaponToBuy.priceWeapon)
            {
                DataManager.ins.dt.gold -= weaponToBuy.priceWeapon;
                goldText.text = DataManager.ins.dt.gold.ToString();
                DataManager.ins.dt.status_Weapon[currentIndex] = true;
                weaponDisplay.SetStateDisplay(weaponToBuy, currentIndex);
            }
        }  
        else
        {
            if (Player.ins.WeaponToUse != currentIndex)
            {
                SetWeapon();
                weaponDisplay.SetStateDisplay(weaponToBuy, currentIndex);
            }
        }
    }    

}
