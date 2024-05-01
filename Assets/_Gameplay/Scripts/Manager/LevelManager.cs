using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

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
    

    public void NextLevel()
    {

    }    
}
