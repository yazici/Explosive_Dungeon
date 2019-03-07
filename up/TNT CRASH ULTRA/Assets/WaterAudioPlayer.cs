using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAudioPlayer : MonoBehaviour
{
    private AudioSource a;
    void Start()
    {
        a = gameObject.GetComponent<AudioSource>();
    }
    void Update()
    {
        if(PlayerPrefs.GetString("Audio") == "true" && !a.isPlaying)
            a.Play();
        if(PlayerPrefs.GetString("Audio") == "false" && a.isPlaying)
            a.Stop();
    }
}
