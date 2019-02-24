using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EyeMain : MonoBehaviour {
    private GameObject[] Spawners; // Ссылка на спавнеры
    public GameObject[] EyePrefab;
    public float spawnSpeed = 3f;
    public readonly float[] EnemySpeedsInStates = { 17f, 12f }; // Скорость глаза в разных состояниях
    void Start () {
        
        Spawners = GameObject.FindGameObjectsWithTag("SpawnerOfEnemy");
        StartCoroutine("SpawnEye");
        print(Spawners[0].name);
    }
    IEnumerator SpawnEye()
    {
        print("gg");
        bool GlitchEye;
        int c = Random.Range(0,100);
        GlitchEye = c <= 30 ? true : false;
        Spawners[0].transform.position = new Vector3(Spawners[0].transform.position.x, Random.Range(-5.0f, 5.0f), Spawners[0].transform.position.z);
        Spawners[1].transform.position = new Vector3(Spawners[1].transform.position.x, Random.Range(-5.0f, 5.0f), Spawners[1].transform.position.z);
        if(GlitchEye)
            Instantiate(EyePrefab[1], Spawners[Random.Range(0, 2)].transform.position, Quaternion.identity);
        else
            Instantiate(EyePrefab[0], Spawners[Random.Range(0, 2)].transform.position, Quaternion.identity);
        
        if(SceneManager.GetActiveScene().name == "Level3")
            yield return new WaitForSeconds(3f * spawnSpeed);
        else   
            yield return new WaitForSeconds(8f * spawnSpeed); 
        if(spawnSpeed > 1)
            spawnSpeed = spawnSpeed - 0.25f;    
        StartCoroutine("SpawnEye");

    }
}
