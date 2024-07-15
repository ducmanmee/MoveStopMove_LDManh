using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrateBtn : MonoBehaviour
{
    public GameObject vibrateBtn;
    public GameObject noVibrateBtn;

    public void StatusBtn()
    {
        DataManager.ins.dt.isVibrate = !DataManager.ins.dt.isVibrate;
        HideBtn(DataManager.ins.dt.isVibrate);
    }   
    
    public void HideBtn(bool status)
    {
        vibrateBtn.SetActive(status);
        noVibrateBtn.SetActive(!status);
    }    
}
