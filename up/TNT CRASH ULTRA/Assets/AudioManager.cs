using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public bool Audio = true, Music = true;
    public AudioClip[] SoundsBlips;
    private void Start()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    public void SoundPlay(int bl)
    {
        if (Audio)
        {            
            GameObject.Find("AudioSource").GetComponent<AudioSource>().PlayOneShot(SoundsBlips[bl-1]);
        }
    }
    public void InitializeSources()
    {
        if (Music)
            GameObject.Find("MusicSource").GetComponent<AudioSource>().Play();
        else
            GameObject.Find("MusicSource").GetComponent<AudioSource>().Stop();
        
    }
    public void ChangeAudio()
    {
        GameObject.Find("AudioSource").GetComponent<AudioSource>().PlayOneShot(SoundsBlips[1]);
        if (Audio)
            Audio = false;
        else
            Audio = true;
    }
    public void ChangeMusic()
    {
        SoundPlay(2);
        if (Music)
        {
            Music = false;
            GameObject.Find("MusicSource").GetComponent<AudioSource>().Stop();
        }
        else
        {
            Music = true;
            GameObject.Find("MusicSource").GetComponent<AudioSource>().Play();
        }
    }
}
