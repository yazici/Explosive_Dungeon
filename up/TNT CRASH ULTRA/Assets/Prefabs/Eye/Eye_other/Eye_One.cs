using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye_One : MonoBehaviour {
    public GameObject Explosion;
    private bool Angry;
    private GameObject TargetPlayer;
	void Start () {
        StartCoroutine(EyeLifeCycle());
        TargetPlayer = GameObject.FindGameObjectWithTag("Player");
    }
    private void FixedUpdate()
    {
        float speed = Angry ? 12f : 17f;
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, TargetPlayer.transform.position, speed / 10 * Time.deltaTime);
    }
    IEnumerator EyeLifeCycle()
    {
        Angry = false;
        Animator A = gameObject.GetComponent<Animator>();
        A.Play("LifeCycle_0", 0);
        yield return new WaitForSeconds(6f);
        A.Play("LifeCycle_1", 0);
        yield return new WaitForSeconds(0.05f);
        A.Play("LifeCycle_2", 0);
        yield return new WaitForSeconds(0.05f);
        A.Play("LifeCycle_3", 0);
        Angry = true;
        yield return new WaitForSeconds(3f);
        Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        Instantiate(Explosion, pos, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
