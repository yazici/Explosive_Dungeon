﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public enum typeOfThis
{
    Image,
    Text
}
public class NewTranslations : MonoBehaviour {
    private GameObject operatingWith;
    private string CurrentLanguage;
    
    [SerializeField]private typeOfThis thisObjectType;
    [SerializeField]private Sprite[] SpritesForButtons_UA;
    [SerializeField]private Sprite[] SpritesForButtons_RU;
    [SerializeField]private Sprite[] SpritesForButtons_EN;
    [SerializeField]private string[] StringsForTexts;
   
    void Start ()
    {
        //SceneManager.sceneLoaded += OnSceneLoaded;
        operatingWith = this.gameObject;
        Translate_Update();
        NewTranslation_Control.TranslateAll.Subscribe(this.Translate_Update);
	}
    //void OnSceneLoaded(Scene s, LoadSceneMode m)
    //{
    ///    NewTranslation_Control.TranslateAll.Subscribe(this.Translate_Update);
    //    Debug.Log("stop shto");
    //}
    private void Translate_Update()
    {
        
        UpdateCurrentLanguage();
        InitializeObject();
    }
    private void UpdateCurrentLanguage()
    {
        if (PlayerPrefs.HasKey("CurrentLanguage"))
        {
            CurrentLanguage = PlayerPrefs.GetString("CurrentLanguage");
            Debug.Log("Данный ключ (CurrentLanguage) уже был в системе!");
        }
        else
        {
            if(Application.systemLanguage == SystemLanguage.Russian 
                || Application.systemLanguage == SystemLanguage.Belarusian)
            {
                PlayerPrefs.SetString("CurrentLanguage", "RU");
            }
            else if (Application.systemLanguage == SystemLanguage.Ukrainian)
            {
                PlayerPrefs.SetString("CurrentLanguage", "UA");
            }
            else
            {
                PlayerPrefs.SetString("CurrentLanguage", "EN");
            }
            Debug.Log("Данный ключ (CurrentLanguage) не существует в системе!" + " Он был автоматически создан и назначен! Новое значение - " + PlayerPrefs.GetString("CurrentLanguage"));
        }
    }
	void InitializeObject()
    {
        if (thisObjectType == typeOfThis.Text)
        {
            switch (PlayerPrefs.GetString("CurrentLanguage"))
            {
                
                case "EN": this.gameObject.GetComponent<TextMeshProUGUI>().SetText(StringsForTexts[0]); break;
                case "RU": this.gameObject.GetComponent<TextMeshProUGUI>().SetText(StringsForTexts[1]); break;
                case "UA": this.gameObject.GetComponent<TextMeshProUGUI>().SetText(StringsForTexts[2]); break;
                default: Debug.Log("Ошибка инициализации перевода! Code: 666."); break;
            }
        }
        else if(thisObjectType == typeOfThis.Image)
        {
            operatingWith.GetComponent<Image>().sprite = GenerateStandartSprite();
            operatingWith.GetComponent<Button>().spriteState = GenerateSpriteStates();
        }
        else
        {
            Debug.Log("Ошибка инициализации перевода! Code: 15.");
        }
	}
    Sprite GenerateStandartSprite()
    {
        Sprite returned = null;
        switch (PlayerPrefs.GetString("CurrentLanguage"))
        {
            case "UA":
                returned = SpritesForButtons_UA[0];
                break;
            case "RU":
                returned = SpritesForButtons_RU[0];
                break;
            case "EN":
                returned = SpritesForButtons_EN[0];
                break;
        }
        return returned;
    }
    SpriteState GenerateSpriteStates()
    {
        SpriteState returned;
        switch (PlayerPrefs.GetString("CurrentLanguage"))
        {
            case "UA":
                returned.highlightedSprite = SpritesForButtons_UA[0];
                returned.pressedSprite = SpritesForButtons_UA[1];
                break;
            case "RU":
                returned.highlightedSprite = SpritesForButtons_RU[0];
                returned.pressedSprite = SpritesForButtons_RU[1];
                break;
            case "EN":
                returned.highlightedSprite = SpritesForButtons_EN[0];
                returned.pressedSprite = SpritesForButtons_EN[1];
                break;
        }
        return returned;
    }
}