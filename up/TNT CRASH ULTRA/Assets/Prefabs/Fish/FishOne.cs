using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishOne : MonoBehaviour
{
    public Rigidbody2D FishPhysics;
    void Start()
    {
        gameObject.transform.Rotate(0,0,90);
        FishPhysics = gameObject.GetComponent<Rigidbody2D>();
        Vector2 force = new Vector2(0f, 20f);
        FishPhysics.AddForce(force, ForceMode2D.Impulse);
        gameObject.GetComponent<SpriteRenderer>().flipY = gameObject.transform.position.x > 0 ? true : false;
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.name == "Player"){
            Player.Instance.StartCoroutine(Player.Instance.TryToKillPlayer());
        }
    }
}
