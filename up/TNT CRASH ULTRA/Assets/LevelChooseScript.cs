using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;
public class LevelChooseScript : MonoBehaviour {

    private Save sv = new Save();
    private string path;
    public int curIndex;
    public int[] pricesLevels;
    public Sprite[] BoxState;
    public Image[] LevelsImages;
    //public int Diamonds = 1500;
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
    private void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        path = Path.Combine(Application.persistentDataPath, "Save.json");
#else
        path = Path.Combine(Application.dataPath, "Save.json");
#endif
        if (File.Exists(path))
        {
            sv = JsonUtility.FromJson<Save>(File.ReadAllText(path));
        }
    }
    private void LevelBuy(int indexBuy) {
        sv.buyedLevels[indexBuy] = true;
        sv.Diamonds = sv.Diamonds - pricesLevels[indexBuy];
    }
    private void OnApplicationQuit()
    {
        File.WriteAllText(path, JsonUtility.ToJson(sv));
    }
    public void Update()
    {
        DiamondsText.text = "" + sv.Diamonds;
        if (sv.buyedLevels[1])
        {
            SecondLevel[0].SetActive(false);
            SecondLevel[1].SetActive(true);
        }
        if (sv.buyedLevels[2]) {
            ThirdLevel[0].SetActive(false);
            ThirdLevel[1].SetActive(true);
        }
    }
    public void OnLevelChecked(int index) {
        if (sv.buyedLevels[index]) {
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
            if (pricesLevels[index] <= sv.Diamonds) { LevelBuy(index); }
        }
    }
}
[Serializable]
public class Save{
    public bool[] buyedLevels = { true, false, false };
    public int Diamonds = 1590;
   }