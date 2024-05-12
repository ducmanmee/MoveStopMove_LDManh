using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBtn : MonoBehaviour
{
    public int idBtnShop;
    [SerializeField] private GameObject lockPant;


    public void SetPaintBtn()
    {
        if(CanvasShopFashion.ins.GetCurrentShop() == 0)
        {
            PantDisplay.ins.DisplayPant(idBtnShop);
            PantDisplay.ins.SetCurrentPant(idBtnShop);
        }    
        else if(CanvasShopFashion.ins.GetCurrentShop() == 1)
        {
            HatDisplay.ins.DisplayHat(idBtnShop);
            HatDisplay.ins.SetCurrentHat(idBtnShop);
        }
    }  
}
