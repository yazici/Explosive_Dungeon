using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour {
    public Save sv = new Save();

    public TextMeshProUGUI Descryption_Text;
    public string[] Level_Descryption; 
    private UnityAction Desc_Lvl;
    public int Diamonds = 0;
    public TextMeshProUGUI DiamondsCounter;
    public GameObject[] LevelRecords;
    public Sprite[] Button_States; /* 0 {Заблокирован} / 1 {Не выбран} / 2 {Выбран};*/
    public Image[] Level_Buttons;
    private int level_index;
   // public bool[] Buyed_Levels = { true, false, false };
    public GameObject[] WhenBuyed, NotBuyed;
    public int[] Levels_Prices = { 0, 250, 500 };
    void Start()
    {
        print("nawJL?");
        LevelRecords = GameObject.FindGameObjectsWithTag("LevelRecords");
        if (PlayerPrefs.HasKey("Save"))
        {
            LoadAllData();
        }
        else
        {
            SaveAllData();
        }
        if (PlayerPrefs.HasKey("DiamondsCount"))
        {
            DiamondsCounter.text = "" + PlayerPrefs.GetInt("DiamondsCount");
            //Diamonds = PlayerPrefs.GetInt("DiamondsCount");
        }
        else
        {
            PlayerPrefs.SetInt("DiamondsCount", 0);
        }
        ChangeActiveLevel(0);
        NewTranslation_Control.TranslateAll.Subscribe(this.TrDc);
    }
    private void Awake()
    {
        LoadAllData();
    }
    public void StartButton() {
        switch (level_index)
        {
            case 0: SceneManager.LoadScene("Level1"); break;
            case 1: SceneManager.LoadScene("Level2"); break;
            case 2: SceneManager.LoadScene("Level3"); break;
        }
    }
   // public string GetTranslate() 
   // {
//
    //}
    public void TrDc()
    {
        TranslateDesc(level_index);
    }
    public void TranslateDesc(int indexOfCurLevel)
    {
        string returned = null;
        if (PlayerPrefs.HasKey("CurrentLanguage"))
        {
            if(indexOfCurLevel == 0){
                    switch(PlayerPrefs.GetString("CurrentLanguage")){
                    case "UA": returned = "ЗБИРАЙТЕ АЛМАЗИ ТА ТIКАЙТЕ ВIД МОНСТРIВ I ВИБУХIВ"; break;
                    case "RU": returned = "СОБИРАЙТЕ АЛМАЗЫ И УБЕГАЙТЕ ОТ МОНСТРОВ И ВЗРЫВОВ"; break;
                    case "EN": returned = "COLLECT DIAMONDS AND RUN AWAY FROM MONSTERS AND EXPLOSIONS"; break;
                }
            }
            if(indexOfCurLevel == 1){
                    switch(PlayerPrefs.GetString("CurrentLanguage")){
                    case "UA": returned = "ОСТЕРIГАЙТЕСЬ РIЧКОВИХ МОНСТРIВ!"; break;
                    case "RU": returned = "ОСТЕРЕГАЙТЕСЬ РЕЧНЫХ МОНСТРОВ!"; break;
                    case "EN": returned = "BEWARE OF RIVER MONSTERS!"; break;
                }
            }
            if(indexOfCurLevel == 2){
                    switch(PlayerPrefs.GetString("CurrentLanguage")){
                    case "UA": returned = "ЗАХИЩАЙТЕ ТОТЕМ ВIД МОНСТРIВ."; break;
                    case "RU": returned = "ЗАЩИЩАЙТЕ ТОТЕМ ОТ МОНСТРОВ."; break;
                    case "EN": returned = "PROTECT TOTEM FROM MONSTERS."; break;
                }
            }
        }
        Descryption_Text.SetText(returned);
    }
    public void ChangeActiveLevel(int indexOfCurrentLevel) // Сменить уровень
    {
        if (sv.Buyed_Levels[indexOfCurrentLevel] || !sv.Buyed_Levels[indexOfCurrentLevel] && Levels_Prices[indexOfCurrentLevel] <= PlayerPrefs.GetInt("DiamondsCount"))
        {
            for (int i = 0; i < Level_Buttons.Length; i++) // Перечисление всех уровней в качестве i
            {
                if (i == indexOfCurrentLevel) // Если уровень выбран
                {
                    if (sv.Buyed_Levels[i]) // Если уровень выбран и куплен
                    {
                        
                        level_index = indexOfCurrentLevel;
                        /* Выбор уровня (Смена скина на активный) */
                        TrDc();
                        Level_Buttons[i].sprite = Button_States[2];
                        Debug.Log("Уровень номер" + indexOfCurrentLevel + 1 + " выбран!");
                    }
                    else if (!sv.Buyed_Levels[i]) // Если уровень выбран и не куплен
                    {
                        /* Покупка уровня и запуск реверсивного метода */
                        if (Levels_Prices[i] <= PlayerPrefs.GetInt("DiamondsCount")) // Алмазов больше или равно чем цена
                        {
                            Debug.Log("Попытка купить уровен успешна.");
                            sv.Buyed_Levels[i] = true;
                            ChangeActiveLevel(i);
                            WhenBuyed[i].SetActive(true);
                            NotBuyed[i].SetActive(false);
                            foreach (GameObject g in LevelRecords){ g.GetComponent<LevelRecordInMenu>().UpdateRecords();}
                            //Diamonds = Diamonds - Levels_Prices[i];
                            PlayerPrefs.SetInt("DiamondsCount", PlayerPrefs.GetInt("DiamondsCount") - Levels_Prices[i]);
                            SaveAllData();
                        }
                        else if (Levels_Prices[i] > PlayerPrefs.GetInt("DiamondsCount")) // Недостаточное количество алмазов
                        {
                            Debug.Log("Уровень не куплен. Ошибка: " + "Недостаточное количество алмазов.");
                        }
                    }
                }
                else if (i != indexOfCurrentLevel) // Если уровень не выбран
                {
                    if (sv.Buyed_Levels[i] && Level_Buttons[i] != null)
                    {
                        /* Сделать скин неактивного */
                        Level_Buttons[i].sprite = Button_States[1];
                        WhenBuyed[i].SetActive(true);
                        NotBuyed[i].SetActive(false);
                    }
                    else if (!sv.Buyed_Levels[i] && Level_Buttons[i] != null)
                    {
                        /* Сделать скин не купленого */
                        Level_Buttons[i].sprite = Button_States[0];
                        WhenBuyed[i].SetActive(false);
                        NotBuyed[i].SetActive(true);
                    }
                }
            }
        }
        DiamondsCounter.text = "" + PlayerPrefs.GetInt("DiamondsCount");
    }
    public void SaveAllData()
    {
        PlayerPrefs.SetString("Save", JsonUtility.ToJson(sv));
    }
    public void LoadAllData()
    {
        if(PlayerPrefs.HasKey("Save")){
       sv = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("Save"));
    }
    
}
}


[System.Serializable]
public class Save
{
    public bool[] Buyed_Levels = { true, false, false };
}
