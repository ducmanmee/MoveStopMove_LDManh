using UnityEngine;

public class ScriptableObjectChanger : MonoBehaviour
{
    [SerializeField] private ScriptableObject[] scriptableObjects;
    [SerializeField] private WeaponDisplay weaponDisplay;
    private int currentIndex;

    private void Awake()
    {
        weaponDisplay.DisplayWeapon((Weapon)scriptableObjects[0]);
    }

    public void ChangeScriptObject(int change)
    {
        currentIndex += change;

        if( currentIndex < 0 )
        {
            currentIndex = scriptableObjects.Length - 1;
        }
        else if( currentIndex > scriptableObjects.Length - 1)
        {
            currentIndex = 0;
        }

        if(weaponDisplay != null)
        {
            weaponDisplay.DisplayWeapon((Weapon)scriptableObjects[currentIndex]);

        }
    }

}
