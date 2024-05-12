using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataManager : MonoBehaviour
{
    public static DataManager ins;
    
    private void Awake()
    {
        ins = this;
        DontDestroyOnLoad(gameObject);
    }
    public bool isLoaded = false;
    public PlayerData playerData;
    public const string PLAYER_DATA = "PLAYER_DATA";


    private void OnApplicationPause(bool pause) { SaveData();  }
    private void OnApplicationQuit() { SaveData(); }

    public void LoadData()
    {
        string d = PlayerPrefs.GetString(PLAYER_DATA, "");
        if (d != "")
        {
            playerData = JsonUtility.FromJson<PlayerData>(d);
        }
        else
        {
            playerData = new PlayerData();
        }
        isLoaded = true;
        //loadskin
        //load pet

    }

    public void SaveData()
    {
        if (!isLoaded) return;
        string json = JsonUtility.ToJson(playerData);
        PlayerPrefs.SetString(PLAYER_DATA, json);
    }
}


[System.Serializable]
public class PlayerData
{
    [Header("--------- Game Setting ---------")]
    public bool isNew = true;
    public bool isMusic = true;
    public bool isSound = true;
    public bool isVibrate = true;
    public bool isNoAds = false;
    public int starRate = -1;


    [Header("--------- Game Params ---------")]
    public int gold = 10000;
    public int level = 0;
    public int idSkin = 0;
    public int idPant = 0;
    public int idHat = 20;

    public int idWeapon = 0;
    public bool[] status_Weapon = { true, false, false, false, false, false, false, false, false, false, false, false};
    public bool[] status_Pant = { true, false, false, false, false, false, false, false, false};
    public bool[] status_Hat = { false, false, false, false, false, false, false, false, false, false};


}
