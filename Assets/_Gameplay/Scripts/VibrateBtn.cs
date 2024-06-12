using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrateBtn : MonoBehaviour
{
    public GameObject vibrateBtn;
    public GameObject noVibrateBtn;
    private bool isVibrate;

    private void Start()
    {
        isVibrate = true;
    }

    public void StatusBtn()
    {
        isVibrate = !isVibrate;
        HideBtn(isVibrate);
    }   
    
    public void HideBtn(bool status)
    {
        vibrateBtn.SetActive(status);
        noVibrateBtn.SetActive(!status);
    }    
}
