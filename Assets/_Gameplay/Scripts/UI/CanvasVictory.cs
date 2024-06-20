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
        UIManager.ins.CloseAllUI();
        GameManager.ins.RestartToAgain();
        UIManager.ins.OpenUI<CanvasMainmenu>();
        GameManager.ins.ChangeState(new MenuState());
    }

    public void NextLevelButton()
    {
        UIManager.ins.CloseAllUI();
        LevelManager.ins.NextLevel();
        Player.ins.OnInit();
        GameManager.ins.PlayGame();
    }
}
