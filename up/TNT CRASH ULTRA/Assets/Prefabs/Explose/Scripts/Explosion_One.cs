using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_One : MonoBehaviour {
    private AudioSource explosion_wav;
    private void Start()
    {
        explosion_wav = GameObject.Find("StandartExplosion").GetComponent<AudioSource>();
        Animator a = this.gameObject.GetComponent<Animator>();
        a.Play("Explose", 0);
        StartCoroutine(Destroyer());
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && !MoveAndJump.Instance.Invisible) { MoveAndJump.Instance.StartCoroutine("KillPlayer"); }
    }
    IEnumerator Destroyer() {
        explosion_wav.Play();
        CameraShake.Should_Shake = true;
        yield return new WaitForSeconds(0.413f);
        Destroy(this.gameObject);
    }
}
