using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin_Khien : CanvasSkin
{
    public static Skin_Khien ins;
    public Khien khien;

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
        Player.ins.SetKhien(SelectingID);
        SetStatekhien();
    }

    public override void Btn_Click()
    {
        base.Btn_Click();
        Player.ins.SetKhien(SelectingID);
        SetStatekhien();
    }

    public void SetStatekhien()
    {
        khien = (Khien)scriptableObjects[SelectingID - 1];
        if (DataManager.ins.playerData.status_Khien[SelectingID])
        { 
            if (Player.ins.KhienToUse == SelectingID)
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
            priceText.text = khien.price.ToString();    
        }    
    }

    public void Buy()
    {
        if (DataManager.ins.playerData.status_Khien[SelectingID])
        {
            if (Player.ins.KhienToUse != SelectingID)
            {
                DataManager.ins.playerData.idKhien = SelectingID;
                Player.ins.KhienToUse = DataManager.ins.playerData.idKhien;
                Player.ins.SetKhien(Player.ins.KhienToUse);
                SetStatekhien();
            }
            else
            {
                DataManager.ins.playerData.idKhien = 0;
                Player.ins.KhienToUse = 0;
                Player.ins.SetKhien(0);
                SetStatekhien();
            }    
        }
        else
        {
            if (DataManager.ins.playerData.gold > int.Parse(priceText.text))
            {
                GetCurrentShopBtn().UnLock();
                DataManager.ins.playerData.gold -= int.Parse(priceText.text);
                UpdateGoldText();
                DataManager.ins.playerData.status_Khien[SelectingID] = true;
                SetStatekhien();
            }
        }
    }

}
