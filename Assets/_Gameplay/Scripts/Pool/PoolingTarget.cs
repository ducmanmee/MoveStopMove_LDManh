using System.Collections.Generic;
using UnityEngine;

public class PoolingTarget : MonoBehaviour
{
    #region Singleton
    public static PoolingTarget ins;
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
        public TargetIndicator prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<TargetIndicator>> poolDictionary;

    void SetUp()
    {
        poolDictionary = new Dictionary<string, Queue<TargetIndicator>>();

        foreach (Pool p in pools)
        {
            Queue<TargetIndicator> objectPool = new Queue<TargetIndicator>();

            for (int i = 0; i < p.size; i++)
            {
                TargetIndicator obj = Instantiate(p.prefab, transform);
                obj.gameObject.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(p.tag, objectPool);
        }
    }

    TargetIndicator preb;
    public TargetIndicator SpawnFromPool(string tag)
    {
        TargetIndicator objToSpawn;
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

            TargetIndicator obj = Instantiate(preb, transform);
            obj.gameObject.SetActive(false);
            poolDictionary[tag].Enqueue(obj);

            objToSpawn = poolDictionary[tag].Dequeue();
        }

        objToSpawn.gameObject.SetActive(true);
        return objToSpawn;
    }

    public void EnQueueObj(string tag, TargetIndicator objToEnqueue)
    {
        poolDictionary[tag].Enqueue(objToEnqueue);
        objToEnqueue.gameObject.SetActive(false);
    }
}
