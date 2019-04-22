using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject Fish;
    public float FishSpawnerMultiplier=3.0f;
    public Transform[] Spawners;
    void Start()
    {
        StartCoroutine(SpawnFish());
    }

    // Update is called once per frame
    IEnumerator SpawnFish()
    {
        yield return new WaitForSeconds(3.0f*FishSpawnerMultiplier);
        Instantiate (Fish, Spawners[Random.Range(0, Spawners.Length)].position, Quaternion.identity);
        if(FishSpawnerMultiplier != 1f)
            FishSpawnerMultiplier -= 0.25f;
        StartCoroutine(SpawnFish());
    }
}
