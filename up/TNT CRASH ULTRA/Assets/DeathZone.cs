using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Diamond") || other.CompareTag("Chest") || other.CompareTag("TNT"))
        {
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            MoveAndJump.KillPlayer();
        }
    }
}
