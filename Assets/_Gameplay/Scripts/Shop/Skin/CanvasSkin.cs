using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSkin : Singleton<CanvasSkin>
{
    public ScriptableObject[] scriptableObjects;
    public List<Texture2D> iconList;
    public ShopBtn[] btn_shop;
    public Text goldText;
    public Text priceText;
    public List<GameObject> btn;


    public int idSelecting;


    public virtual void OnInit()
    {
        UpdateGoldText();
        InitializeButtons();
    }

    private void InitializeButtons()
    {
        for (int i = 0; i < btn_shop.Length; i++)
        {
            InitializeButton(i);
        }
    }

    private void InitializeButton(int index)
    {
        btn_shop[index].idBtnShop = index + 1;
        btn_shop[index].canvasSkin = this;

        if (index < iconList.Count)
        {
            UnlockItems(index);
            SetIcon(btn_shop[index].iconShop, iconList[index]);
        }
    }

    private void UnlockItems(int index)
    {
        int currentShop = CanvasShopFashion.ins.GetCurrentShop();
        switch (currentShop)
        {
            case 0:
                btn_shop[index].UnLock(DataManager.ins.playerData.status_Pant[index + 1]);
                break;
            case 1:
                btn_shop[index].UnLock(DataManager.ins.playerData.status_Hat[index + 1]);
                break;
            case 2:
                btn_shop[index].UnLock(DataManager.ins.playerData.status_Khien[index + 1]);
                break;
            default:
                btn_shop[index].UnLock(DataManager.ins.playerData.status_SetSkin[index + 1]);
                break;
        }
    }

    public int SelectingID
    {
        get { return idSelecting; }
        set { idSelecting = value; }
    }

    public virtual void Btn_Click()
    {
        CheckSelectItem();
    }

    public void CheckSelectItem()
    {
        for (int i = 0; i < btn_shop.Length; i++)
        {
            if (i == SelectingID - 1)
            {
                btn_shop[i].StatusBtn(true);
            }
            else
            {
                btn_shop[i].StatusBtn(false);
            }
        }
    }

    public void UpdateGoldText()
    {
        goldText.text = DataManager.ins.playerData.gold.ToString();
    }

    public void SetIcon(Image icon, Texture2D iconTexture)
    {
        if (icon != null && iconTexture != null)
        {
            icon.sprite = Sprite.Create(iconTexture, new Rect(0, 0, iconTexture.width, iconTexture.height), Vector2.zero);
            icon.SetNativeSize();
        }
    }

    public void SetStateBtn(int index)
    {
        for (int i = 0; i < btn.Count; i++)
        {
            if (i == index)
            {
                btn[i].SetActive(true);
            }
            else
            {
                btn[i].SetActive(false);
            }
        }
    }

    public ShopBtn GetCurrentShopBtn() => btn_shop[idSelecting - 1]; 

}
