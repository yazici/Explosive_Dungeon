using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NewTranslation_Control : MonoBehaviour
{
    public static MyEvents TranslateAll = new MyEvents();
    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene s, LoadSceneMode m)
    {
        TranslateAll = new MyEvents();
    }
    public void ChangeLanguageButton()
    {
        switch (PlayerPrefs.GetString("CurrentLanguage"))
        {
            case "EN": PlayerPrefs.SetString("CurrentLanguage", "RU");  break;
            case "RU": PlayerPrefs.SetString("CurrentLanguage", "UA"); break;
            case "UA": PlayerPrefs.SetString("CurrentLanguage", "EN"); break;
        }
        TranslateAll.Publish();
    }
}
public class MyEvents
{
    private readonly List<Action> _callbacks = new List<Action>();
    public void Subscribe(Action callback)
    {
        _callbacks.Add(callback);
    }
    public void Publish()
    {
        foreach (Action callback in _callbacks)
            callback();
    }
}
