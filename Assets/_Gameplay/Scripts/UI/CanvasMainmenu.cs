using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMainmenu : UICanvas
{

    [SerializeField] Text goldText;
    [SerializeField] TMP_InputField namePlayer;

    [Header("Move Button")]
    [SerializeField] List<GameObject> leftBtnMove;
    [SerializeField] List<GameObject> rightBtnMove;
    [SerializeField] List<float> listLeftStartPosX = new List<float>();
    [SerializeField] List<float> listRightStartPosX = new List<float>();
    [SerializeField] GameObject nameInput;
    public float leftBtnX;
    public float rightBtnX;

    private void Start()
    {
        for(int i = 0; i < leftBtnMove.Count; i++)
        {
            listLeftStartPosX.Add(leftBtnMove[i].transform.localPosition.x);
        }
        
        for(int i = 0; i < rightBtnMove.Count; i++)
        {
            listRightStartPosX.Add(rightBtnMove[i].transform.localPosition.x);
        }
        UpdateGoldText(DataManager.ins.dt.gold);
        UpdateName(DataManager.ins.dt.namePlayer);
    }

    public void UpdateName(string name)
    {
        namePlayer.text = name;
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
        MoveBtn();
        //Close(0);
        Time.timeScale = 1;
        StartCoroutine(OpenShopWeapon());
        
    }  

    IEnumerator OpenShopSkin()
    {
        yield return new WaitForSeconds(.6f);
        GameManager.ins.HideShopFashionCamera(true);
        Player.ins.ChangeAnim(Constain.ANIM_DANCE);
        UIManager.ins.OpenUI<CanvasShopFashion>();
        CanvasShopFashion.ins.SetStateShop(0);
        Skin_Pant.ins.OnInit();
    }  
    
    IEnumerator OpenShopWeapon()
    {
        yield return new WaitForSeconds(.6f);
        GameManager.ins.HideShopWeaponCamera(true);
        GameManager.ins.ChangeState(new ShopState());
        UIManager.ins.OpenUI<CanvasShopWeapon>();
        WeaponDisplay.ins.ResetWeaponInShop();
    }

    public void ShopFashionBtn()
    {
        MoveBtn();
        //Close(0);
        StartCoroutine(OpenShopSkin());
        
    }   

    public void MoveBtn()
    {
        nameInput.SetActive(false);
        for (int i = 0; i < leftBtnMove.Count; i++)
        {
            leftBtnMove[i].transform.DOLocalMove(new Vector3(leftBtnX, leftBtnMove[i].transform.localPosition.y, leftBtnMove[i].transform.localPosition.z), 1f, false);
        }
        
        for (int i = 0; i < rightBtnMove.Count; i++)
        {
            rightBtnMove[i].transform.DOLocalMove(new Vector3(rightBtnX, rightBtnMove[i].transform.localPosition.y, rightBtnMove[i].transform.localPosition.z), 1f, false);
        }
    } 
    
    public void MoveAgainBtn()
    {
        nameInput.SetActive(true);
        for (int i = 0; i < leftBtnMove.Count; i++)
        {
            leftBtnMove[i].transform.DOLocalMove(new Vector3(listLeftStartPosX[i], leftBtnMove[i].transform.localPosition.y, leftBtnMove[i].transform.localPosition.z), 1f, false);
        }

        for (int i = 0; i < rightBtnMove.Count; i++)
        {
            rightBtnMove[i].transform.DOLocalMove(new Vector3(listRightStartPosX[i], rightBtnMove[i].transform.localPosition.y, rightBtnMove[i].transform.localPosition.z), 1f, false);
        }
    }    
}
