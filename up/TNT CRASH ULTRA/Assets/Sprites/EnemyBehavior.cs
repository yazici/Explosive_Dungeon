using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBehavior : MonoBehaviour {
    public GameObject EnemyPrefab;
    private GameObject EnemyCloned;
    private Animator EnemyAnimator;
    public Transform[] Spawners;
    public float[] Offset;
    public Transform target;
    public float enemySpeed;
    public void Start () {
        StartCoroutine(EnemySpawn());
	}
    void Update()
    {
        if (EnemyCloned != null)
        {
            EnemyCloned.transform.position = Vector2.MoveTowards(EnemyCloned.transform.position, target.position, enemySpeed / 10 * Time.deltaTime);
        }
    }
    IEnumerator EnemySpawn (){
        EnemyCloned = Instantiate(EnemyPrefab, Spawners[Random.Range(0,1)].transform.position, Quaternion.identity) as GameObject;
        EnemyAnimator = EnemyCloned.GetComponent<Animator>();
        EnemyAnimator.SetInteger("State", 0);
        yield return new WaitForSeconds(5);
        EnemyAnimator.SetInteger("State", 1);
        yield return new WaitForSeconds(5);
        EnemyAnimator.SetInteger("State", 2);
        yield return new WaitForSeconds(5);
        EnemyAnimator.SetInteger("State", 3);
        enemySpeed = 7.5f;
        yield return new WaitForSeconds(5);
        //explose
        enemySpeed = 10;
        Destroy(EnemyCloned);
    }
}
