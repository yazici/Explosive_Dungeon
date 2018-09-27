using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelManager : MonoBehaviour {
    public Sprite[] Button_States; /* 0 {Заблокирован} / 1 {Не выбран} / 2 {Выбран};*/
    public Image[] Buttons;
    private int level_index;
    public bool[] Buyed_Levels = { true, true, false };
    public void ChangeLevelIndex(int index) {
        if (Buyed_Levels[index])
        {
            for (int i = 0; i < Buttons.Length; i++)
            {
                if (i != index)
                {
                    Buttons[i].sprite = Button_States[1];
                }
            }
            level_index = index;
            Buttons[index].sprite = Button_States[2];
            Debug.Log("Кнопка с индексом " + index + " уcпешно включена, все остальные были выключены (Номер по счету = index + 1)");
        }
        else { Debug.LogWarning("Уровень с индексом " + index + " не куплен. (Номер по счету = index + 1)"); }
    }
}
