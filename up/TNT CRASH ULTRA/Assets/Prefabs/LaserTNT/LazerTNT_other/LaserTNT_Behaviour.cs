using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTNT_Behaviour : MonoBehaviour
{
    public GameObject TNT_prefab;
    public GameObject Spawner;
    private GameObject TNT_Cloned;
    private bool inExplosion;
    private Animator TNT_Cloned_Anim;
    private AudioSource explosion_wav;
    private void Start()
    {
        StartCoroutine(TNT_SPAWN());
        explosion_wav = GameObject.Find("LaserExplosion").GetComponent<AudioSource>();
    }
    public void Update()
    {
        if (inExplosion) { if (LaserTNT_Checker.isPlayerOnTriger() && !MoveAndJump.Invisible) { MoveAndJump.KillPlayer(); } }
    }
    public IEnumerator TNT_SPAWN()
    {
        inExplosion = false;
        yield return new WaitForSeconds(Random.Range(7.5f, 15.0f));
        Spawner.transform.position = new Vector3(Random.Range(-8.0f, 8.0f), Spawner.transform.position.y, Spawner.transform.position.z);
        TNT_Cloned = Instantiate(TNT_prefab, Spawner.transform.position, Quaternion.identity);
        TNT_Cloned_Anim = TNT_Cloned.GetComponent<Animator>();
        TNT_Cloned_Anim.SetInteger("State", 0);
        yield return new WaitForSecondsRealtime(7);
        inExplosion = true;
        explosion_wav.Play();
        CameraShake.Should_Shake = true;
        TNT_Cloned_Anim.SetInteger("State", 1);
        yield return new WaitForSecondsRealtime(0.5f);
        Destroy(TNT_Cloned);
        inExplosion = false;
        StartCoroutine(TNT_SPAWN());
    }
}
