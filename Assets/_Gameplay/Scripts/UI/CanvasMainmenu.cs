using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMainmenu : UICanvas
{
    public void PlayButton()
    {
        Close(0);
        GameManager.instance.ChangeState(new PlayState());
        UIManager.Instance.OpenUI<CanvasGameplay>();
        Time.timeScale = 1;
        Player.Instance.OnInit();
        GameManager.instance.OnInit();
    }

    public void SettingsButton()
    {
        UIManager.Instance.OpenUI<CanvasSetting>().SetState(this);
    }
}
