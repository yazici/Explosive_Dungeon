using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishOne : MonoBehaviour
{
    public Rigidbody2D FishPhysics;
    void Start()
    {
        FishPhysics = gameObject.GetComponent<Rigidbody2D>();
        Vector2 force = new Vector2(0f, 20f);
        FishPhysics.AddForce(force, ForceMode2D.Impulse);
        gameObject.GetComponent<SpriteRenderer>().flipX = gameObject.transform.position.x > 0 ? true : false;
    }
    private void Update() {
        if(gameObject.transform.position.y > 0.14f)
            gameObject.GetComponent<SpriteRenderer>().flipY = true;
    }
    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.name == "Player" 
            && !Player.Instance.Invisible 
                && !Player.Instance.Died){
            Player.Instance.StartCoroutine(Player.Instance.TryToKillPlayer());
                Achievements.AchievementSave.DiedByFish++;
        }
    }
}
