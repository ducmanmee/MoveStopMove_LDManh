using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGameplay : UICanvas
{
    public static CanvasGameplay ins;
    [SerializeField] Text coinText;
    [SerializeField] List<KillNotifyManager> notifyList; 

    private void MakeInstance()
    {
        if(ins == null)
        {
            ins = this;
        }    
    }    

    public override void Setup()
    {
        MakeInstance();
        base.Setup();
    }


    public void UpdateCharacterAlive()
    {
        coinText.text = GameManager.ins.GetCharacterAlive().ToString();
    }
    public void SettingsButton()
    {
        UIManager.ins.OpenUI<CanvasSetting>().SetState(this);
    }
}
