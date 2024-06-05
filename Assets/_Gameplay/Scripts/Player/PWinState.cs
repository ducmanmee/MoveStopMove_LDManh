using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PWinState : IState<Player>
{
    public void OnEnter(Player t)
    {
        t.ChangeAnim(Constain.ANIM_DANCEWIN);
    }

    public void OnExecute(Player t)
    {
        
    }

    public void OnExit(Player t)
    {
        
    }
}
