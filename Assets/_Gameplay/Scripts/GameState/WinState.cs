using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : IState<GameManager>
{
    public void OnEnter(GameManager t)
    {
        t.StartCoroutine(t.WinGame());
    }

    public void OnExecute(GameManager t)
    {
        
    }

    public void OnExit(GameManager t)
    {
        
    }
}
