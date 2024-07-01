using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class VibrationManager : MonoBehaviour
{
    public static VibrationManager ins;

    private void Awake()
    {
        ins = this;
    }

    public void SelectionButton()
    {
        if(DataManager.ins.dt.isVibrate)
        MMVibrationManager.Haptic(HapticTypes.Selection, false, true, this);
    }

    public void SuccessButton()
    {
        if(DataManager.ins.dt.isVibrate)
        MMVibrationManager.Haptic(HapticTypes.Success, false, true, this);
    }

    public void WarningButton()
    {
        if(DataManager.ins.dt.isVibrate)
        MMVibrationManager.Haptic(HapticTypes.Warning, false, true, this);
    }

    public void FailureButton()
    {
        if(DataManager.ins.dt.isVibrate)
        MMVibrationManager.Haptic(HapticTypes.Failure, false, true, this);
    }

    public void RigidButton()
    {
        if(DataManager.ins.dt.isVibrate)
        MMVibrationManager.Haptic(HapticTypes.RigidImpact, false, true, this);
    }

    public void SoftButton()
    {
        if(DataManager.ins.dt.isVibrate)
        MMVibrationManager.Haptic(HapticTypes.SoftImpact, false, true, this);
    }

    public void LightButton()
    {
        if(DataManager.ins.dt.isVibrate)
        MMVibrationManager.Haptic(HapticTypes.LightImpact, false, true, this);
    }

    public void MediumButton()
    {
        if(DataManager.ins.dt.isVibrate)
        MMVibrationManager.Haptic(HapticTypes.MediumImpact, false, true, this);
    }

    public void HeavyButton()
    {
        if(DataManager.ins.dt.isVibrate)
        MMVibrationManager.Haptic(HapticTypes.HeavyImpact, false, true, this);
    }
}
