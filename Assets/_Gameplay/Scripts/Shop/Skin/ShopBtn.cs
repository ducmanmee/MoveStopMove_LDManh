using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class ShopBtn : MonoBehaviour
{
    public int idBtnShop;
    public GameObject lockSkin;
    public Image iconShop;
    public GameObject selectBtn;

    public CanvasSkin canvasSkin;

    public void Click()
    {
        if(CanvasShopFashion.ins.GetCurrentShop() == 0)
        {
            Skin_Pant.ins.SelectingID = idBtnShop;
            Skin_Pant.ins.Btn_Click(); 
        }   
        else if (CanvasShopFashion.ins.GetCurrentShop() == 1)
        {
            Skin_Hat.ins.SelectingID = idBtnShop;
            Skin_Hat.ins.Btn_Click();
        }
        else if (CanvasShopFashion.ins.GetCurrentShop() == 2)
        {
            Skin_Khien.ins.SelectingID = idBtnShop;
            Skin_Khien.ins.Btn_Click();
        }
        else
        {
            Skin_FullSet.ins.SelectingID = idBtnShop;
            Skin_FullSet.ins.Btn_Click();
        }    
    }    

    public void UnLock(bool isLock)
    {
        if(isLock)
        {
            lockSkin.SetActive(false);
        }    
    }
    
    public void StatusBtn(bool isActive)
    {
        selectBtn.SetActive(isActive);
    }    

}
