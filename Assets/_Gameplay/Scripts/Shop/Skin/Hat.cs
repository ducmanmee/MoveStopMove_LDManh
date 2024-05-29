using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pant", menuName = "Scriptable Objects/Hats")]

public class Hat : Item
{
    [Header("Hat Model")]
    public GameObject hatModel;
}
