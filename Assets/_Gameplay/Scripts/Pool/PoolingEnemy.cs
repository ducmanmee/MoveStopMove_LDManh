using System.Collections.Generic;
using UnityEngine;

public class PoolingEnemy : MonoBehaviour
{
    #region Singleton
    public static PoolingEnemy ins;
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
        public Enemy prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<Enemy>> poolDictionary;

    void SetUp()
    {
        poolDictionary = new Dictionary<string, Queue<Enemy>>();

        foreach (Pool p in pools)
        {
            Queue<Enemy> objectPool = new Queue<Enemy>();

            for (int i = 0; i < p.size; i++)
            {
                Enemy obj = Instantiate(p.prefab, transform);
                obj.gameObject.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(p.tag, objectPool);
        }
    }

    Enemy preb;
    public Enemy SpawnFromPool(string tag)
    {
        Enemy objToSpawn;
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

            Enemy obj = Instantiate(preb, transform);
            obj.gameObject.SetActive(false);
            poolDictionary[tag].Enqueue(obj);

            objToSpawn = poolDictionary[tag].Dequeue();
        }

        objToSpawn.gameObject.SetActive(true);
        return objToSpawn;
    }

    public void EnQueueObj(string tag, Enemy objToEnqueue)
    {
        poolDictionary[tag].Enqueue(objToEnqueue);
        objToEnqueue.gameObject.SetActive(false);
    }
}
