using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelUI : MonoBehaviour {
    public GameObject[] Windowses;
    public Text DiamondScore;
    public Text[] toTranslate;
    private AudioSource[] Audio;
    private AudioSource Music;
    public void Update()
    {
        DiamondScore.text = "" + DiamondSpawn.CurrentValueOfDiamonds;
    }
    public void Start()
    {

        //Audio = GameObject.FindGameObjectsWithTag("AudioSources");
        Music = GameObject.Find("MusicSource").GetComponent<AudioSource>();
        if (!PlayerPrefs.HasKey("Audio") || !PlayerPrefs.HasKey("Audio"))
            PlayerPrefs.SetString("Audio", "true"); PlayerPrefs.SetString("Music", "true");
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
    public void ExitFromPause()
    {
        if (!Player.Instance.Died) { PlayerPrefs.SetInt("DiamondsCount", PlayerPrefs.GetInt("DiamondsCount") + DiamondSpawn.CurrentValueOfDiamonds); }
        DiamondSpawn.CurrentValueOfDiamonds = 0;
        DiamondSpawn.doubleDiamonds = false;
        TimeScale(1);
        SceneManager.LoadScene(2);
    }
    public void ExitFromDeath()
    {
        DiamondSpawn.CurrentValueOfDiamonds = 0;
        DiamondSpawn.doubleDiamonds = false;
        TimeScale(1);
        SceneManager.LoadScene(2);
    }
}
