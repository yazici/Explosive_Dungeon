using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelUI : MonoBehaviour {
    public GameObject[] Windowses;
    public Text DiamondScore;
    public Text[] toTranslate;
    public void Update()
    {
        DiamondScore.text = "" + DiamondSpawn.CurrentValueOfDiamonds;
    }
    public void Start()
    {
        for (int i = 0; i != toTranslate.Length; i++) {
           // toTranslate[i].text = ChangeLanguage.lng.Word[i + 31];
        }
    }
    public void ChangeWindow(int Window) {
            for (int i = 0; i < Windowses.Length; i++) {
                if (i != Window) {
                    Windowses[i].SetActive(false);
                }
            }
        Windowses[Window].SetActive(true);
    }
    public void TimeScale(int Scale) {Time.timeScale = Scale;}
    public void Exit()
    {
        TimeScale(1);
        SceneManager.LoadScene(0);
    }
}
