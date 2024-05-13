using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSkin : MonoBehaviour
{
    public ScriptableObject[] scriptableObjects;
    public List<Texture2D> iconList;
    public ShopBtn[] btn_shop;
    public Text goldText;
    public Text priceText;

    public int idSelecting;


    public virtual void OnInit()
    {
        UpdateGoldText();
        for (int i = 0; i < btn_shop.Length; i++)
        {
            btn_shop[i].idBtnShop = i;
            btn_shop[i].canvasSkin = this;
            if(i < iconList.Count)
            {
                SetIcon(btn_shop[i].iconHat, iconList[i]);
            }    
        }
    }
    
    public int SelectingID
    {
        get { return idSelecting; }
        set { idSelecting = value; }
    }

    public virtual void Btn_Click()
    {

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

}
