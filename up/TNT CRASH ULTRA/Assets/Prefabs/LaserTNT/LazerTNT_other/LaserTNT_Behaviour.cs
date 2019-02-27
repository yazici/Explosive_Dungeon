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
    bool first = true;
    public void Update()
    {
        if (DiamondSpawn.CurrentValueOfDiamonds >= 10 && first) { first = false; StartCoroutine(TNT_SPAWN()); }
        if (inExplosion) { if (LaserTNT_Checker.isPlayerOnTriger() && !Player.Instance.Invisible && !Player.Instance.Died) { Player.Instance.StartCoroutine(Player.Instance.TryToKillPlayer()); } }
    }
    public IEnumerator TNT_SPAWN()
    {
            inExplosion = false;
            yield return new WaitForSeconds(Random.Range(7.5f, 15.0f));
            Spawner.transform.position = new Vector3(Random.Range(-8.0f, 8.0f), Spawner.transform.position.y, Spawner.transform.position.z);
            TNT_Cloned = Instantiate(TNT_prefab, Spawner.transform.position, Quaternion.identity);
            TNT_Cloned_Anim = TNT_Cloned.GetComponent<Animator>();
            TNT_Cloned_Anim.SetInteger("State", 0);
            yield return new WaitForSeconds(7);
            AudioManager.Instance.SoundPlay(7);
            inExplosion = true;
            CameraShake.Should_Shake = true;
            TNT_Cloned_Anim.SetInteger("State", 1);
            yield return new WaitForSeconds(0.5f);
            Destroy(TNT_Cloned);
            inExplosion = false;
            StartCoroutine(TNT_SPAWN());
        
       
    }
}
