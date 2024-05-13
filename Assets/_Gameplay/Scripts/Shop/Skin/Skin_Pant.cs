using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin_Pant : CanvasSkin
{
    public static Skin_Pant ins;
    public List<Material> pants;
    public Pant pant;

    private void MakeInstance()
    {
        if (ins == null)
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
        SetStatePant(SelectingID);
    }

    public override void Btn_Click()
    {
        base.Btn_Click();
        Player.ins.SetPant(pants[SelectingID]);
        SetStatePant(SelectingID);
    }

    public void SetStatePant(int index)
    {
        pant = (Pant)scriptableObjects[SelectingID];
        if (DataManager.ins.playerData.status_Pant[index])
        {
            if (Player.ins.PantToUse == SelectingID)
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
            priceText.text = pant.pricePant.ToString();
        }
    }

    public void Buy()
    {
        if (DataManager.ins.playerData.status_Pant[SelectingID])
        {
            if (Player.ins.PantToUse != SelectingID)
            {
                DataManager.ins.playerData.idPant = SelectingID;
                Player.ins.PantToUse = DataManager.ins.playerData.idPant;
                SetStatePant(SelectingID);
            }
            else return;
        }
        else
        {
            if (DataManager.ins.playerData.gold > int.Parse(priceText.text))
            {
                DataManager.ins.playerData.gold -= int.Parse(priceText.text);
                UpdateGoldText();
                DataManager.ins.playerData.status_Pant[SelectingID] = true;
                SetStatePant(SelectingID);
            }
        }
    }
}
