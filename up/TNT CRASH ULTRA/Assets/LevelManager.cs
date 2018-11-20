﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelManager : MonoBehaviour {
    public static int Diamonds = 100000;
    public Sprite[] Button_States; /* 0 {Заблокирован} / 1 {Не выбран} / 2 {Выбран};*/
    public Image[] Level_Buttons;
    private int level_index;
    public bool[] Buyed_Levels = { true, false, false };
    public GameObject[] WhenBuyed, NotBuyed;
    public int[] Levels_Prices = { 0, 200, 300 };
    public void Start()
    {
        ChangeActiveLevel(0);
    }
    public void StartButton() {
        switch (level_index)
        {
            case 0:  break;
            case 1:  break;
            case 2:  break;
        }
    }
    public void ChangeActiveLevel(int indexOfCurrentLevel) // Сменить уровень
    {
        for (int i = 0; i < Level_Buttons.Length; i++) // Перечисление всех уровней в качестве i
        {
            if (i == indexOfCurrentLevel) // Если уровень выбран
            {
                if (Buyed_Levels[i]) // Если уровень выбран и куплен
                {
                    /* Выбор уровня (Смена скина на активный) */
                    level_index = indexOfCurrentLevel;
                    Level_Buttons[i].sprite = Button_States[2];
                    Debug.Log("Уровень номер" + indexOfCurrentLevel + 1 +" выбран!");
                }
                else if (!Buyed_Levels[i]) // Если уровень выбран и не куплен
                {
                    /* Покупка уровня и запуск реверсивного метода */
                    if (Levels_Prices[i] <= Diamonds) // Алмазов больше или равно чем цена
                    {
                        Debug.Log("Попытка купить уровен успешна.");
                        Buyed_Levels[i] = true;
                        ChangeActiveLevel(i);
                        WhenBuyed[i].SetActive(true);
                        NotBuyed[i].SetActive(false);
                    }
                    else if (Levels_Prices[i] > Diamonds) // Недостаточное количество алмазов
                    {
                        Debug.Log("Уровень не куплен. Ошибка: " + "Недостаточное количество даймондов.");
                    }
                }
            }
            else if (i != indexOfCurrentLevel) // Если уровень не выбран
            {
                if (Buyed_Levels[i] && Level_Buttons[i] != null)
                {
                    /* Сделать скин неактивного */
                    Level_Buttons[i].sprite = Button_States[1];
                    WhenBuyed[i].SetActive(true);
                    NotBuyed[i].SetActive(false);
                }
                else if (!Buyed_Levels[i] && Level_Buttons[i] != null)
                {
                    /* Сделать скин не купленого */
                    Level_Buttons[i].sprite = Button_States[0];
                    WhenBuyed[i].SetActive(false);
                    NotBuyed[i].SetActive(true);
                }
            }
        }
    }
}
