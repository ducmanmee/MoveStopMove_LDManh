using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int numberOfEnemies = 100;
    public float minDistance;
    public float maxDistance;

    public IState<GameManager> currentState;
    public List<Enemy> activeEnemys = new List<Enemy>();


    private void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }    
    }

    private void Awake()
    {
        MakeInstance();
        Time.timeScale = 0;
        
    }

    void Start()
    {
        UIManager.Instance.OpenUI<CanvasMainmenu>();
    }

    public void OnInit()
    {
        ChangeState(new PlayState());
        SpawnEnemies();
    }

    public void ChangeState(IState<GameManager> newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    void SpawnEnemies()
    {
        List<Vector3> enemyPositions = new List<Vector3>();
        int spawnedEnemies = 0;
        int maxAttempts = 1000;

        while (spawnedEnemies < numberOfEnemies && maxAttempts > 0)
        {
            Vector3 randomPosition = GetRandomPosition();
            bool isValidPosition = true;

            if (Vector3.Distance(randomPosition, Player.Instance.transform.position) < minDistance)
            {
                isValidPosition = false;
            }

            foreach (Vector3 pos in enemyPositions)
            {
                if (Vector3.Distance(randomPosition, pos) < minDistance)
                {
                    isValidPosition = false;
                    break;
                }
            }

            if (isValidPosition)
            {
                Enemy enemy = PoolingEnemy.ins.SpawnFromPool(Constain.TAG_ENEMY);
                enemy.OnInit();
                activeEnemys.Add(enemy);
                enemy.gameObject.transform.position = randomPosition;
                enemyPositions.Add(randomPosition);
                spawnedEnemies++;
            }

            maxAttempts--; 
        }
    }


    public void ClearEnemyActive()
    {
        foreach (Enemy enemy in activeEnemys)
        {
            if (!enemy.gameObject.activeSelf) continue;
            PoolingEnemy.ins.EnQueueObj(Constain.TAG_ENEMY, enemy);
        }
        activeEnemys.Clear();
    }

    Vector3 GetRandomPosition()
    {
        NavMeshHit hit;
        Vector3 randomPosition = Vector3.zero;
        float distance = Random.Range(minDistance, maxDistance);
        Vector3 randomDirection = Random.insideUnitSphere.normalized;
        randomDirection.y = 0;

        if (NavMesh.SamplePosition(Player.Instance.transform.position + randomDirection * distance, out hit, maxDistance, NavMesh.AllAreas))
        {
            randomPosition = hit.position;
        }

        return randomPosition;
    }

}
