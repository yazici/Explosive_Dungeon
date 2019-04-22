using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//==============================
// Hello! It is the script for one lonely crab, like me.
// He dont have family, but he have very good friends.
// Please, dont say me about 'govnokod', I know. At this time I 'try' to learn OOP. 25.03.2019
//==============================
public class Crab_One : MonoBehaviour
{
    
    private bool MoveDirection; // true - налево; false - направо.
    private void Start()
    {
        Invoke("KillCrab", 5f);
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
        int c = Random.Range(0,5);
        if (c == 1){
            CameraShake.Should_Shake = true;
        }
        if(MoveDirection)
            gameObject.transform.Translate(new Vector2( Random.Range(-0.04f, -0.16f), 0f));
        else
            gameObject.transform.Translate(new Vector2( Random.Range(0.04f, 0.16f), 0));
    }
}
