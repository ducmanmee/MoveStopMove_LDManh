using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : IState<Enemy>
{
    public void OnEnter(Enemy t)
    {
        t.ChangeAnim(Constain.ANIM_DEAD);
        t.OnDespawn();
    }

    public void OnExecute(Enemy t)
    {

    }

    public void OnExit(Enemy t)
    {
        
    }
}
