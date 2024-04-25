using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAttackState : IState<Player>
{
    float timer;
    public void OnEnter(Player t)
    {
        t.Attack();
        t.canAttack = false;
    }

    public void OnExecute(Player t)
    {
        timer += Time.deltaTime;
        if(timer > 1f)
        {
            t.ChangeState(new PIdleState());
        }
    }

    public void OnExit(Player t)
    {

    }
}
