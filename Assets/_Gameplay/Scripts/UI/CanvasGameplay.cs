using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGameplay : UICanvas
{
    public static CanvasGameplay ins;
    [SerializeField] Text coinText;
    [SerializeField] List<KillNotifyManager> notifyList;
    [SerializeField] GameObject hand;
    int notifyIndex;

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

    public void ShowNotifyKill(string killer, string victim)
    {
        if(CheckActiveNotify())
        {
            for (int i = notifyList.Count - 1; i > 0; i--)
            {
                notifyList[i].SetNotifyKill(
                    notifyList[i - 1].killer.text,
                    notifyList[i - 1].victim.text
                );
            }

            notifyList[0].SetNotifyKill(killer, victim);
            return;
        }

        notifyList[notifyIndex].SetNotifyKill(killer, victim);
        notifyIndex = (notifyIndex + 1) % notifyList.Count;
    }

    public bool CheckActiveNotify()
    {
        foreach (KillNotifyManager obj in notifyList)
        {
            if (!obj.gameObject.activeSelf)
            {
                return false;
            }
        }
        return true;
    }

    public void ResetTabNotify()
    {
        notifyIndex = 0;
        foreach (KillNotifyManager obj in notifyList)
        {
            obj.gameObject.SetActive(false);
            obj.killer.color = Color.white;
            obj.victim.color = Color.white;
        }
    }    


    public void UpdateCharacterAlive()
    {
        coinText.text = GameManager.ins.GetCharacterAlive().ToString();
    }
    public void SettingsButton()
    {
        UIManager.ins.OpenUI<CanvasSetting>().SetState(this);
    }

    public void HandBtn(bool isActive)
    {
        hand.SetActive(isActive);
    }    
}
