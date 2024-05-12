using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMainmenu : UICanvas
{

    [SerializeField] Text goldText;

    private void Start()
    {
        UpdateGoldText(DataManager.ins.playerData.gold);
    }

    public void UpdateGoldText(int gold)
    {
        goldText.text = gold.ToString();
    }    

    public void PlayButton()
    {
        Close(0);
        GameManager.ins.PlayGame();
    }

    public void SettingsButton()
    {
        UIManager.ins.OpenUI<CanvasSetting>().SetState(this);
    }

    public void ShopWeaponBtn()
    {
        Close(0);
        Time.timeScale = 1;
        GameManager.ins.HideShopWeaponCamera(true);
        GameManager.ins.ChangeState(new ShopState());
        UIManager.ins.OpenUI<CanvasShopWeapon>();
        WeaponDisplay.ins.ResetWeaponInShop();
    }  
    
    public void ShopFashionBtn()
    {
        Close(0);
        GameManager.ins.HideShopFashionCamera(true);
        Player.ins.ChangeAnim(Constain.ANIM_DANCE);
        UIManager.ins.OpenUI<CanvasShopFashion>();
        CanvasShopFashion.ins.SetStateShop(0);
    }   
}
