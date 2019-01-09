using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeMain : MonoBehaviour {
    private GameObject[] Spawners; // Ссылка на спавнеры
    public GameObject EyePrefab;
    public readonly float[] EnemySpeedsInStates = { 17f, 12f }; // Скорость глаза в разных состояниях
    void Start () {
        InvokeRepeating("SpawnEye", Random.Range(4f, 8f), 18f);
        Spawners = GameObject.FindGameObjectsWithTag("SpawnerOfEnemy");
    }
    public void SpawnEye()
    {
        Spawners[0].transform.position = new Vector3(Spawners[0].transform.position.x, Random.Range(-5.0f, 5.0f), Spawners[0].transform.position.z);
        Spawners[1].transform.position = new Vector3(Spawners[1].transform.position.x, Random.Range(-5.0f, 5.0f), Spawners[1].transform.position.z);
        Instantiate(EyePrefab, Spawners[Random.Range(0, 2)].transform.position, Quaternion.identity);
    }
}
