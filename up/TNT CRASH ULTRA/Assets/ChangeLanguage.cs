using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class ChangeLanguage : MonoBehaviour
{
    public Text[] Backs;
    public Text[] Translate;
    private string json;
    public static lang lng = new lang();
    private int langIndex = 1;
    private string[] langArray = { "ru_RU", "en_US","ua_UA"  };

    void Awake()
    {
        if (!PlayerPrefs.HasKey("Language"))
        {
            if (Application.systemLanguage == SystemLanguage.Russian || Application.systemLanguage == SystemLanguage.Belarusian)
                PlayerPrefs.SetString("Language", "ru_RU");
            else if (Application.systemLanguage == SystemLanguage.Ukrainian) { PlayerPrefs.SetString("Language", "ua_UA"); }
            else PlayerPrefs.SetString("Language", "en_US");
        }
        LangLoad();
    }
   /* void Start()
    {
        for (int i = 0; i < langArray.Length; i++)
        {
            if (PlayerPrefs.GetString("Language") == langArray[i])
            {
                langIndex = i + 1;
                break;
            }
        }
  } */ 

    void LangLoad()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        string path = Path.Combine(Application.streamingAssetsPath, "Language/" + PlayerPrefs.GetString("Language") + ".json");
        WWW reader = new WWW(path);
        while (!reader.isDone) { }
        json = reader.text;
#else
        json = File.ReadAllText(Application.streamingAssetsPath + "/Language/" + PlayerPrefs.GetString("Language") + ".json");
#endif
        lng = JsonUtility.FromJson<lang>(json);
    }
    public void LoadLanguageFull()
    {
        for (int i = 0; i != 31; i++) Translate[i].text = ChangeLanguage.lng.Word[i];
        for (int i = 0; i != Backs.Length; i++) Backs[i].text = ChangeLanguage.lng.Word[16];
    }
    public void switchButton()
    {
        switch (PlayerPrefs.GetString("Language")) {
            case "en_US": PlayerPrefs.SetString("Language", "ru_RU"); break;
            case "ru_RU": PlayerPrefs.SetString("Language", "ua_UA"); break;
            case "ua_UA": PlayerPrefs.SetString("Language", "en_US"); break;
        }
        //if (langIndex != langArray.Length) langIndex++;
        //else langIndex = 1;
        Debug.Log(PlayerPrefs.GetString("Language"));
        LangLoad();
        LoadLanguageFull();
    }

}
public class lang
{
    public string[] Word = new string[39];
}