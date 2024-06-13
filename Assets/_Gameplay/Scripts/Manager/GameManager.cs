using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public static GameManager ins;
    public float minDistance;
    public float maxDistance;
    public int totalEnemiesSpawned = 0;
    public int maxEnemies;
    public int maxEnemiesOnScreen;
    public int counterEnemy;

    private Quaternion startPlayer;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera shopWeaponCamera;
    [SerializeField] private Camera shopFashionCamera;
    [SerializeField] private GameObject player;

    private IState<GameManager> currentState;
    public List<Enemy> activeEnemys = new List<Enemy>();


    private void MakeInstance()
    {
        if (ins == null)
        {
            ins = this;
        }
    }

    private void Awake()
    {
        MakeInstance();
    }

    void Start()
    {
        DataManager.ins.LoadData();
        ChangeState(new MenuState());
        UIManager.ins.OpenUI<CanvasMainmenu>();
        startPlayer = Player.ins.PlayerRotation;
        Player.ins.OnInit();
    }

    public void OnInit()
    {
        ChangeState(new PlayState());
        totalEnemiesSpawned = 0;
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

    private void SpawnEnemies()
    {
        for(int i=0; i < maxEnemiesOnScreen; i++)
        {
            SwarmE();
        }      
    }
    public void SwarmE()
    {
        Enemy enemy = PoolingEnemy.ins.SpawnFromPool(Constain.TAG_ENEMY);
        enemy.OnInit();
        activeEnemys.Add(enemy);
        enemy.gameObject.transform.position = GetRandomPosition();
        totalEnemiesSpawned++;
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


    public void RemoveEnemy(Enemy enemy)
    {
        counterEnemy--;
        activeEnemys.Remove(enemy);
        if(totalEnemiesSpawned < maxEnemies) SwarmE();
    }

    public int GetCharacterAlive() => counterEnemy + 1;

    Vector3 GetRandomPosition()
    {
        NavMeshHit hit;
        Vector3 randomPosition = Vector3.zero;

        float distance = Random.Range(minDistance, maxDistance);

        Vector3 randomDirection = Random.insideUnitSphere;
        randomDirection.y = 0;
        randomDirection.Normalize();

        Vector3 potentialPosition = Player.ins.transform.position + randomDirection * distance;

        if (NavMesh.SamplePosition(potentialPosition, out hit, maxDistance, NavMesh.AllAreas))
        {
            randomPosition = hit.position;
        }

        return randomPosition;
    }

    public void PlayGame()
    {
        GameManager.ins.ChangeState(new PlayState());
        Player.ins.OnInit();
        counterEnemy = maxEnemies;
        UIManager.ins.OpenUI<CanvasGameplay>();
        Time.timeScale = 1;
        GameManager.ins.OnInit();
        CanvasGameplay.ins.UpdateCharacterAlive();
    }

    public void RestartPlayer()
    {
        Player.ins.OnInit();
        Player.ins.PlayerRotation = startPlayer;
        Time.timeScale = 1;
    }

    public IEnumerator LoseGame()
    {
        yield return new WaitForSeconds(1.5f);
        if(!(currentState is MenuState) && Player.ins.IsDead)
        {
            UIManager.ins.CloseAllUI();
            UIManager.ins.OpenUI<CanvasFail>();
            CanvasFail.ins.SetNameKiller(Player.ins.NameOfKiller);
            CanvasFail.ins.SetRank(Player.ins.RankPlayer);
        }
    }

    public void CheckWin()
    {
        CanvasGameplay.ins.UpdateCharacterAlive();
        if (counterEnemy == 0)
        {
            StartCoroutine(WinGame());
        }
    }    

    public IEnumerator WinGame()
    {
        Player.ins.IsWin = true;
        Player.ins.PlayerRotation = startPlayer;
        Player.ins.ChangeState(new PWinState());
        yield return new WaitForSeconds(3.5f);
        if (!(GameManager.ins.GetGameState() is MenuState))
        {
            UIManager.ins.CloseAllUI();
            UIManager.ins.OpenUI<CanvasVictory>();
            GameManager.ins.ClearEnemyActive();
        }    
    }

    public IState<GameManager> GetGameState() => currentState;

    public void HideShopWeaponCamera(bool hide)
    {
        player.SetActive(!hide);
        mainCamera.enabled = !hide;
        shopWeaponCamera.enabled = hide;
    }

    public void HideShopFashionCamera(bool hide)
    {
        mainCamera.enabled = !hide;
        shopFashionCamera.enabled = hide;
    }

}
