using DG.Tweening.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public static CountdownTimer ins;
    public int countdownTimer;
    public Text countdownText;

    private void MakeInstance()
    {
        if(ins  == null)
        {
            ins = this;
        }    
    }

    private void Awake()
    {
        MakeInstance();
    }

    private void OnEnable()
    {
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        while (countdownTimer > 0)
        {
            countdownText.text = countdownTimer.ToString();
            yield return new WaitForSeconds(1f);
            countdownTimer--;
        }
        CanvasRevival.ins.DontRevive();
    }    

    public void ResetCountdown()
    { 
        countdownTimer = 5;
        StartCoroutine(Countdown());
    }
}
