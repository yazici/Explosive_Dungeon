using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject Fish;
    public Transform Spawner;
    void Start()
    {
        StartCoroutine(SpawnFish());
    }

    // Update is called once per frame
    IEnumerator SpawnFish()
    {
        print("privetik");
        int r = Random.Range(0,2);
        Vector3 _spawnerPos = r == 0 ? new Vector3(-3.0f, Spawner.position.y, Spawner.position.z) : new Vector3(3.0f, Spawner.position.y, Spawner.position.z);
        Spawner.position = _spawnerPos;
        Instantiate (Fish, Spawner.position, Quaternion.identity);
        yield return new WaitForSeconds(5.0f);
        StartCoroutine(SpawnFish());
    }
}
