using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT_One : MonoBehaviour {
    public GameObject Explosion;
    void Start()
    {
        StartCoroutine(TNTLifeCycle());
    }
    IEnumerator TNTLifeCycle()
    { 
        yield return new WaitForSeconds(5f);
        Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        Instantiate(Explosion, pos, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
