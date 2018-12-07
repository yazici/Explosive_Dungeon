﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MoveAndJump : MonoBehaviour {
    [Header("Player")] // Игрок
    public Transform Player; // Координаты игрока
    public static bool Died; // Мертв ли игрок?
    public static bool Invisible; // Невидимость
    public static GameObject Shake;
    public Effects_Mechanics Effects_Mechanics;
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
    
    public static GameObject[] canvases;

    private float init_MoveForce;
    private void Start()
    {
        Shake = GameObject.Find("Main Camera");
        canvases = GameObject.FindGameObjectsWithTag("Canvases");
        init_MoveForce = MoveForce;
    }
    public static void KillPlayer()
    {
        Time.timeScale = 0;
        Shake.GetComponent<CameraShake>().enabled = false;
        Died = true;
        canvases[0].GetComponent<Canvas>().enabled = false;
        canvases[1].GetComponent<Canvas>().enabled = true;
    }
    public static void Respawn()
    {
        Time.timeScale = 1;
        Shake.GetComponent<CameraShake>().enabled = true;
        Died = false;
        Invisible = false;
        speedboost = false;
        DiamondSpawn.doubleDiamonds = false;
        DiamondSpawn.CurrentValueOfDiamonds = 0;
        canvases[0].GetComponent<Canvas>().enabled = true;
        canvases[1].GetComponent<Canvas>().enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

   
    public void Update() // Каждый кадр (Зависит от FPS)
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }
        if (Input.GetAxisRaw("Horizontal") > 0) { Flip(false); } else { Flip(true); }
            Move((int)Input.GetAxisRaw("Horizontal"));
#endif
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
        Effects_Mechanics.ChangeStateOfEffectIcon(Effects_Mechanics.Effects.Speed, true);
        yield return new WaitForSeconds(15f);
        speedboost = false;
        Effects_Mechanics.ChangeStateOfEffectIcon(Effects_Mechanics.Effects.Speed, false);
    }
    IEnumerator SetInvisible() { Effects_Mechanics.ChangeStateOfEffectIcon(Effects_Mechanics.Effects.Armor, true); this.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255,255,255,255/2);  yield return new WaitForSeconds(15f); Invisible = false; this.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255); Effects_Mechanics.ChangeStateOfEffectIcon(Effects_Mechanics.Effects.Armor, false); }
}
