using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class ShopBtn : MonoBehaviour
{
    public int idBtnShop;
    public GameObject lockPant;
    public Image iconHat;

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
    }    

}
