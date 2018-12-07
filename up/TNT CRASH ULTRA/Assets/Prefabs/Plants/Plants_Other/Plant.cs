using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {
    public Sprite[] Plants;
    private void Start()
    {
        SpawnPlant();
    }
    public void SpawnPlant()
    {
        if (Random.Range(0, 2) == 1)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = Plants[Random.Range(0, Plants.Length)];
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = null;
        }
    }
}
