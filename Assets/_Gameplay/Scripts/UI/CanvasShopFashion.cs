using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasShopFashion : UICanvas
{
    public static CanvasShopFashion ins;
    [SerializeField] private List<GameObject> shops;
    [SerializeField] private List<GameObject> checkChooseShops;

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
        CanvasMainmenu canvasMainmenu = UIManager.ins.OpenUI<CanvasMainmenu>();
        canvasMainmenu.MoveAgainBtn();
        canvasMainmenu.UpdateGoldText(DataManager.ins.dt.gold);
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
                CheckChooseShop(0);
                Player.ins.SetHat(Player.ins.HatToUse);
                Player.ins.SetKhien(Player.ins.KhienToUse);
                Player.ins.SetSkin(0);
                break;
            case 1:
                Skin_Hat.ins.OnInit();
                CheckChooseShop(1);
                Player.ins.SetPant(Player.ins.PantToUse);
                Player.ins.SetKhien(Player.ins.KhienToUse);
                Player.ins.SetSkin(0);
                break;
            case 2:
                Skin_Khien.ins.OnInit();
                CheckChooseShop(2);
                Player.ins.SetPant(Player.ins.PantToUse);
                Player.ins.SetHat(Player.ins.HatToUse);
                Player.ins.SetSkin(0);
                break;
            default:
                Skin_FullSet.ins.OnInit();
                CheckChooseShop(3);
                Player.ins.SetSkin(1);
                break;
        }
    }

    public int GetCurrentShop() => currentShop;
    public void CheckChooseShop(int index)
    {
        for(int i=0; i < checkChooseShops.Count; i++)
        {
            if(i == index)
            {
                checkChooseShops[i].SetActive(true);
            }    
            else
            {
                checkChooseShops[i].SetActive(false);
            }
        }    
    }    
}
