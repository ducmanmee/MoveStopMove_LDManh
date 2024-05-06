using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMainmenu : UICanvas
{
    public void PlayButton()
    {
        Close(0);
        GameManager.ins.PlayGame();
    }

    public void SettingsButton()
    {
        UIManager.Instance.OpenUI<CanvasSetting>().SetState(this);
    }
}
