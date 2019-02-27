using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//==============================
// Hello! It is the script for one lonely crab, like me.
// He dont have family, but he have very good friends.
// Please, dont say me about 'govnokod', I know. At this time I try to learn OOP.
//==============================
public class Crab_One : MonoBehaviour
{
    private bool MoveDirection; // true - налево; false - направо.
    private void Start()
    {
        Invoke("KillCrab", 10f);
        if(gameObject.transform.position.x > 0)
            MoveDirection = true;
        else
            MoveDirection = false;
        if(MoveDirection)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    void KillCrab(){Destroy(gameObject);}
    private void FixedUpdate()
    {
        if(MoveDirection)
            gameObject.transform.Translate(new Vector2( Random.Range(-0.04f, -0.16f), 0f));
        else
            gameObject.transform.Translate(new Vector2( Random.Range(0.04f, 0.16f), 0));
    }
}
