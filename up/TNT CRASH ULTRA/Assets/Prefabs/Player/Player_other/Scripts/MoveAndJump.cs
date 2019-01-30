using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class MoveAndJump : MonoBehaviour {
    public static MoveAndJump Instance;
    private Image record_notification;
    [Header("Player")] // Игрок
    public Transform Player; // Координаты игрока
    public bool Died; // Мертв ли игрок?
    public bool Invisible; // Невидимость
    public GameObject Shake;
    public Effects_Mechanics Effects_Mechanics;
    [Header("Move")] // Ходьба
    private int MoveAxis; // Направление движения   
    private bool facingRight;
    public bool speedboost;
    public float MoveForce; // Скорость

    [Header("Animations")] // Анимации
    public Animator PlayerMainAnimator; // Ссылка на аниматор

    [Header("Jump")] // Прыжок 
    public Transform GroundChecker; // Проверятор земли
    public LayerMask Ground; // Маска-слой земли
    private bool onGround; // Игрок на земле?
    public Rigidbody2D PlayerPhysics; // Физика игрока
    public float JumpForce = 0; // Сила прыжка

    bool pause;


    public GameObject[] canvases;
    private GameObject unPauseCounter;
    private GameObject[] onLevelButtons;
    //private float init_MoveForce;
    private void Start()
    {
        Invoke("SetPause", 2.0f);
        AudioManager.Instance.InitializeSources();
        record_notification = GameObject.Find("Record_Notifications").GetComponent<Image>();
        unPauseCounter = GameObject.Find("Exit_From_Pause");
        unPauseCounter.SetActive(false);
        onLevelButtons = GameObject.FindGameObjectsWithTag("onLevelButtons");
        Instance = this;
        Shake = GameObject.Find("Main Camera");;
       // init_MoveForce = MoveForce;
    }
    void SetPause() { pause = true; }
    public IEnumerator KillPlayer(bool fast_die=false)
    {
        PlayerPrefs.SetInt("DiamondsCount", PlayerPrefs.GetInt("DiamondsCount") + DiamondSpawn.CurrentValueOfDiamonds);
        Died = true;
        if (!PlayerPrefs.HasKey("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", DiamondSpawn.CurrentValueOfDiamonds);
            GameObject.Find("BestScore_Value").GetComponent<Text>().text = "" + DiamondSpawn.CurrentValueOfDiamonds;
            GameObject.Find("YourScore_Value").GetComponent<Text>().text = "" + DiamondSpawn.CurrentValueOfDiamonds;
            record_notification.enabled = true;
            print("1");
        }
        else if (PlayerPrefs.GetInt("BestScore") <= DiamondSpawn.CurrentValueOfDiamonds)
        {
            PlayerPrefs.SetInt("BestScore", DiamondSpawn.CurrentValueOfDiamonds);
            GameObject.Find("BestScore_Value").GetComponent<Text>().text = "" + DiamondSpawn.CurrentValueOfDiamonds;
            GameObject.Find("YourScore_Value").GetComponent<Text>().text = "" + DiamondSpawn.CurrentValueOfDiamonds;
            record_notification.enabled = true;
            print("2");
        }
        else
        {
            GameObject.Find("BestScore_Value").GetComponent<Text>().text = "" + PlayerPrefs.GetInt("BestScore");
            GameObject.Find("YourScore_Value").GetComponent<Text>().text = "" + DiamondSpawn.CurrentValueOfDiamonds;
            record_notification.enabled = false;
            print("3");
        }
        if (!fast_die)
        {
            PlayerMainAnimator.Play("Player_die", 0);
            yield return new WaitForSeconds(2f);
            Destroy(Player.gameObject.GetComponent<SpriteRenderer>());
            yield return new WaitForSeconds(1f);
            Time.timeScale = 0;
        }
        Shake.GetComponent<CameraShake>().enabled = false;
        canvases[0].GetComponent<Canvas>().enabled = false;
        canvases[1].GetComponent<Canvas>().enabled = true;
        canvases[2].GetComponent<Canvas>().enabled = false;
    }
    public void Pause()
    {
        if (pause && !Died)
        {
            GameObject.Find("YourScore_Value_Pause").GetComponent<Text>().text = "" + DiamondSpawn.CurrentValueOfDiamonds;
            Time.timeScale = 0;
            canvases[0].GetComponent<Canvas>().enabled = false;
            canvases[1].GetComponent<Canvas>().enabled = false;
            canvases[2].GetComponent<Canvas>().enabled = true;
        }
    }
    public void unPause()
    {
        StartCoroutine(unPauseAction());
    }
    IEnumerator unPauseAction()
    {
        unPauseCounter.SetActive(true);
        unPauseCounter.GetComponent<Text>().text = "3";
        canvases[0].GetComponent<Canvas>().enabled = true;
        canvases[1].GetComponent<Canvas>().enabled = false;
        canvases[2].GetComponent<Canvas>().enabled = false;
        foreach (GameObject Buttons in onLevelButtons)
        {
            Buttons.GetComponent<Button>().enabled = false;
            if(Buttons.GetComponent<EventTrigger>() != null)
                Buttons.GetComponent<EventTrigger>().enabled = false;
        }
        yield return new WaitForSecondsRealtime(0.667f);
        unPauseCounter.GetComponent<Text>().text = "2";
        yield return new WaitForSecondsRealtime(0.667f);
        unPauseCounter.GetComponent<Text>().text = "1";
        yield return new WaitForSecondsRealtime(0.667f);
        unPauseCounter.SetActive(false);
        foreach (GameObject Buttons in onLevelButtons)
        {
            Buttons.GetComponent<Button>().enabled = true;
            if (Buttons.GetComponent<EventTrigger>() != null)
                Buttons.GetComponent<EventTrigger>().enabled = true;
        }
        Time.timeScale = 1;
    }
    public void StartRespawning() { StartCoroutine(Respawn()); Time.timeScale = 1; }
    public IEnumerator Respawn()
    {
        record_notification.enabled = false;
        DiamondSpawn.CurrentValueOfDiamonds = 0;
        DiamondSpawn.doubleDiamonds = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        yield return new WaitForSecondsRealtime(0.2f);
        Died = false;
        Invisible = false;
        speedboost = false;
        DiamondSpawn.doubleDiamonds = false;
        DiamondSpawn.CurrentValueOfDiamonds = 0;
        canvases[0].GetComponent<Canvas>().enabled = true;
        canvases[1].GetComponent<Canvas>().enabled = false;
        
    }

   
    public void Update() // Каждый кадр (Зависит от FPS)
    {
        if (Died) { MoveForce = 0; } else if(!Died && speedboost) { MoveForce = 9; } else if (!Died && !speedboost) { MoveForce = 6; }
        onGround = Physics2D.OverlapCircle(GroundChecker.position, 0.1f, Ground); // Проверка на земле ли игрок
        PlayerPhysics.velocity = new Vector2(MoveForce * MoveAxis, PlayerPhysics.velocity.y); // Ходьба
        if (onGround && MoveAxis == 0 && !Died) { PlayerMainAnimator.SetInteger("State", 0); } // Анимация "Idle"
        if (MoveAxis == 1 && onGround && !Died || MoveAxis == -1 && onGround && !Died) { PlayerMainAnimator.SetInteger("State", 1); } // Анимация "Run"
        if (!onGround && !Died) { PlayerMainAnimator.SetInteger("State", 2); } // Анимация "Jump"
        //if (Died) PlayerMainAnimator.SetInteger("State", 3); // Анимация "Die"
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
        speedboost = true;
        Effects_Mechanics.ChangeStateOfEffectIcon(Effects_Mechanics.Effects.Speed, true);
        yield return new WaitForSeconds(15f);
        speedboost = false;
        Effects_Mechanics.ChangeStateOfEffectIcon(Effects_Mechanics.Effects.Speed, false);
    }
    IEnumerator SetInvisible() {
        Invisible = true;
        Effects_Mechanics.ChangeStateOfEffectIcon(Effects_Mechanics.Effects.Armor, true);
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255,255,255,255/2);
        yield return new WaitForSeconds(15f); Invisible = false;
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        Effects_Mechanics.ChangeStateOfEffectIcon(Effects_Mechanics.Effects.Armor, false);
        Invisible = false;
    }
}
