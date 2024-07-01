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
    public PlayerData dt;
    public const string PLAYER_DATA = "PLAYER_DATA";


    private void OnApplicationPause(bool pause) { SaveData();  }
    private void OnApplicationQuit() { SaveData(); }

    public void LoadData()
    {
        string d = PlayerPrefs.GetString(PLAYER_DATA, "");
        if (d != "")
        {
            dt = JsonUtility.FromJson<PlayerData>(d);
        }
        else
        {
            dt = new PlayerData();
        }
        isLoaded = true;
        //loadskin
        //load pet

    }

    public void SaveData()
    {
        if (!isLoaded) return;
        string json = JsonUtility.ToJson(dt);
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
    public string namePlayer = "You";
    public int gold = 10000;
    public int level = 0;
    public int idSkin = 0;
    public int idPant = 0;
    public int idHat = 20;
    public int idSetSkin = 0;
    public int idKhien = 20;

    public int idWeapon = 0;
    public bool[] status_Weapon = { true, false, false, false, false, false, false, false, false, false, false, false};
    public bool[] status_Pant = {true, false, false, false, false, false, false, false, false, false};
    public bool[] status_Hat = {true, false, false, false, false, false, false, false, false, false, false};
    public bool[] status_SetSkin = {true, false, false, false, false, false};
    public bool[] status_Khien = {true, false, false};


}
