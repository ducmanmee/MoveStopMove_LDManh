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
        Player.ins.SetHat(SelectingID);
        SetStateHat();
    }

    public override void Btn_Click()
    {
        base.Btn_Click();
        Player.ins.SetHat(SelectingID);
        SetStateHat();
    }

    public void SetStateHat()
    {
        hat = (Hat)scriptableObjects[SelectingID - 1];
        if (DataManager.ins.playerData.status_Hat[SelectingID])
        {
            if (Player.ins.HatToUse == SelectingID)
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
            priceText.text = hat.price.ToString();
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
                Player.ins.SetHat(Player.ins.HatToUse);
                SetStateHat();
            }
            else
            {
                DataManager.ins.playerData.idHat = 0;
                Player.ins.HatToUse = 0;
                Player.ins.SetHat(0);
                SetStateHat();
            }
        }
        else
        {
            if (DataManager.ins.playerData.gold > int.Parse(priceText.text))
            {
                GetCurrentShopBtn().UnLock();
                DataManager.ins.playerData.gold -= int.Parse(priceText.text);
                UpdateGoldText();
                DataManager.ins.playerData.status_Hat[SelectingID] = true;
                SetStateHat();
            }
        }
    }

}