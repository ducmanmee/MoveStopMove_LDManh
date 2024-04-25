using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeEnemy : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Constain.TAG_PLAYER))
        {
            enemy.AddCharacterInRange(other.GetComponent<Character>());
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constain.TAG_PLAYER))
        {
            enemy.RemoveCharacterInRange(other.GetComponent<Character>());
        }
    }
}
