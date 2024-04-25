using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIdleState : IState<Player>
{
    public void OnEnter(Player t)
    {
        t.ChangeAnim(Constain.ANIM_IDLE);
    }

    public void OnExecute(Player t)
    {
        
    }

    public void OnExit(Player t)
    {
        
    }
}
