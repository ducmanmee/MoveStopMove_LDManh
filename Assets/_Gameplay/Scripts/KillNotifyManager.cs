using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KillNotifyManager : Singleton<KillNotifyManager>
{
    [SerializeField] Text killer;
    [SerializeField] Text victim;
    bool isActive;

    public void SetNotifyKill(string name1, string name2)
    {
        killer.text = name1;
        victim.text = name2;
    }

    public bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }
}
