using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public int countdownTimer;
    public Text countdownText;

    private void Start()
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
}
