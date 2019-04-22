using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
public class Player : MonoBehaviour {
    private int ad_dieds;

    public SaveRecord svRec = new SaveRecord();
    [HideInInspector]public static Player Instance;
    private Image record_notification;
    [Header("Player")] // Игрок
    public Transform Player_Transform; // Координаты игрока
    public bool Died; // Мертв ли игрок?
    public bool Invisible; // Невидимость
    public GameObject Shake;
    public Effects_Mechanics Effects_Mechanics;
    public GameObject BlickImage;
    public float BlickLenght;
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
    private int LevelID;

    public GameObject[] canvases;
    private GameObject unPauseCounter;
    private GameObject[] onLevelButtons;
    private void Start()
    {

        switch(SceneManager.GetActiveScene().name){
            case "Level1": LevelID = 0; break;
            case "Level2": LevelID = 1; break;
            case "Level3": LevelID = 2; break;
        }
        if(!PlayerPrefs.HasKey("SavedRecord")) 
            SaveAllData();
        LoadAllData();
        Invoke("SetPause", 1.0f);
        if(LevelID == 0)
            Achievements.AchievementSave.FirstCaveVisited = true;
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
    public IEnumerator TryToKillPlayer(bool fast_die = false)
    {
        if(LevelID == 0)
            Social.ReportScore(DiamondSpawn.CurrentValueOfDiamonds, "CgkIyriH1Y4ZEAIQDQ", (bool success) => {print("Reported Score Status: " + success);});
        else if(LevelID == 1)
            Social.ReportScore(DiamondSpawn.CurrentValueOfDiamonds, "CgkIyriH1Y4ZEAIQDg", (bool success) => {print("Reported Score Status: " + success);});
        else if(LevelID == 2)
            Social.ReportScore(DiamondSpawn.CurrentValueOfDiamonds, "CgkIyriH1Y4ZEAIQDw", (bool success) => {print("Reported Score Status: " + success);});
        
        if(LevelID == 2)
            Achievements.AchievementSave.DiedInThirdCave++;
        StartCoroutine(Blick());
        Achievements.AchievementSave.ClearOneGameAchievements();
        PlayerPrefs.SetInt("DiamondsCount", PlayerPrefs.GetInt("DiamondsCount") + DiamondSpawn.CurrentValueOfDiamonds);
        Died = true;
        if (svRec.LevelsRecords[LevelID] <= DiamondSpawn.CurrentValueOfDiamonds)
        {
            svRec.LevelsRecords[LevelID] = DiamondSpawn.CurrentValueOfDiamonds;
            GameObject.Find("BestScore_Value").GetComponent<Text>().text = "" + DiamondSpawn.CurrentValueOfDiamonds;
            GameObject.Find("YourScore_Value").GetComponent<Text>().text = "" + DiamondSpawn.CurrentValueOfDiamonds;
            record_notification.enabled = true;
        }else
        {
            GameObject.Find("BestScore_Value").GetComponent<Text>().text = "" + svRec.LevelsRecords[LevelID];
            GameObject.Find("YourScore_Value").GetComponent<Text>().text = "" + DiamondSpawn.CurrentValueOfDiamonds;
            record_notification.enabled = false;
        }

        if (!fast_die)
        {
            PlayerMainAnimator.Play("Player_die", 0);
            yield return new WaitForSeconds(2f);
            Destroy(Player_Transform.gameObject.GetComponent<SpriteRenderer>());
            yield return new WaitForSeconds(1f);
            
        }else{Destroy(Player_Transform.gameObject.GetComponent<SpriteRenderer>()); yield return new WaitForSeconds(1.5f);}
        Time.timeScale = 0;
        Shake.GetComponent<CameraShake>().enabled = false;
        canvases[0].GetComponent<Canvas>().enabled = false;
        canvases[1].GetComponent<Canvas>().enabled = true;
        canvases[2].GetComponent<Canvas>().enabled = false;
        AdvertisimentShow.Instance.ShowAd();
        SaveAllData();
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
        unPauseCounter.GetComponent<TextMeshProUGUI>().text = "3";
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
        unPauseCounter.GetComponent<TextMeshProUGUI>().text = "2";
        yield return new WaitForSecondsRealtime(0.667f);
        unPauseCounter.GetComponent<TextMeshProUGUI>().text = "1";
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
    public IEnumerator Blick()
    {
        BlickImage.SetActive(true);
        yield return new WaitForSeconds(BlickLenght);
        BlickImage.SetActive(false);
    }
    public void RespawnPlayer() { StartCoroutine(Respawn()); Time.timeScale = 1; }
    public IEnumerator Respawn()
    {
        Time.timeScale = 1;
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

   
    public void FixedUpdate() // Каждый кадр (Зависит от FPS)
    {
        if (Died) { MoveForce = 0; } else if(!Died && speedboost) { MoveForce = 8; } else if (!Died && !speedboost) { MoveForce = 6; }
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
            Achievements.AchievementSave.JumpedTimesInOneGame++;
            if(this.Invisible){Achievements.AchievementSave.JumpedTimesInOneGameUnderArmorEffect++;}
        }
    }
    public void Flip(bool right) // Повернуть персонажа
    {
        if (facingRight != right) // Если направление не равно новому направлению
        {
            if (!Died) // Если игрок жив
            {
                facingRight = right; // Сменить переменную направления
                Vector3 theScale = Player_Transform.localScale; // Текущее направление
                theScale.x = theScale.x * -1; // Смена текущего направления на новое
                Player_Transform.localScale = theScale; // Замена направления
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
        yield return new WaitForSeconds(11f); this.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        for(int i = 0; i!=4; i++){
        yield return new WaitForSeconds(0.5f); this.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255,255,255,255/2);
        yield return new WaitForSeconds(0.5f); this.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        }
        Invisible = false;
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        Effects_Mechanics.ChangeStateOfEffectIcon(Effects_Mechanics.Effects.Armor, false);
        Invisible = false;
    }
    public void SaveAllData()
    {
        PlayerPrefs.SetString("SavedRecord", JsonUtility.ToJson(svRec));
    }
    public void LoadAllData()
    {       
        svRec = JsonUtility.FromJson<SaveRecord>(PlayerPrefs.GetString("SavedRecord"));
        
    }
}

public class SaveRecord 
{
    public int[] LevelsRecords = { 0, 0, 0 };
}