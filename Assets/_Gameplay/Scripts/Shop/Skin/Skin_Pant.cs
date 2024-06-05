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
        SelectingID = 1;
        CheckSelectItem();
        Player.ins.SetPant(SelectingID);
        SetStatePant();
    }

    public override void Btn_Click()
    {
        base.Btn_Click();
        Player.ins.SetPant(SelectingID);
        SetStatePant();
    }

    public void SetStatePant()
    {
        pant = (Pant)scriptableObjects[SelectingID - 1];
        if (DataManager.ins.playerData.status_Pant[SelectingID])
        {
            if (Player.ins.PantToUse == SelectingID)
            {
                SetStateBtn(2);
            }
            else
            {
                SetStateBtn(1);
            }
        }
        else
        {
            SetStateBtn(0);
            priceText.text = pant.price.ToString();
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
                Player.ins.SetPant(Player.ins.PantToUse);
                SetStatePant();
            }
            else
            {
                DataManager.ins.playerData.idPant = 0;
                Player.ins.PantToUse = 0;
                Player.ins.SetPant(0);
                SetStatePant();
            }
        }
        else
        {

            if (DataManager.ins.playerData.gold > int.Parse(priceText.text))
            {
                GetCurrentShopBtn().UnLock(true);
                DataManager.ins.playerData.gold -= int.Parse(priceText.text);
                UpdateGoldText();
                DataManager.ins.playerData.status_Pant[SelectingID] = true;
                SetStatePant();
            }
        }
    }
}