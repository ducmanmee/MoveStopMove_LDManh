using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Enemy>
{
    float timer;
    public void OnEnter(Enemy t)
    {
        t.ChangeAnim(Constain.ANIM_IDLE);
        t.NearestEnemy();
    }

    public void OnExecute(Enemy t)
    {
        timer += Time.deltaTime;
        if (t.nearestCharacter != null)
        {
            t.ChangeState(new AttackState());
        }
        else
        {
            if(timer > 1f)
            {
                t.ChangeState(new MoveState());
            }
        }  
    }

    public void OnExit(Enemy t)
    {
        
    }
}
