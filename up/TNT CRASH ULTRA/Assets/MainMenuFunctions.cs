using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenuFunctions : MonoBehaviour {
    public Text[] Backs;
    public Text[] Translate;
    [Header("Канвасы и смена камеры")]
    public Camera[] Cameras;
    public Canvas[] Canvases;
	[Header("Настройки")]
	public AudioSource MainMenuSource;
	public AudioSource ButtonSoundSource;
	public Text MusicText;
	public Text SoundsText;
	public bool Music = true;
	public bool Sounds = true;

    [Header("Статистика")]
    public Text[] Stats;
    public int[] StatsCounts;
    public void Awake()
    {
        LoadLanguageFull();
    }
    public void LoadLanguageFull() {
        for (int i = 0; i != 31; i++) Translate[i].text = ChangeLanguage.lng.Word[i];
        for (int i = 0; i != Backs.Length; i++) Backs[i].text = ChangeLanguage.lng.Word[16];
    }
    public void ButtonSound(){if(Sounds){ButtonSoundSource.Play ();}}
    public void StartButton() {StatsCounts[0]++; PlayerPrefs.SetInt("Games", StatsCounts[0]); Application.LoadLevel(1); }
    public void SetStats() {
        Stats[0].text = ChangeLanguage.lng.Word[18] + StatsCounts[0];
        Stats[1].text = ChangeLanguage.lng.Word[19] + StatsCounts[1];
        Stats[2].text = ChangeLanguage.lng.Word[20] + StatsCounts[2];
        Stats[3].text = ChangeLanguage.lng.Word[21] + StatsCounts[3];
        Stats[4].text = ChangeLanguage.lng.Word[22] + StatsCounts[4];
        Stats[5].text = ChangeLanguage.lng.Word[23] + StatsCounts[5];
        Stats[6].text = ChangeLanguage.lng.Word[24] + StatsCounts[6];
        Stats[7].text = ChangeLanguage.lng.Word[25] + StatsCounts[7];
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey("Games")) { StatsCounts[0] = PlayerPrefs.GetInt("Games"); }
    }
    public void ChangeCamera(int IdToActive){//Метод, меняющий камеру
		for(int i = 0; i < 5; i++){//Перечисление переменных с массива
			if (i != IdToActive){//Все переменные массива кроме одной
                Cameras[i].enabled = false;
                Canvases[i].enabled = false;
            }
            Cameras[IdToActive].enabled = true;
            Canvases[IdToActive].enabled = true;
        }
        
	}

	public void TwitterUp(){
		Application.OpenURL ("https://twitter.com/8ilver_");
	}

	public void ChangeSounds (){
		if (Sounds == true) {
			Sounds = false;
			ButtonSoundSource.Play ();
            switch (PlayerPrefs.GetString("Language"))
            {
                case "en_US": SoundsText.text = "SOUNDS: OFF"; break;
                case "ru_RU": SoundsText.text = "ЗВУКИ: OFF"; break;
                case "ua_UA": SoundsText.text = "ЗВУКИ: OFF"; break;
            }

		} else {
			Sounds = true;
			ButtonSoundSource.Play ();
            switch (PlayerPrefs.GetString("Language"))
            {
                case "en_US": SoundsText.text = "SOUNDS: ON"; break;
                case "ru_RU": SoundsText.text = "ЗВУКИ: ON"; break;
                case "ua_UA": SoundsText.text = "ЗВУКИ: ON"; break;
            }
        }
	}
    public void Update()
    {
        if (Sounds) {
            switch (PlayerPrefs.GetString("Language"))
            {
                case "en_US": SoundsText.text = "SOUNDS: ON"; break;
                case "ru_RU": SoundsText.text = "ЗВУКИ: ON"; break;
                case "ua_UA": SoundsText.text = "ЗВУКИ: ON"; break;
            }
        } else {
            switch (PlayerPrefs.GetString("Language"))
            {
                case "en_US": SoundsText.text = "SOUNDS: OFF"; break;
                case "ru_RU": SoundsText.text = "ЗВУКИ: OFF"; break;
                case "ua_UA": SoundsText.text = "ЗВУКИ: OFF"; break;
            }
        }
        if (Music)
        {
            switch (PlayerPrefs.GetString("Language"))
            {
                case "en_US": MusicText.text = "MUSIC: ON"; break;
                case "ru_RU": MusicText.text = "МУЗЫКА: ON"; break;
                case "ua_UA": MusicText.text = "МУЗИКА: ON"; break;
            }
        }
        else
        {
            switch (PlayerPrefs.GetString("Language"))
            {
                case "en_US": MusicText.text = "MUSIC: OFF"; break;
                case "ru_RU": MusicText.text = "МУЗЫКА: OFF"; break;
                case "ua_UA": MusicText.text = "МУЗИКА: OFF"; break;
            }
        }
    }
    public void ChangeMusic (){
		if (Music == true) {
			Music = false;
			MainMenuSource.Stop ();
            switch (PlayerPrefs.GetString("Language"))
            {
                case "en_US": MusicText.text = "MUSIC: OFF"; break;
                case "ru_RU": MusicText.text = "МУЗЫКА: OFF"; break;
                case "ua_UA": MusicText.text = "МУЗИКА: OFF"; break;
            }

        } else {
			Music = true;
			MainMenuSource.Play ();
            switch (PlayerPrefs.GetString("Language"))
            {
                case "en_US": MusicText.text = "MUSIC: ON"; break;
                case "ru_RU": MusicText.text = "МУЗЫКА: ДА"; break;
                case "ua_UA": MusicText.text = "МУЗИКА: ТАК"; break;
            }
        }
	}
}
