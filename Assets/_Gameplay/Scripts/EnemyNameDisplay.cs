using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyNameDisplay : MonoBehaviour
{
    public Text nameE;
    
    public void SetName(string name)
    {
        nameE.text = name;
    }    
}
