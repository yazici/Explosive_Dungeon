using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondTrigger : MonoBehaviour {
    public GameObject adskiy_razgon;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Instantiate(adskiy_razgon, this.transform.position, Quaternion.identity);
            DiamondSpawn.TakeDiamond();
            DiamondSpawn.DestroyDiamond();
        }
    }
}
