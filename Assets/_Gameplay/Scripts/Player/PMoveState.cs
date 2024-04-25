using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMoveState : IState<Player>
{
    public void OnEnter(Player t)
    {
        t.transform.rotation = Quaternion.LookRotation(t.movementDirection);
        t.ChangeAnim(Constain.ANIM_RUN);
        t.canAttack = true;
    }

    public void OnExecute(Player t)
    {
        t.transform.Translate(t.movementDirection * t.moveSpeed * Time.deltaTime, Space.World);
    }

    public void OnExit(Player t)
    {

    }
}
