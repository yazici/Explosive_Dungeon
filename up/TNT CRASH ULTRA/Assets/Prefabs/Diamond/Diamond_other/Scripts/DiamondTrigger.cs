﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondTrigger : MonoBehaviour {
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            DiamondSpawn.TakeDiamond();
            DiamondSpawn.DestroyDiamond();
        }
    }
}
