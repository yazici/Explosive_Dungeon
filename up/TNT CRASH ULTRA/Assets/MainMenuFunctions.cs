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
  //      LoadLanguageFull();
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
