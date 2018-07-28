using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenuFunctions : MonoBehaviour {

	[Header("Канвасы и смена камеры")]
	public Canvas[] Canvases; //Массив всех канвасов из меню
	public Camera[] Cameras;//Массив всех камер из меню

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
    
	public void ButtonSound(){if(Sounds){ButtonSoundSource.Play ();}}
    public void StartButton() {StatsCounts[0]++; PlayerPrefs.SetInt("Games", StatsCounts[0]); Application.LoadLevel(1); }
    public void SetStats() {
        Stats[0].text = "Games: " + StatsCounts[0];
        Stats[1].text = "Diamonds collected: " + StatsCounts[1];
        Stats[2].text = "Best Score: " + StatsCounts[2];
        Stats[3].text = "Deaths: " + StatsCounts[3];
        Stats[4].text = "Classic Chests opened: " + StatsCounts[4];
        Stats[5].text = "Diamond Chest opened: " + StatsCounts[5];
        Stats[6].text = "Rainbow Chest opened: " + StatsCounts[6];
        Stats[7].text = "glitch chests opened: " + StatsCounts[7];
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
		}
		Cameras [IdToActive].enabled = true;//Камера активна
		Canvases [IdToActive].enabled = true;//Канвас активен
	}

	public void TwitterUp(){
		Application.OpenURL ("https://twitter.com/8ilverStudio");
	}

	public void ChangeSounds (){
		if (Sounds == true) {
			Sounds = false;
			ButtonSoundSource.Play ();
			SoundsText.text = "Sounds: off";

		} else {
			Sounds = true;
			ButtonSoundSource.Play ();
			SoundsText.text = "Sounds: on";
		}
	}

	public void ChangeMusic (){
		if (Music == true) {
			Music = false;
			MainMenuSource.Stop ();
			MusicText.text = "Music: off";

		} else {
			Music = true;
			MainMenuSource.Play ();
			MusicText.text = "Music: on";
		}
	}
}
