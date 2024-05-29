using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin_FullSet : CanvasSkin
{
    public static Skin_FullSet ins;
    public FullSet fullset;

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
        Player.ins.SetSkin(SelectingID);
        Player.ins.SetupWeapon();
        SetStateSkin();
    }

    public override void Btn_Click()
    {
        base.Btn_Click();
        Player.ins.SetSkin(SelectingID);
        Player.ins.SetupWeapon();
        SetStateSkin();
    }

    public void SetStateSkin()
    {
        fullset = (FullSet)scriptableObjects[SelectingID - 1];
        if (DataManager.ins.playerData.status_SetSkin[SelectingID])
        {
            if (Player.ins.SetSkinToUse == SelectingID)
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
            priceText.text = fullset.price.ToString();
        }
    }

    public void Buy()
    {
        if (DataManager.ins.playerData.status_SetSkin[SelectingID])
        {
            if (Player.ins.SetSkinToUse != SelectingID)
            {
                DataManager.ins.playerData.idSetSkin = SelectingID;
                Player.ins.SetSkinToUse = DataManager.ins.playerData.idSetSkin;
                Player.ins.SetSkin(Player.ins.SetSkinToUse);
                SetStateSkin();
            }
            else
            {
                DataManager.ins.playerData.idSetSkin = 0;
                Player.ins.SetSkinToUse = 0;
                Player.ins.SetSkin(0);
                SetStateSkin();
            }    
        }
        else
        {
            if (DataManager.ins.playerData.gold > int.Parse(priceText.text))
            {
                GetCurrentShopBtn().UnLock();
                DataManager.ins.playerData.gold -= int.Parse(priceText.text);
                UpdateGoldText();
                DataManager.ins.playerData.status_SetSkin[SelectingID] = true;
                SetStateSkin();
            }
        }
    }
}
