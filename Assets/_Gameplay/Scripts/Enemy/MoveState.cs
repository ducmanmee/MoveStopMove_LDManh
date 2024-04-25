using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IState<Enemy>
{
    public void OnEnter(Enemy t)
    {
        t.RandomPosition();
    }

    public void OnExecute(Enemy t)
    {
        t.Moving();
    }

    public void OnExit(Enemy t)
    {

    }
}
