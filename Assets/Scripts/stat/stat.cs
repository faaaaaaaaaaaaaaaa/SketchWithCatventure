using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class stat 
{
    [SerializeField]
    private int basevalue;

    public int GetValue ()
    {
        return basevalue;
    }
}
