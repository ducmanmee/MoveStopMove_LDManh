using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    [SerializeField] private GameObject Aim;
    private GameObject AimPrefab;

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            AimPrefab = Instantiate(Aim, new Vector3(other.transform.position.x, other.transform.position.y + .1f, other.transform.position.z), Quaternion.Euler(new Vector3(90f, 0f, 0f)), other.transform);
        }
    }

}
