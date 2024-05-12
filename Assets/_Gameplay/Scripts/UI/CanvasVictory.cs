using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasVictory : UICanvas
{
    [SerializeField] Text scoreText;

    public void SetBestScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void MainMenuButton()
    {
        Close(0);
        UIManager.ins.OpenUI<CanvasMainmenu>();
    }

    public void NextLevelButton()
    {
        Close(0);
        LevelManager.ins.NextLevel();
        GameManager.ins.OnInit();
    }
}
