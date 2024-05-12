using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFail : UICanvas
{
    [SerializeField] Text scoreText;

    public void SetBestScore(int score)
    {
        scoreText.text = score.ToString();
    }
    public void MainMenuButton()
    {

        UIManager.ins.CloseAllUI();
        UIManager.ins.OpenUI<CanvasMainmenu>();
        GameManager.ins.RestartPlayer();
    }
}
