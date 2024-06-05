using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    [SerializeField] GameObject boosterPrefab;
    private bool isBooster;
    private void OnTriggerEnter(Collider other)
    {
        if(!isBooster)
        {
            if(other.CompareTag(Constain.TAG_PLAYER) || other.CompareTag(Constain.TAG_ENEMY))
            {
                boosterPrefab.SetActive(false);
                StartCoroutine(ActiveBooster());
                Character C = Cache.GetCharacter(other);
                C.weaponBoosted = true;
                isBooster = true;
            }    
        }    
    }
    IEnumerator ActiveBooster()
    {
        yield return new WaitForSeconds(10f);
        boosterPrefab.SetActive(true);
        isBooster = false;
    }
}
