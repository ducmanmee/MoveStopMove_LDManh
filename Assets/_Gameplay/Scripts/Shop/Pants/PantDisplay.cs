using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PantDisplay : Singleton<PantDisplay>
{
    [SerializeField] private ScriptableObject[] scriptableObjectsPant;
    [SerializeField] private Text pantName;
    [SerializeField] private int pantDamage;
    [SerializeField] private Text pricePant;

    [SerializeField] private Text goldText;

    public int currentIndex;
    private int defaultIndex;

    private void Awake()
    {
        goldText.text = DataManager.ins.playerData.gold.ToString();
        DisplayPant(defaultIndex);
    }

    public void DisplayPant(int index)
    {
        if (index > scriptableObjectsPant.Length - 1) return;
        SetStatePant((Pant)scriptableObjectsPant[index], index);
        SetPantInShop((Pant)scriptableObjectsPant[index], index);
    }

    public void SetPantInShop(Pant pant, int index)
    {
        Player.ins.SetPant(pant.pantMaterial);
    }    

    public void SetStatePant(Pant pant, int index)
    {
        if (DataManager.ins.playerData.status_Pant[index])
        {
            if (Player.ins.PantToUse == index)
            {
                pricePant.text = "Equip";
            }
            else
            {
                pricePant.text = "Select";
            }
        }
        else
        {
            pricePant.text = pant.pricePant.ToString();
        }
    }

    public void BuyPant()
    {
        Pant pantToBuy = scriptableObjectsPant[currentIndex] as Pant;
        if (!DataManager.ins.playerData.status_Pant[currentIndex])
        {
            if (DataManager.ins.playerData.gold > pantToBuy.pricePant)
            {
                DataManager.ins.playerData.gold -= pantToBuy.pricePant;
                goldText.text = DataManager.ins.playerData.gold.ToString();
                DataManager.ins.playerData.status_Pant[currentIndex] = true;
                SetStatePant(pantToBuy, currentIndex);
            }
        }
        else
        {
            if (DataManager.ins.playerData.idPant != currentIndex)
            {
                DataManager.ins.playerData.idPant = currentIndex;
                Player.ins.PantToUse = DataManager.ins.playerData.idPant;
                SetPantInShop(pantToBuy, currentIndex);
                SetStatePant(pantToBuy, currentIndex);
            }
        }
    }

    public void SetCurrentPant(int index)
    {
        currentIndex = index;
    }    
}
