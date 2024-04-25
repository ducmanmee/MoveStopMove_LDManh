using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public int numberOfEnemies = 50;
    public float minDistance = 10f;
    public float maxDistance = 50f;
    void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        //SpawnEnemies();
    }    

    void SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            GameObject enemy = Pooling.ins.SpawnFromPool(Constain.TAG_ENEMY);
            enemy.transform.position = randomPosition;
        }
    }

    Vector3 GetRandomPosition()
    {
        NavMeshHit hit;
        Vector3 randomPosition = Vector3.zero;
        float distance = Random.Range(minDistance, maxDistance);
        Vector3 randomDirection = Random.insideUnitSphere.normalized;
        randomDirection.y = 0; // Ensure the position is on the same level as the player

        if (NavMesh.SamplePosition(Player.Instance.transform.position + randomDirection * distance, out hit, maxDistance, NavMesh.AllAreas))
        {
            randomPosition = hit.position;
        }

        return randomPosition;
    }

}
