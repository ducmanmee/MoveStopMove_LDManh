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
        GameManager.ins.ClearEnemyActive();
        UIManager.ins.OpenUI<CanvasMainmenu>();
        GameManager.ins.ChangeState(new MenuState());
        GameManager.ins.RestartPlayer();
    }

    public void NextLevelButton()
    {
        Close(0);
        LevelManager.ins.NextLevel();
        GameManager.ins.OnInit();
    }
}
