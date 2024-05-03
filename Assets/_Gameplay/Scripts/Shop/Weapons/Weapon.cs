using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Scriptable Objects/Weapon")]

public class Weapon : ScriptableObject
{
    [Header ("Weapon Name")]
    public int mapIndex;
    public string weaponName;

    [Header("Weapon Stats")]
    public int damage;
    public int speed;

    [Header("Weapon Model")]
    public GameObject weaponModel;
}
