using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager ins;
    [SerializeField] List<GameObject> mapPrefab;
    [SerializeField] List<Transform> swarmEnemyPos;
    private GameObject map;
    public int level;

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
        OnInit();
    }

    public void OnInit()
    {
        level = 0;
        ChangeMap(level);
    }    

    public void NextLevel()
    {
        level++;
        ChangeMap(level);
        if (level >=  mapPrefab.Count)
        {
            ChangeMap(mapPrefab.Count - 1);
        }
    }

    public void ChangeMap(int level)
    {
        if(map != null)
        {
            Destroy(map);
        }
        map = Instantiate(mapPrefab[level], Vector3.zero, Quaternion.identity);
    }

    public List<Transform> GetSwarmPos() => swarmEnemyPos;
}
