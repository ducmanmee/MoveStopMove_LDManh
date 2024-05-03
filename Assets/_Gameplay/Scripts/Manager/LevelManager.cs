using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] GameObject mapPrefab;

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
    }
    
    public void OnInit()
    {
        GameObject map = Instantiate(mapPrefab, Vector3.zero, Quaternion.identity);
    }    

    public void NextLevel()
    {

    }    
}
