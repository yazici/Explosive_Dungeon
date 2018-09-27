using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : MonoBehaviour {
    public Transform Spawner;
    public GameObject TNTprefab;
    public static bool inTrigger;
    private void Start()
    {
        StartCoroutine("Spawn");
    }
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1.0f);
        Spawner.position = new Vector2(Random.Range(-8.0f, 8.0f), Spawner.position.y);
        GameObject TnTSpawned = Instantiate(TNTprefab, Spawner.transform.position, Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(2.8f);
        if (inTrigger) { Debug.Log("ти сдох сучек"); } else { Debug.Log("ахуит как ти вижил падаль тнт форевер"); }
        Destroy(TnTSpawned);

        Repeat();
    }
    public void Repeat() {StartCoroutine("Spawn"); }
   
}
