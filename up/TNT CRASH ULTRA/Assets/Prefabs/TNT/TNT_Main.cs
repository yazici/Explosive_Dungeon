using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT_Main : MonoBehaviour
{
    private GameObject Spawner; // Ссылка на спавнеры
    public GameObject TNTPrefab;
    void Start()
    {
        Spawner = GameObject.Find("SpawnerOfTNT");
        InvokeRepeating("SpawnTNT", Random.Range(2f, 6f), 18f);
    }
    public void SpawnTNT()
    {
        Spawner.transform.position = new Vector3(Random.Range(-8.0f, 8.0f), Spawner.transform.position.y, Spawner.transform.position.z);
        Instantiate(TNTPrefab, Spawner.transform.position, Quaternion.identity);
    }
}