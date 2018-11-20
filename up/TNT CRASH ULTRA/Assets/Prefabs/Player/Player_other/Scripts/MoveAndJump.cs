using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndJump : MonoBehaviour {
    [Header("Player")] // Игрок
    public Transform Player; // Координаты игрока
    public static bool Died; // Мертв ли игрок?
    public static bool Invisible;
    

    [Header("Move")] // Ходьба
    private int MoveAxis; // Направление движения   
    private bool facingRight;
    public static bool speedboost;
    public float MoveForce; // Скорость

    [Header("Animations")] // Анимации
    public Animator PlayerMainAnimator; // Ссылка на аниматор

    [Header("Jump")] // Прыжок 
    public Transform GroundChecker; // Проверятор земли
    public LayerMask Ground; // Маска-слой земли
    private bool onGround; // Игрок на земле?
    public Rigidbody2D PlayerPhysics; // Физика игрока
    public float JumpForce = 0; // Сила прыжка

    public float init_MoveForce;
    private void Start()
    {
        init_MoveForce = MoveForce;
    }
    public static void KillPlayer()
    {
        
    }
    public void Update() // Каждый кадр (Зависит от FPS)
    {
        if (speedboost) { StartCoroutine(OffSpeedboost()); }
        if (Invisible) { StartCoroutine(SetInvisible()); }
        if (Died) { MoveForce = 0; } else if(!Died && speedboost) { MoveForce = 9; } else if (!Died && !speedboost) { MoveForce = 6; }
        onGround = Physics2D.OverlapCircle(GroundChecker.position, 0.1f, Ground); // Проверка на земле ли игрок
        PlayerPhysics.velocity = new Vector2(MoveForce * MoveAxis, PlayerPhysics.velocity.y); // Ходьба
        if (onGround && MoveAxis == 0 && !Died) { PlayerMainAnimator.SetInteger("State", 0); } // Анимация "Idle"
        if (MoveAxis == 1 && onGround && !Died || MoveAxis == -1 && onGround && !Died) { PlayerMainAnimator.SetInteger("State", 1); } // Анимация "Run"
        if (!onGround && !Died) { PlayerMainAnimator.SetInteger("State", 2); } // Анимация "Jump"
        if (Died) PlayerMainAnimator.SetInteger("State", 3); // Анимация "Die"
    }
    public void Move(int Axis) // Ходьба (Axis - направление)
    {
        if (!Died) // Если игрок жив
        {
            MoveAxis = Axis; // Получение направления
        }
    }
    public void Jump() // Прыжок
    {
        if (onGround && !Died) // Если на земле и игрок жив
        {
            PlayerPhysics.AddForce(transform.up * JumpForce, ForceMode2D.Impulse); // Прыжок
        }
    }
    public void Flip(bool right) // Повернуть персонажа
    {
        if (facingRight != right) // Если направление не равно новому направлению
        {
            if (!Died) // Если игрок жив
            {
                facingRight = right; // Сменить переменную направления
                Vector3 theScale = Player.localScale; // Текущее направление
                theScale.x = theScale.x * -1; // Смена текущего направления на новое
                Player.localScale = theScale; // Замена направления
            }
        }
    }
    IEnumerator OffSpeedboost()
    {
        yield return new WaitForSeconds(15f);
        speedboost = false;
    }
    IEnumerator SetInvisible() { this.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255,255,255,255/2);  yield return new WaitForSeconds(15f); Invisible = false; this.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255); }
}
