﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : CharacterStats


{
    // Use this for initialization

    void Start () {

        

    }
    

    public override void Die()
    {
        base.Die();
        PlayerManager.instance.KillPlayer();
    }
}
