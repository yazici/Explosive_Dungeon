using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT_Behaviour : MonoBehaviour {
    public GameObject TNT_prefab;
    public GameObject Spawner;
    private GameObject TNT_Cloned;
    private Animator TNT_Cloned_Anim;
    private AudioSource explosion_wav;
    private void Start()
    {
        StartCoroutine(TNT_SPAWN());
        explosion_wav = GameObject.Find("StandartExplosion").GetComponent<AudioSource>();
    }
    public IEnumerator TNT_SPAWN() {
        yield return new WaitForSeconds(Random.Range(2.0f, 6.0f));
        Spawner.transform.position = new Vector3(Random.Range(-8.0f, 8.0f), Spawner.transform.position.y, Spawner.transform.position.z);
        TNT_Cloned = Instantiate(TNT_prefab, Spawner.transform.position, Quaternion.identity);
        TNT_Cloned_Anim = TNT_Cloned.GetComponent<Animator>();
        TNT_Cloned_Anim.SetInteger("State", 0);
        yield return new WaitForSeconds(5);
        TNT_Cloned_Anim.SetInteger("State", 1);
        explosion_wav.Play();
        CameraShake.Should_Shake = true;
        if (TNT_Trigger.isPlayerOnTriger() && !MoveAndJump.Invisible) { MoveAndJump.Died = true; }
        yield return new WaitForSeconds(0.413f);
        Destroy(TNT_Cloned);
        
        StartCoroutine(TNT_SPAWN());
        //babax kill
    }
}
