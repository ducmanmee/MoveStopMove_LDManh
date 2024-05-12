using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseState : IState<GameManager>
{
    public void OnEnter(GameManager t)
    {
        t.StartCoroutine(t.LoseGame());
    }

    public void OnExecute(GameManager t)
    {

    }

    public void OnExit(GameManager t)
    {

    }
}
