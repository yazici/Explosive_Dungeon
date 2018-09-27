using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondSpawn : MonoBehaviour {
    private static GameObject DiamondCloned;
    public GameObject DiamondPrefab;
    public Transform DiamondSpawner;
    public static int CurrentValueOfDiamonds;
   
    private void Update()
    {
        if (DiamondCloned == null)
        {
            DiamondSpawner.position = new Vector2(Random.Range(-8.0f, 8.0f), DiamondSpawner.position.y);
            DiamondCloned = Instantiate(DiamondPrefab, DiamondSpawner.transform.position, Quaternion.identity) as GameObject;
        Debug.Log(CurrentValueOfDiamonds);
        }
    }
    public static void DestroyDiamond() { Destroy(DiamondCloned); }
}

