using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabKill : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.name == "Player"  && !Player.Instance.Invisible && !Player.Instance.Died)
            Player.Instance.StartCoroutine(Player.Instance.TryToKillPlayer()); 
    }
}
