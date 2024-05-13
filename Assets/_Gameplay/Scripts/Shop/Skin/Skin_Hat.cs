using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin_Hat : CanvasSkin
{
    public static Skin_Hat ins;
    public List<GameObject> hats;
    public Hat hat;

    private void MakeInstance()
    {
        if(ins == null)
        {
            ins = this;
        }    
    }

    private void Awake()
    {
        MakeInstance();
    }

    private void Start()
    {
        OnInit();
    }

    public override void OnInit()
    {
        base.OnInit();
        SelectingID = 0;
        SetStateHat(SelectingID);
    }

    public override void Btn_Click()
    {
        base.Btn_Click();
        Player.ins.SetHat(SelectingID);
        SetStateHat(SelectingID);
    }  

    public void SetStateHat(int index)
    {
        hat = (Hat)scriptableObjects[SelectingID];
        if (DataManager.ins.playerData.status_Hat[index])
        {
            if(Player.ins.HatToUse == SelectingID)
            {
                priceText.text = "Equip";
            }
            else
            {
                priceText.text = "Select";
            }
        } 
        else
        {
            priceText.text = hat.priceHat.ToString();
        }    
    }    

    public void Buy()
    {
        if (DataManager.ins.playerData.status_Hat[SelectingID])
        {
            if (Player.ins.HatToUse != SelectingID)
            {
                DataManager.ins.playerData.idHat = SelectingID;
                Player.ins.HatToUse = DataManager.ins.playerData.idHat;
                SetStateHat(SelectingID);
            }
            else return;
        }
        else
        {
            if(DataManager.ins.playerData.gold > int.Parse(priceText.text))
            {
                DataManager.ins.playerData.gold -= int.Parse(priceText.text);
                UpdateGoldText();
                DataManager.ins.playerData.status_Hat[SelectingID] = true;
                SetStateHat(SelectingID);
            }
        }
    }    

}
