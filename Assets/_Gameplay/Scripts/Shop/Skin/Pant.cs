using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pant", menuName = "Scriptable Objects/Pant")]

public class Pant : ScriptableObject
{
    [Header ("Pant Name")]
    public int pantID;
    public string pantName;
    public int pricePant;

    [Header("Pant Stats")]
    public int damage;
    public int speed;

    [Header("Pant Model")]
    public Material pantMaterial;
}
