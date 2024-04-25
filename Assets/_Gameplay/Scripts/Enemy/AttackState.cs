using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using UnityEngine;

public class AttackState : IState<Enemy>
{
    float timer;
    public void OnEnter(Enemy t)
    {
        if(t.nearestCharacter != null)
        {
            t.Attack();
        }    
    }

    public void OnExecute(Enemy t)
    {
        timer += Time.deltaTime;
        if(timer > 2f)
        {
            t.ChangeState(new IdleState());
        }
    }

    public void OnExit(Enemy t)
    {

    }
}
