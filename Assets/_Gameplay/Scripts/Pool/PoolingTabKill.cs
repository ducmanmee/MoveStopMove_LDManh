
using System.Collections.Generic;
using UnityEngine;

public class PoolingTabKill : MonoBehaviour
{
    #region Singleton
    public static PoolingTabKill ins;
    public void Awake()
    {
        ins = this;
        SetUp();
    }
    #endregion

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public KillNotifyManager prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<KillNotifyManager>> poolDictionary;

    void SetUp()
    {
        poolDictionary = new Dictionary<string, Queue<KillNotifyManager>>();

        foreach (Pool p in pools)
        {
            Queue<KillNotifyManager> objectPool = new Queue<KillNotifyManager>();

            for (int i = 0; i < p.size; i++)
            {
                KillNotifyManager obj = Instantiate(p.prefab, transform);
                obj.gameObject.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(p.tag, objectPool);
        }
    }

    KillNotifyManager preb;
    public KillNotifyManager SpawnFromPool(string tag)
    {
        KillNotifyManager objToSpawn;
        try
        {
            objToSpawn = poolDictionary[tag].Dequeue();
        }
        catch
        {

            foreach (Pool p in pools)
            {
                if (p.tag.Equals(tag))
                {
                    preb = p.prefab;
                }
            }

            KillNotifyManager obj = Instantiate(preb, transform);
            obj.gameObject.SetActive(false);
            poolDictionary[tag].Enqueue(obj);

            objToSpawn = poolDictionary[tag].Dequeue();
        }

        objToSpawn.gameObject.SetActive(true);
        return objToSpawn;
    }

    public void EnQueueObj(string tag, KillNotifyManager objToEnqueue)
    {
        poolDictionary[tag].Enqueue(objToEnqueue);
        objToEnqueue.gameObject.SetActive(false);
    }
}

