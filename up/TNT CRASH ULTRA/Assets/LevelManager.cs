using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour {
    public Save sv = new Save();
    public int Diamonds = 0;
    public Text DiamondsCounter;

    public Sprite[] Button_States; /* 0 {Заблокирован} / 1 {Не выбран} / 2 {Выбран};*/
    public Image[] Level_Buttons;
    private int level_index;
   // public bool[] Buyed_Levels = { true, false, false };
    public GameObject[] WhenBuyed, NotBuyed;
    public int[] Levels_Prices = { 0, 250, 500 };
    public void Start()
    {
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
            Diamonds = PlayerPrefs.GetInt("DiamondsCount");
        }
        else
        {
            PlayerPrefs.SetInt("DiamondsCount", 0);
        }
        ChangeActiveLevel(0);
    }
    public void StartButton() {
        switch (level_index)
        {
            case 0: SceneManager.LoadScene("Level1"); break;
            case 1: SceneManager.LoadScene("Level2"); break;
            case 2: SceneManager.LoadScene("Level3"); break;
        }
    }
    public void ChangeActiveLevel(int indexOfCurrentLevel) // Сменить уровень
    {
        if (sv.Buyed_Levels[indexOfCurrentLevel] || !sv.Buyed_Levels[indexOfCurrentLevel] && Levels_Prices[indexOfCurrentLevel] <= Diamonds)
        {
            for (int i = 0; i < Level_Buttons.Length; i++) // Перечисление всех уровней в качестве i
            {
                if (i == indexOfCurrentLevel) // Если уровень выбран
                {
                    if (sv.Buyed_Levels[i]) // Если уровень выбран и куплен
                    {
                        /* Выбор уровня (Смена скина на активный) */
                        level_index = indexOfCurrentLevel;
                        Level_Buttons[i].sprite = Button_States[2];
                        Debug.Log("Уровень номер" + indexOfCurrentLevel + 1 + " выбран!");
                    }
                    else if (!sv.Buyed_Levels[i]) // Если уровень выбран и не куплен
                    {
                        /* Покупка уровня и запуск реверсивного метода */
                        if (Levels_Prices[i] <= Diamonds) // Алмазов больше или равно чем цена
                        {
                            Debug.Log("Попытка купить уровен успешна.");
                            sv.Buyed_Levels[i] = true;
                            ChangeActiveLevel(i);
                            WhenBuyed[i].SetActive(true);
                            NotBuyed[i].SetActive(false);
                            Diamonds = Diamonds - Levels_Prices[i];
                            PlayerPrefs.SetInt("DiamondsCount", Diamonds);
                            SaveAllData();
                        }
                        else if (Levels_Prices[i] > Diamonds) // Недостаточное количество алмазов
                        {
                            Debug.Log("Уровень не куплен. Ошибка: " + "Недостаточное количество даймондов.");
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
       sv = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("Save"));
    }
}


[System.Serializable]
public class Save
{
    public bool[] Buyed_Levels = { true, false, false};
}
