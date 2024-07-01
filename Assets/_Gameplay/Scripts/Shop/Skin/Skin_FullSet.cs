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
        if (DataManager.ins.dt.status_SetSkin[SelectingID])
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
        if (DataManager.ins.dt.status_SetSkin[SelectingID])
        {
            if (Player.ins.SetSkinToUse != SelectingID)
            {
                DataManager.ins.dt.idSetSkin = SelectingID;
                Player.ins.SetSkinToUse = DataManager.ins.dt.idSetSkin;
                Player.ins.SetSkin(Player.ins.SetSkinToUse);
                SetStateSkin();
            }
            else
            {
                DataManager.ins.dt.idSetSkin = 0;
                Player.ins.SetSkinToUse = 0;
                Player.ins.SetSkin(0);
                SetStateSkin();
            }    
        }
        else
        {
            if (DataManager.ins.dt.gold > int.Parse(priceText.text))
            {
                GetCurrentShopBtn().UnLock(true);
                DataManager.ins.dt.gold -= int.Parse(priceText.text);
                UpdateGoldText();
                DataManager.ins.dt.status_SetSkin[SelectingID] = true;
                SetStateSkin();
            }
        }
    }
}
