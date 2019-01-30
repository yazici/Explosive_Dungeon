using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_One : MonoBehaviour {
    bool first = true;
    private void Start()
    {
        Animator a = this.gameObject.GetComponent<Animator>();
        a.Play("Explose", 0);
        StartCoroutine(Destroyer());
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && !MoveAndJump.Instance.Invisible && !MoveAndJump.Instance.Died && first) { MoveAndJump.Instance.StartCoroutine(MoveAndJump.Instance.KillPlayer()); first = false; }
    }
    IEnumerator Destroyer() {
        AudioManager.Instance.SoundPlay(6);
        CameraShake.Should_Shake = true;
        yield return new WaitForSeconds(0.413f);
        Destroy(this.gameObject);
    }
}
