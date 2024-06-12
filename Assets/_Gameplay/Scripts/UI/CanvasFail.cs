using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFail : UICanvas
{
    public static CanvasFail ins;
    [SerializeField] Text scoreText;
    [SerializeField] Text nameKillerText;
    [SerializeField] Text rankText;

    private void MakeInstance()
    {
        if(ins  == null)
        {
            ins = this;
        }    
    }

    private void Awake()
    {
        MakeInstance();
    }

    public void SetBestScore(int score)
    {
        scoreText.text = score.ToString();
    }
    public void MainMenuButton()
    {
        UIManager.ins.CloseAllUI();
        GameManager.ins.ClearEnemyActive();
        UIManager.ins.OpenUI<CanvasMainmenu>();
        GameManager.ins.ChangeState(new MenuState());
        GameManager.ins.RestartPlayer();
    }

    public void SetNameKiller(string killer)
    {
        nameKillerText.text = killer;
    }    
}
