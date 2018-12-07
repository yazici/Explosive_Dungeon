using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEye_Behaviour : MonoBehaviour {
    public GameObject EyePrefab; // Ссылка на префаб глаза
    private GameObject ClonedEnemy; // Ссылка на клон глаза
    private Animator EnemyAnimator; // Ссылка на аниматор клона
    private GameObject[] Spawners; // Ссылка на спавнеры
    private GameObject TargetPlayer; // За кем следует глаз
    public bool isExplosion = false;
    private AudioSource explosion_wav;
    private void Start()
    {
        explosion_wav = GameObject.Find("LaserExplosion").GetComponent<AudioSource>();
        Spawners = GameObject.FindGameObjectsWithTag("SpawnerOfEnemy");
        TargetPlayer = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(EnemySpawn());
    }
    private void FixedUpdate()
    {
        if (isExplosion) { if (LaserEye_Trigger.isPlayerOnTriger() && !MoveAndJump.Invisible || LaserEYETRIGGER2.isPlayerOnTriger() && !MoveAndJump.Invisible) { MoveAndJump.KillPlayer(); } }
        if (ClonedEnemy != null && isExplosion == false)
        {
            ClonedEnemy.transform.position = Vector2.MoveTowards(ClonedEnemy.transform.position, TargetPlayer.transform.position, 17.5f / 10 * Time.deltaTime);
        }
    }
    public IEnumerator EnemySpawn()
    {
        Spawners[0].transform.position = new Vector3(Spawners[0].transform.position.x, Random.Range(-5.0f, 5.0f), Spawners[0].transform.position.z);
        Spawners[1].transform.position = new Vector3(Spawners[1].transform.position.x, Random.Range(-5.0f, 5.0f), Spawners[1].transform.position.z);
        yield return new WaitForSeconds(Random.Range(6.0f, 12.0f)); // 6 12
        isExplosion = false;
        ClonedEnemy = Instantiate(EyePrefab, Spawners[Random.Range(0, 2)].transform.position, Quaternion.identity) as GameObject;
        EnemyAnimator = ClonedEnemy.GetComponent<Animator>();
        EnemyAnimator.SetInteger("State", 0);
        yield return new WaitForSecondsRealtime(7.5f);//7,5
        EnemyAnimator.SetInteger("State", 1);
        CameraShake.Should_Shake = true;
        if (isExplosion) { if (LaserEye_Trigger.isPlayerOnTriger() && !MoveAndJump.Invisible  || LaserEYETRIGGER2.isPlayerOnTriger() && !MoveAndJump.Invisible) { MoveAndJump.KillPlayer();  } }
        explosion_wav.Play();
        isExplosion = true;
        yield return new WaitForSecondsRealtime(0.5f);//0,5
        isExplosion = false;
        Destroy(ClonedEnemy);
        yield return new WaitForSecondsRealtime(3.75f); // 3,75
        StartCoroutine(EnemySpawn());
    }
}
