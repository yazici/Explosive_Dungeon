using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeTrigger : MonoBehaviour {
    public static bool PlayerOnTrigger;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player") { PlayerOnTrigger = true; Debug.Log("Player в коллайдере глаза." + this.name); }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player") { PlayerOnTrigger = false; Debug.Log("Player  не в коллайдере глаза." + this.name); }
    }
    public static bool isPlayerOnTriger() { return PlayerOnTrigger;}
}
