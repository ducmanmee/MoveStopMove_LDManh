using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    [Header("Item Name")]
    public int itemID;
    public string itemName;
    public int price;


    [Header("Item Stats")]
    public int damage;
    public int speed;
}
