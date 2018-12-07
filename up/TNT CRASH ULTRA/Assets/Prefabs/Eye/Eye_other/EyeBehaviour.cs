using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBehaviour : MonoBehaviour {
    public GameObject EyePrefab; // Ссылка на префаб глаза
    private static GameObject ClonedEnemy; // Ссылка на клон глаза
    private Animator EnemyAnimator; // Ссылка на аниматор клона
    private GameObject[] Spawners; // Ссылка на спавнеры
    private GameObject TargetPlayer; // За кем следует глаз
    public readonly float[] EnemySpeedsInStates = { 17f, 12f }; // Скорость глаза в разных состояниях
    private int StateOfEnemy = 0; // Состояние глаза
    public bool isExplosion = false;
    private AudioSource explosion_wav;
    private void Start()
    {
        explosion_wav = GameObject.Find("StandartExplosion").GetComponent<AudioSource>();
        Spawners = GameObject.FindGameObjectsWithTag("SpawnerOfEnemy");
        TargetPlayer = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(EnemySpawn());
    }
    private void FixedUpdate()
    {
        if (ClonedEnemy != null && isExplosion == false)
        {
            ClonedEnemy.transform.position = Vector2.MoveTowards(ClonedEnemy.transform.position, TargetPlayer.transform.position, EnemySpeedsInStates[StateOfEnemy] / 10 * Time.deltaTime);
        }
    }
    public IEnumerator EnemySpawn()
    {
        Spawners[0].transform.position = new Vector3(Spawners[0].transform.position.x, Random.Range(-5.0f, 5.0f), Spawners[0].transform.position.z);
        Spawners[1].transform.position = new Vector3(Spawners[1].transform.position.x, Random.Range(-5.0f, 5.0f), Spawners[1].transform.position.z);
        yield return new WaitForSeconds(Random.Range(2.0f, 6.0f));
        isExplosion = false;
        StateOfEnemy = 0;
        ClonedEnemy = Instantiate(EyePrefab, Spawners[Random.Range(0, 2)].transform.position, Quaternion.identity) as GameObject;
        EnemyAnimator = ClonedEnemy.GetComponent<Animator>();
        EnemyAnimator.SetInteger("State", 0);
        yield return new WaitForSeconds(6f);
        EnemyAnimator.SetInteger("State", 1);
        yield return new WaitForSeconds(0.05f);
        EnemyAnimator.SetInteger("State", 2);
        yield return new WaitForSeconds(0.05f);
        EnemyAnimator.SetInteger("State", 3);
        StateOfEnemy = 1;
        yield return new WaitForSeconds(3);
        EnemyAnimator.SetInteger("State", 4);
        CameraShake.Should_Shake = true;
        explosion_wav.Play();
        isExplosion = true;
        if (EyeTrigger.isPlayerOnTriger() && !MoveAndJump.Invisible) { MoveAndJump.KillPlayer(); Destroy(ClonedEnemy); }
        yield return new WaitForSeconds(0.413f);
        Destroy(ClonedEnemy);
        yield return new WaitForSeconds(3.75f);
        StartCoroutine(EnemySpawn());
    }
}
