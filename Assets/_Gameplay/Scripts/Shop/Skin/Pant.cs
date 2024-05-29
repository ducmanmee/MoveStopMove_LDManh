using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pant", menuName = "Scriptable Objects/Pant")]

public class Pant : Item
{
  [Header("Pant Model")]
    public Material pantMaterial;
}
