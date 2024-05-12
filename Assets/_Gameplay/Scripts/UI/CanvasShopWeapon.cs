using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasShopWeapon : UICanvas
{
    public void MainmenuBtn()
    {
        Close(0);
        GameManager.ins.HideShopWeaponCamera(false);
        GameManager.ins.ChangeState(new MenuState());
        WeaponDisplay.ins.ResetWeaponInShop();
        UIManager.ins.OpenUI<CanvasMainmenu>();
        UIManager.ins.OpenUI<CanvasMainmenu>().UpdateGoldText(DataManager.ins.playerData.gold);
    }    

}
