using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glitch_Main : MonoBehaviour {
    private GameObject Spawner; // Ссылка на спавнеры
    public GameObject TNTPrefab;
    public static Glitch_Main Instance;
    void Start()
    {
        Instance = this;
        Spawner = GameObject.Find("SpawnerOfTNT");
    }
    public void SpawnGlitchTNTs()
    {
        for (int i = 0; i != 10; i++)
        {
            Spawner.transform.position = new Vector3(Random.Range(-8.0f, 8.0f), Spawner.transform.position.y, Spawner.transform.position.z);
            Instantiate(TNTPrefab, Spawner.transform.position, Quaternion.identity);
        }
    }
}
