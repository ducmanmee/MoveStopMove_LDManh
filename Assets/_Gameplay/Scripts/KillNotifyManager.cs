using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KillNotifyManager : Singleton<KillNotifyManager>
{
    public Text killer;
    public Text victim;
    bool isActive;

    public void SetNotifyKill(string name1, string name2)
    {
        this.gameObject.SetActive(true);
        isActive = true;
        killer.text = name1;
        victim.text = name2;
        if (name1 == Player.ins.NameOfCharacter)
        {
            killer.color = Color.green;
        }
        else if(name2 == Player.ins.NameOfCharacter)
        {
            victim.color = Color.red;
        }
        else
        {
            killer.color = Color.white;
            victim.color = Color.white;
        }    
        
    }

    public bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }
}
