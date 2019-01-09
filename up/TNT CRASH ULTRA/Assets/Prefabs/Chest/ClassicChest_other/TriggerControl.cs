using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerControl : MonoBehaviour {
    public enum Chest { Classic, Diamond, Rainbow, Glitch }
    private AudioSource diamond_wav;
    public Chest typeOfThisChest;
    private void Start()
    {
        diamond_wav = GameObject.Find("Audio_Diamond").GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            diamond_wav.Play();
            switch (typeOfThisChest)
            {
                case Chest.Diamond:
                    DiamondSpawn.Instance.StartCoroutine("SetDoubleDiamonds");
                    Debug.Log("Вы подобрали алмазный сундук!");
                    break;
                case Chest.Rainbow:
                    MoveAndJump.Instance.StartCoroutine("SetInvisible");
                    Debug.Log("Вы подобрали радужный сундук!");
                    break;
                case Chest.Glitch:
                    Glitch_Main.Instance.SpawnGlitchTNTs();
                    Debug.Log("Вы подобрали глитч-сундук!");
                    break;
                default: // Chest.Classic
                    Debug.Log("Вы подобрали классический сундук!");
                    MoveAndJump.Instance.StartCoroutine("OffSpeedboost");
                    break;
            }
            if (gameObject.transform.parent.gameObject != null) { Destroy(gameObject.transform.parent.gameObject); }
        }
    }
}
