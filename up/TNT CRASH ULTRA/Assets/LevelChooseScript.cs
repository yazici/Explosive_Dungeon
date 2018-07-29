using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelChooseScript : MonoBehaviour {

    public int curIndex;
    public bool[] buyedLevels;
    public int[] pricesLevels;
    public Sprite[] BoxState;
    public Image[] LevelsImages;
    public int Diamonds = 1500;
    public Text DiamondsText;
    public GameObject[] SecondLevel;
    public GameObject[] ThirdLevel;
    public void StartButton()
    {
        switch (curIndex)
        {
            case 0: Application.LoadLevel(1); break;
            case 1: Application.LoadLevel(2); break;
            case 2: Application.LoadLevel(3); break;
        }
    }
    private void LevelBuy(int indexBuy) {
        buyedLevels[indexBuy] = true;
        Diamonds = Diamonds - pricesLevels[indexBuy];
    }
    public void Update()
    {
        DiamondsText.text = "" + Diamonds;
        if (buyedLevels[1])
        {
            SecondLevel[0].SetActive(false);
            SecondLevel[1].SetActive(true);
        }
        if (buyedLevels[2]) {
            ThirdLevel[0].SetActive(false);
            ThirdLevel[1].SetActive(true);
        }
    }
    public void OnLevelChecked(int index) {
        if (buyedLevels[index]) {
            LevelsImages[index].sprite = BoxState[1];
            curIndex = index;
            for (int i = 0; i < LevelsImages.Length; i++)
            {
                if(i != index)
                {
                    LevelsImages[i].sprite = BoxState[0];
                }
            }
        }
        else
        {
            if (pricesLevels[index] <= Diamonds) { LevelBuy(index); }
        }
    }
}
