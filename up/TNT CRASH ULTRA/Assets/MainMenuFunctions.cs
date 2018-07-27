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

	public void ButtonSound(){if(Sounds){ButtonSoundSource.Play ();}}

	public void ChangeCamera(int IdToActive){//Метод, меняющий камеру
		for(int i = 0; i < 4; i++){//Перечисление переменных с массива
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
