using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchTNTBehaviour : MonoBehaviour {
    public GameObject prefab;
    public static bool spawntnt;
    private GameObject[] clones = new GameObject[10];
    public GameObject spawner;
    private Animator tnt_cloned_anim;
    private AudioSource explosion_wav;
    private void Start()
    {
        explosion_wav = GameObject.Find("LaserExplosion").GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (spawntnt) { SpawnTNTCommand(); }
    }
    public void SpawnTNTCommand() { StartCoroutine(SpawnGlitchTNTs()); spawntnt = false; }
    public IEnumerator SpawnGlitchTNTs()
    {
        for (int i = 0; i != 10; i++) { spawner.transform.position = new Vector3(Random.Range(-8.0f, 8.0f), spawner.transform.position.y, spawner.transform.position.z); clones[i] = Instantiate(prefab, spawner.transform.position, Quaternion.identity) as GameObject; }
        yield return new WaitForSeconds(Random.Range(0, 1));
    }
}
