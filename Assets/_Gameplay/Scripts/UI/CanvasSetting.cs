using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSetting : UICanvas
{
    [SerializeField] GameObject[] buttons;

    public void SetState(UICanvas canvas)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }

        if (canvas is CanvasMainmenu)
        {
            buttons[2].gameObject.SetActive(true);
        }
        else if (canvas is CanvasGameplay)
        {
            buttons[0].gameObject.SetActive(true);
            buttons[1].gameObject.SetActive(true);
        }
    }

    public void MainMenuButton()
    {
        UIManager.ins.CloseAllUI();
        GameManager.ins.ClearEnemyActive();
        GameManager.ins.RestartPlayer();
        GameManager.ins.ChangeState(new MenuState());
        UIManager.ins.OpenUI<CanvasMainmenu>();
    }

    public void ContinueButton()
    {
        Close(0);
        Time.timeScale = 1;
    }    
}
