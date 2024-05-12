using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            if (i == index)
            {
                shops[i].SetActive(true); 
            }
            else
            {
                shops[i].SetActive(false); 
            }
        }
    }

    public int GetCurrentShop() => currentShop;

}
