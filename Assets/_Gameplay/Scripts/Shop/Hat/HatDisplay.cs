using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HatDisplay : Singleton<HatDisplay>
{
    [SerializeField] private ScriptableObject[] scriptableObjectsHat;
    [SerializeField] private Text priceHat;

    [SerializeField] private Text goldText;
    private int defaultIndex;
    public int currentIndex;


    private void Awake()
    {
        goldText.text = DataManager.ins.playerData.gold.ToString();
        DisplayHat(defaultIndex);
    }

    public void DisplayHat(int index)
    {
        if (index > scriptableObjectsHat.Length - 1) return;
        SetStateHat((Hat)scriptableObjectsHat[index], index);
        SetHatInShop(index);
    }

    public void SetHatInShop(int index)
    {
        Player.ins.SetHat(index);
    }
    public void SetStateHat(Hat hat, int index)
    {
        if (DataManager.ins.playerData.status_Hat[index])
        {
            if (Player.ins.HatToUse == index)
            {
                priceHat.text = "Equip";
            }
            else
            {
                priceHat.text = "Select";
            }
        }
        else
        {
            priceHat.text = hat.priceHat.ToString();
        }
    }

    public void BuyHat()
    {
        Hat hatToBuy = scriptableObjectsHat[currentIndex] as Hat;
        if (!DataManager.ins.playerData.status_Hat[currentIndex])
        {
            if (DataManager.ins.playerData.gold > hatToBuy.priceHat)
            {
                DataManager.ins.playerData.gold -= hatToBuy.priceHat;
                goldText.text = DataManager.ins.playerData.gold.ToString();
                DataManager.ins.playerData.status_Hat[currentIndex] = true;
                SetStateHat(hatToBuy, currentIndex);
            }
        }
        else
        {
            Debug.Log(currentIndex);
            Debug.Log(DataManager.ins.playerData.idHat);
            if (DataManager.ins.playerData.idHat != currentIndex)
            {
                DataManager.ins.playerData.idHat = currentIndex;
                Player.ins.HatToUse = DataManager.ins.playerData.idHat;
                SetHatInShop(currentIndex);
                SetStateHat(hatToBuy, currentIndex);
            }
        }
    }

    public void SetCurrentHat   (int index)
    {
        currentIndex = index;
    }
}
