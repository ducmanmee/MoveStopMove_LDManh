using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangerShop : MonoBehaviour
{
    public int idShopFashion;
    public void SetActiveShop()
    {
        CanvasShopFashion.ins.SetStateShop(idShopFashion);
    }    
}
