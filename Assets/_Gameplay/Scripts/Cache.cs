using UnityEngine;
using System.Collections.Generic;
using System;

public class Cache
{
    //Get Rect Tranform
    private static Dictionary<Collider, Character> m_Character = new Dictionary<Collider, Character>();

    public static Character GetCharacter(Collider collider)
    {
        if (!m_Character.ContainsKey(collider))
        {
            m_Character[collider] = collider.GetComponent<Character>();
        }

        return m_Character[collider];
    }


}
