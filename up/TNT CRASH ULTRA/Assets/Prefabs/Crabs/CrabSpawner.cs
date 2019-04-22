using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CrabSpawner : MonoBehaviour {
    private GameObject[] Spawners; // Ссылка на спавнеры
    public GameObject CrabPrefab;
    public GameObject[] ExclamationMark;
    public AudioSource MusicSource;
    public AudioClip[] MusicAudioClip;
    void Start () 
    {        
        Spawners = GameObject.FindGameObjectsWithTag("SpawnerOfCrabs");
        foreach(GameObject g in ExclamationMark)
            g.SetActive(false);
        StartCoroutine("SpawnCrab");
    }
    IEnumerator SpawnCrab()
    {
        yield return new WaitForSeconds(70f);
        int r = Random.RandomRange(0,2);
        bool MoveDirection = r == 0 ? true : false;
        if(MoveDirection)
            ExclamationMark[1].SetActive(true);
        else
            ExclamationMark[0].SetActive(true);
        yield return new WaitForSeconds(5);
        if(PlayerPrefs.GetString("Music") == "true")
        while(MusicSource.volume != 0){
            MusicSource.volume-=0.1f;
            yield return new WaitForSeconds(0.35f);
        }
        MusicSource.clip = MusicAudioClip[1];
            MusicSource.Play();
        if(PlayerPrefs.GetString("Music") == "true")
            while(MusicSource.volume != 0.5){
                MusicSource.volume+=0.1f;
                yield return new WaitForSeconds(0.35f);
        }
        if(MoveDirection)
            ExclamationMark[1].SetActive(false);
        else
            ExclamationMark[0].SetActive(false);
        for(int i = 0; i!=20; i++){
            yield return new WaitForSeconds(0.3f);
        if(MoveDirection)
            Instantiate(CrabPrefab, Spawners[0].transform.position, Quaternion.identity);
        else
            Instantiate(CrabPrefab, Spawners[1].transform.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(6f);
        if(PlayerPrefs.GetString("Music") == "true")
            while(MusicSource.volume != 0){
                MusicSource.volume-=0.1f;
                yield return new WaitForSeconds(0.35f);
        }
        MusicSource.clip = MusicAudioClip[0];
        MusicSource.Play();
        if(PlayerPrefs.GetString("Music") == "true")
        while(MusicSource.volume != 0.5f){
            MusicSource.volume+=0.1f;
            yield return new WaitForSeconds(0.35f);
        }
        StartCoroutine("SpawnCrab");
        
    }
}
