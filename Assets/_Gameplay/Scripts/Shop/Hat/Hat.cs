using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pant", menuName = "Scriptable Objects/Hats")]

public class Hat : ScriptableObject
{
    [Header("Hat Name")]
    public int hatID;
    public string hatName;
    public int priceHat;


    [Header("Hat Stats")]
    public int damage;
    public int speed;

    [Header("Hat Model")]
    public GameObject hatModel;
}
