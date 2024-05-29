using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasShopFashion : UICanvas
{
    public static CanvasShopFashion ins;
    [SerializeField] private List<GameObject> shops;

    private int currentShop;

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

    public void MainmenuBtn()
    {
        Close(0);
        GameManager.ins.HideShopFashionCamera(false);
        Player.ins.OnInit();
        UIManager.ins.OpenUI<CanvasMainmenu>();
        UIManager.ins.OpenUI<CanvasMainmenu>().UpdateGoldText(DataManager.ins.playerData.gold);
    }

    public void SetStateShop(int index)
    {
        currentShop = index;
        for (int i = 0; i < shops.Count; i++)
        {
            shops[i].SetActive(i == index);
        }

        switch (currentShop)
        {
            case 0:
                Skin_Pant.ins.OnInit();
                Player.ins.SetHat(Player.ins.HatToUse);
                Player.ins.SetKhien(Player.ins.KhienToUse);
                Player.ins.SetSkin(0);
                break;
            case 1:
                Skin_Hat.ins.OnInit();
                Player.ins.SetPant(Player.ins.PantToUse);
                Player.ins.SetKhien(Player.ins.KhienToUse);
                Player.ins.SetSkin(0);
                break;
            case 2:
                Skin_Khien.ins.OnInit();
                Player.ins.SetPant(Player.ins.PantToUse);
                Player.ins.SetHat(Player.ins.HatToUse);
                Player.ins.SetSkin(0);
                break;
            default:
                Skin_FullSet.ins.OnInit();
                Player.ins.SetSkin(1);
                break;
        }
    }

    public int GetCurrentShop() => currentShop;

}
