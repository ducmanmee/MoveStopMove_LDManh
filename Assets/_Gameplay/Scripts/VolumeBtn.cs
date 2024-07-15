using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeBtn : MonoBehaviour
{
    public GameObject volumeBtn;
    public GameObject noVolumeBtn;

    public void StatusBtn()
    {
        DataManager.ins.dt.isSound = !DataManager.ins.dt.isSound;
        HideBtn(DataManager.ins.dt.isSound);
    }

    public void HideBtn(bool status)
    {
        volumeBtn.SetActive(status);
        noVolumeBtn.SetActive(!status);
    }
}
