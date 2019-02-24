using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum Levels { Level1, Level2, Level3 } 
public class LevelRecordInMenu : MonoBehaviour
{
    public static LevelRecordInMenu Instance;
    private SaveRecord_D svRecrd = new SaveRecord_D();
    public Levels ButtonID;
    public Image diamond;
    
    string bscore;
    int LevelID;
    bool thisBuyed;
    public LevelManager lm;
    public void UpdateRecords()
    {
        print(gameObject.name);
        diamond = transform.GetComponentInChildren<Image>();
        switch (ButtonID)
        {
            case Levels.Level1:
                LevelID = 0;
                break;
            case Levels.Level2:
                LevelID = 1;
                break;
            case Levels.Level3:
                LevelID = 2;
                break;
            
        }
        if(lm.sv.Buyed_Levels[LevelID]){
            if(PlayerPrefs.HasKey("SavedRecord")){
                Debug.Log(PlayerPrefs.GetString("SavedRecord"));
                svRecrd = JsonUtility.FromJson<SaveRecord_D>(PlayerPrefs.GetString("SavedRecord"));
                gameObject.GetComponent<TextMeshProUGUI>().text = bscore + svRecrd.LevelsRecords[LevelID];
                gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0,-200,0); 
                diamond.enabled = false;
        }else if(LevelID == 0){
            gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0,-200,0); 
            gameObject.GetComponent<TextMeshProUGUI>().text = bscore + "0";
        }else{
                diamond.enabled = true;
                gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0,-155,0);
            }
        }
    }
    void Start()
    {
        InitializeString();
        UpdateRecords();
        Instance = this;
        NewTranslation_Control.TranslateAll.Subscribe(this.InitializeString);
    }
    private void InitializeString()
    {
        if (PlayerPrefs.HasKey("CurrentLanguage"))
        {
            switch(PlayerPrefs.GetString("CurrentLanguage")){
                case "UA": bscore = "НАЙКРАЩИЙ РАХУНОК: \n"; break;
                case "RU": bscore = "ЛУЧШИЙ СЧЕТ: \n"; break;
                case "EN": bscore = "BEST SCORE: \n"; break;
            }
            UpdateRecords();
        }
    }
}
public class SaveRecord_D 
{
    public int[] LevelsRecords = { 0, 0, 0 };
}
