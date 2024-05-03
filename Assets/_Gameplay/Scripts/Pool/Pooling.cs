using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour
{
    #region Singleton
    public static Pooling ins;
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
        public WeaponController prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<WeaponController>> poolDictionary;

    void SetUp()
    {
        poolDictionary = new Dictionary<string, Queue<WeaponController>>();

        foreach (Pool p in pools)
        {
            Queue<WeaponController> objectPool = new Queue<WeaponController>();

            for (int i = 0; i < p.size; i++)
            {
                WeaponController obj = Instantiate(p.prefab, transform);
                obj.gameObject.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(p.tag, objectPool);
        }
    }

    WeaponController preb;
    public WeaponController SpawnFromPool(string tag)
    {
        WeaponController objToSpawn;
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

            WeaponController obj = Instantiate(preb, transform);
            obj.gameObject.SetActive(false);
            poolDictionary[tag].Enqueue(obj);

            objToSpawn = poolDictionary[tag].Dequeue();
        }

        objToSpawn.gameObject.SetActive(true);
        return objToSpawn;
    }

    public void EnQueueObj(string tag, WeaponController objToEnqueue)
    {
        poolDictionary[tag].Enqueue(objToEnqueue);
        objToEnqueue.gameObject.SetActive(false);
    }
}
