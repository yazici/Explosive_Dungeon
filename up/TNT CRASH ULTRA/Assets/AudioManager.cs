using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public SpriteState[] MusicState;
    public SpriteState[] AudioState;
    public Button[] MusicButtons;
    //public bool Audio = true, Music = true;
    public AudioClip[] SoundsBlips;
    private void Awake()
    {
        if(!PlayerPrefs.HasKey("Music") || !PlayerPrefs.HasKey("Audio")){
            PlayerPrefs.SetString("Music", "true");
            PlayerPrefs.SetString("Audio", "true");
        }
        
        Debug.Log(PlayerPrefs.GetString("Music"));
        Instance = this;
        MusicButtons[1].spriteState = AudioState[PlayerPrefs.GetString("Audio") == "true" ? 0 : 1];
        MusicButtons[1].gameObject.GetComponent<Image>().sprite = AudioState[PlayerPrefs.GetString("Audio") == "true" ? 0 : 1].highlightedSprite;
        MusicButtons[0].spriteState = MusicState[PlayerPrefs.GetString("Music") == "true" ? 0 : 1];
        MusicButtons[0].gameObject.GetComponent<Image>().sprite = MusicState[PlayerPrefs.GetString("Music") == "true" ? 0 : 1].highlightedSprite;
        InitializeSources();
    }
    public void SoundPlay(int bl)
    {
        if (PlayerPrefs.GetString("Audio") == "true")
        {            
            GameObject.Find("AudioSource").GetComponent<AudioSource>().PlayOneShot(SoundsBlips[bl-1]);
        }
    }
    public void InitializeSources()
    {
        if (PlayerPrefs.GetString("Music") == "true")
            GameObject.Find("MusicSource").GetComponent<AudioSource>().Play();
        else
            GameObject.Find("MusicSource").GetComponent<AudioSource>().Stop();
        
        
    }
    public void ChangeAudio()
    {
        GameObject.Find("AudioSource").GetComponent<AudioSource>().PlayOneShot(SoundsBlips[1]);
        if (PlayerPrefs.GetString("Audio") == "true")
            PlayerPrefs.SetString("Audio", "false");
        else
            PlayerPrefs.SetString("Audio", "true");
        MusicButtons[1].spriteState = AudioState[PlayerPrefs.GetString("Audio") == "true" ? 0 : 1];
        MusicButtons[1].gameObject.GetComponent<Image>().sprite = AudioState[PlayerPrefs.GetString("Audio") == "true" ? 0 : 1].highlightedSprite;
    }
    public void ChangeMusic()
    {
        SoundPlay(2);
        if (PlayerPrefs.GetString("Music") == "true")
        {
            PlayerPrefs.SetString("Music", "false");
            GameObject.Find("MusicSource").GetComponent<AudioSource>().Play();
            GameObject.Find("MusicSource").GetComponent<AudioSource>().volume = 0;
        }
        else
        {
            PlayerPrefs.SetString("Music", "true");
            GameObject.Find("MusicSource").GetComponent<AudioSource>().Play();
            GameObject.Find("MusicSource").GetComponent<AudioSource>().volume = 0.5f;
        }
        MusicButtons[0].spriteState = MusicState[PlayerPrefs.GetString("Music") == "true" ? 0 : 1];
        MusicButtons[0].gameObject.GetComponent<Image>().sprite = MusicState[PlayerPrefs.GetString("Music") == "true" ? 0 : 1].highlightedSprite;
        Debug.Log(PlayerPrefs.GetString("Music"));
    }
}
