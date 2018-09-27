using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {
    public Text[] WordsToTranslate; // Тексты, которые перекладаем
    public string[] en_US; // Английский перевод
    public string[] ru_RU; // Русский перевод
    public string[] ua_UA; // Украинский перевод
    public Image[] Masks;
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
        Debug.Log("Установленный язык на устройстве " + Application.systemLanguage + ", текущий язык " + PlayerPrefs.GetString("Current_Language") + ", всего слов переведено " + en_US.Length +".");
    }
    public void ChangeLanguage()
    {
        for (int i = 0; i != WordsToTranslate.Length; i++) {
            switch (PlayerPrefs.GetString("Current_Language")) {
                case "en_US": WordsToTranslate[i].text = en_US[i]; break;
                case "ru_RU": WordsToTranslate[i].text = ru_RU[i]; break;
                case "ua_UA": WordsToTranslate[i].text = ua_UA[i]; break;
            }
        }
        switch (PlayerPrefs.GetString("Current_Language")) {
            case "en_US": for (int i = 0; i != Masks.Length; i++) { Masks[i].enabled = false; } ; break;
            case "ru_RU": Masks[0].enabled = true; Masks[1].enabled = true; Masks[2].enabled = false; Masks[3].enabled = true; break;
            case "ua_UA": Masks[0].enabled = false; Masks[1].enabled = false; Masks[2].enabled = true; Masks[3].enabled = true; break;
        }
    }
    public void ChangeLanguageButton() {
        switch (PlayerPrefs.GetString("Current_Language")) {
            case "en_US": PlayerPrefs.SetString("Current_Language", "ru_RU"); break;
            case "ru_RU": PlayerPrefs.SetString("Current_Language", "ua_UA"); break;
            case "ua_UA": PlayerPrefs.SetString("Current_Language", "en_US"); break;
        }
        ChangeLanguage(); Debug.Log("Установленный язык на устройстве " + Application.systemLanguage + ", текущий язык " + PlayerPrefs.GetString("Current_Language") + ", всего слов переведено " + en_US.Length + ".");
    }
}
