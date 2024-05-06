using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager ins;
    [SerializeField] GameObject mapPrefab;

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
    
    public void OnInit()
    {
        GameObject map = Instantiate(mapPrefab, Vector3.zero, Quaternion.identity);
    }    

    public void NextLevel()
    {

    }    
}
