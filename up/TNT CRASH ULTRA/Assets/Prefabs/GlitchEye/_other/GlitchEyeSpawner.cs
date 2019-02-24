using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchEyeSpawner : MonoBehaviour
{
    public GameObject Explosion;
    public GameObject GlitchEye;
    private GameObject TargetPlayer;

    void Start()
    {
        GlitchEye = gameObject;
        TargetPlayer = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(EyeLifeCycle());
    }

    // Update is called once per frame
    void FixedUpdate() => GlitchEye.transform.position = Vector2.MoveTowards(gameObject.transform.position, TargetPlayer.transform.position, 10 / 10 * Time.deltaTime);
    IEnumerator EyeLifeCycle()
    {
        Animator A = GlitchEye.GetComponent<Animator>();
        yield return new WaitForSeconds(Random.Range(4,12));
        Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        Instantiate(Explosion, pos, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
