using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasRevival : UICanvas
{
    public static CanvasRevival ins;

    private void MakeInstance()
    {
        if(ins == null)
        {
            ins = this;
        }
    }

    private void Awake()
    {
        MakeInstance();
    }

    private void Start()
    {
        
    }
    public void Revive()
    {
        Close(0);
        CountdownTimer.ins.ResetCountdown();
        GameManager.ins.ChangeState(new PlayState());
        UIManager.ins.OpenUI<CanvasGameplay>();
        GameManager.ins.ResetPlayer();
        Player.ins.ActiveName();
    }  
    
    public void DontRevive()
    {
        Close(0);
        CountdownTimer.ins.ResetCountdown();
        UIManager.ins.OpenUI<CanvasFail>();
        CanvasFail.ins.SetNameKiller(Player.ins.NameOfKiller);
        CanvasFail.ins.SetRank(Player.ins.RankPlayer);
    }    
}
