using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glitch_One : MonoBehaviour {
    public GameObject Explosion;
    void Start()
    {
        StartCoroutine(GlitchTNTLifeCycle());
    }
    IEnumerator GlitchTNTLifeCycle()
    {
        yield return new WaitForSeconds(Random.Range(3f, 6f));
        Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        Instantiate(Explosion, pos, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
