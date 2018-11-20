using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {
    public Text[] WordsToTranslate; // Тексты, которые перекладаем
    public string[] Localizations_EN = new string[] { "Tap To Play", "Credits", "Game Design", "Vlad Pavlenko", "Programming", "Vlad Gramma", "Music", "Settings" };
    public string[] Localizations_RU = new string[] { "Нажмите для начала", "Создатели", "Дизайнер", "Влад Павленко", "Программист", "Влад Грамма", "Музыка", "Настройки" };
    public string[] Localizations_UA = new string[] { "Натисніть для початку", "Творці", "Дизайнер", "Влад Павленко", "Програмування", "Влад Грамма", "Музика", "Налаштування" };
    
    public Image[] Masks;
    public AudioSource MusicListener, AudioListener;
    public bool Music, Audio = true;
    private void Start()
    {
        if (!PlayerPrefs.HasKey("Current_Language"))
        {
            if (Application.systemLanguage == SystemLanguage.Russian || Application.systemLanguage == SystemLanguage.Belarusian || Application.systemLanguage == SystemLanguage.Ukrainian)
            {
                switch (Application.systemLanguage)
                {
                    case SystemLanguage.Russian: PlayerPrefs.SetString("Current_Language", "ru_RU"); break;
                    case SystemLanguage.Belarusian: PlayerPrefs.SetString("Current_Language", "ru_RU"); break;
                    case SystemLanguage.Ukrainian: PlayerPrefs.SetString("Current_Language", "ua_UA"); break;
                }
            } else {
                 PlayerPrefs.SetString("Current_Language", "en_US");
            }
        }
        ChangeLanguage();

    }
    public void MusicAndAudio(string NameButton)
    {
        switch (NameButton)
        {
            case "Audio": if (Audio) { Audio = false; AudioListener.Play(); } else { Audio = true; AudioListener.Play(); } Debug.Log("Вы сменили переменную Audio. Новое значение -> " + Audio); break;
            case "Music": if (Music) { Music = false; MusicListener.Stop(); } else { Music = true; MusicListener.Play(); } Debug.Log("Вы сменили переменную Music. Новое значение -> " + Music); break;
        }
    }
    public void ChangeLanguage()
    {
        for (int i = 0; i != WordsToTranslate.Length; i++) {
            switch (PlayerPrefs.GetString("Current_Language")) {
                case "en_US": WordsToTranslate[i].text = Localizations_EN[i].ToUpper(); PlayerPrefs.SetString("Current_Language", "ru_RU"); break;
                case "ru_RU": WordsToTranslate[i].text = Localizations_RU[i].ToUpper(); PlayerPrefs.SetString("Current_Language", "ua_UA"); break;
                case "ua_UA": WordsToTranslate[i].text = Localizations_UA[i].ToUpper(); PlayerPrefs.SetString("Current_Language", "en_US"); break;
            }
        }
        /*switch (PlayerPrefs.GetString("Current_Language")) {
            case "en_US": for (int i = 0; i != Masks.Length; i++) { Masks[i].enabled = false; } ; break;
            case "ru_RU": Masks[0].enabled = true; Masks[1].enabled = true; Masks[2].enabled = false; Masks[3].enabled = true; break;
            case "ua_UA": Masks[0].enabled = false; Masks[1].enabled = false; Masks[2].enabled = true; Masks[3].enabled = true; break;
        }*/
    }
    public void OnButtonTapped() { if (Audio) { AudioListener.Play(); } }
}