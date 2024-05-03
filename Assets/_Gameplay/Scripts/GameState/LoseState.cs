using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseState : IState<GameManager>
{
    public void OnEnter(GameManager t)
    {
        UIManager.Instance.OpenUI<CanvasFail>();
        Player.Instance.OnInit();
        GameManager.instance.ClearEnemyActive();
    }

    public void OnExecute(GameManager t)
    {

    }

    public void OnExit(GameManager t)
    {

    }
}
