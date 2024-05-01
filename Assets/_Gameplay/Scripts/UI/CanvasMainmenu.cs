using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMainmenu : UICanvas
{
    public void PlayButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasGameplay>();
        GameManager.instance.OnInit();
        Time.timeScale = 1;
    }

    public void SettingsButton()
    {
        UIManager.Instance.OpenUI<CanvasSetting>().SetState(this);
    }
}
