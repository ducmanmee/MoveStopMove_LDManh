using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    [SerializeField] private Character C;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Constain.TAG_ENEMY) || other.CompareTag(Constain.TAG_PLAYER))
        {
            Character character = other.GetComponent<Character>();
            C.AddCharacterInRange(character);
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constain.TAG_ENEMY) || other.CompareTag(Constain.TAG_PLAYER))
        {
            Character character = other.GetComponent<Character>();
            C.RemoveCharacterInRange(character);
        }
    }
}
