using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchTrigger : MonoBehaviour {
    public bool isOnTrigger;
    private AudioSource explose_wav;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isOnTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isOnTrigger = false;
        }
    }
    private void Start()
    {
        explose_wav = GameObject.Find("StandartExplosion").GetComponent<AudioSource>();
        StartCoroutine(glitchSpawned());
    }
    IEnumerator glitchSpawned()
    {
        Animator a = this.transform.parent.gameObject.GetComponent<Animator>();
        a.SetInteger("State", 0);
        yield return new WaitForSeconds(Random.Range(3.0f, 6.0f));
        a.SetInteger("State", 1);
        explose_wav.Play();
        if (isOnTrigger && !MoveAndJump.Invisible) { MoveAndJump.KillPlayer(); if (gameObject.transform.parent.gameObject != null) { Destroy(gameObject.transform.parent.gameObject); } }
        yield return new WaitForSecondsRealtime(0.413f);
        if (gameObject.transform.parent.gameObject != null) { Destroy(gameObject.transform.parent.gameObject); } 
    }
}
