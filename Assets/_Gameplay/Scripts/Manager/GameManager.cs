using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public static GameManager ins;

    [Header("Enemy Settings")]
    public float minDistance;
    public float maxDistance;
    public int totalEnemiesSpawned = 0;
    public int maxEnemies;
    public int maxEnemiesOnScreen;
    public int counterEnemy;

    [Header("References")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera shopWeaponCamera;
    [SerializeField] private Camera shopFashionCamera;
    [SerializeField] private GameObject player;

    private Quaternion startPlayer;
    private IState<GameManager> currentState;

    public List<Enemy> activeEnemys = new List<Enemy>();
    public List<TargetIndicator> activeTarget = new List<TargetIndicator>();

    private bool isRevive;

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

    private void Start()
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
        currentState?.OnExit(this);

        currentState = newState;

        currentState?.OnEnter(this);
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < maxEnemiesOnScreen; i++)
        {
            SwarmE();
        }
    }

    public void SwarmE()
    {
        Enemy enemy = PoolingEnemy.ins.SpawnFromPool(Constain.TAG_ENEMY);
        enemy.OnInit();
        activeEnemys.Add(enemy);
        enemy.transform.position = GetRandomPosition();
        totalEnemiesSpawned++;
    }

    public void AddTargetActive(TargetIndicator T)
    {
        activeTarget.Add(T);
    }

    public void ClearTarget()
    {
        foreach (TargetIndicator target in activeTarget)
        {
            if (!target.gameObject.activeSelf) continue;
            PoolingTarget.ins.EnQueueObj(Constain.TAG_TARGET, target);
        }
        activeTarget.Clear();
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
        activeTarget.Remove(enemy.targetE);

        if (totalEnemiesSpawned < maxEnemies)
        {
            SwarmE();
        }
    }

    public int GetCharacterAlive() => counterEnemy + 1;

    private Vector3 GetRandomPosition()
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
        ChangeState(new PlayState());
        Player.ins.OnInit();
        Player.ins.ActiveName();
        counterEnemy = maxEnemies;
        UIManager.ins.CloseAllUI();
        UIManager.ins.OpenUI<CanvasGameplay>();
        Time.timeScale = 1;
        OnInit();
        CanvasGameplay.ins.UpdateCharacterAlive();
    }

    public void RestartToAgain()
    {
        StopAllCoroutines();
        CanvasGameplay.ins.ResetTabNotify();
        isRevive = false;
        ResetPlayer();
        ClearTarget();
        Time.timeScale = 1;
    }

    public void ResetPlayer()
    {
        Player.ins.OnInit();
        Player.ins.ResetName();
        Player.ins.PlayerRotation = startPlayer;
    }    

    public IEnumerator LoseGame()
    {
        yield return new WaitForSeconds(1.5f);
        UIManager.ins.CloseAllUI();
        if(!isRevive)
        {
            isRevive = true;
            UIManager.ins.OpenUI<CanvasRevival>();
        }
        else
        {
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
            if (!(GetGameState() is WinState))
            {
                ChangeState(new WinState());
            }
        }
        else
        {
            if (Player.ins.IsDead && !(GetGameState() is LoseState))
            {
                ChangeState(new LoseState());
            }
        }
    }

    public IEnumerator WinGame()
    {
        Player.ins.IsWin = true;
        Player.ins.PlayerRotation = startPlayer;
        Player.ins.ChangeState(new PWinState());
        yield return new WaitForSeconds(3.5f);
        UIManager.ins.CloseAllUI();
        UIManager.ins.OpenUI<CanvasVictory>();
        ClearEnemyActive();
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
