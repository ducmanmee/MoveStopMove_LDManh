using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeBtn : MonoBehaviour
{
    public GameObject volumeBtn;
    public GameObject noVolumeBtn;
    private bool isVolume;

    private void Start()
    {
        isVolume = true;
    }

    public void StatusBtn()
    {
        isVolume = !isVolume;
        HideBtn(isVolume);
    }

    public void HideBtn(bool status)
    {
        volumeBtn.SetActive(status);
        noVolumeBtn.SetActive(!status);
    }
}
