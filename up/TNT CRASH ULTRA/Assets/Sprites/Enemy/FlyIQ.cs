using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyIQ : MonoBehaviour {
    public float speed;
    public Transform target;
    public Transform FlyClonedTransform;
    public Transform Spawner;
    public GameObject FlyPrefab;
    public GameObject FlyCloned;
    public static bool inTriggerFly;
    private void Start()
    {
        StartCoroutine("Spawn");
    }
    public void Update() {
        FlyClonedTransform.transform.position = Vector2.MoveTowards(FlyClonedTransform.transform.position, target.position, speed * Time.deltaTime);
    }
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1.0f);
        FlyCloned = Instantiate(FlyPrefab, Spawner.transform.position, Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(2.8f);
        if (inTriggerFly) { Debug.Log("ти сдох сучек"); } else { Debug.Log("ахуит как ти вижил падаль тнт форевер"); }
        Destroy(FlyCloned);
        Repeat();
    }
    public void Repeat() { StartCoroutine("Spawn"); }

}
