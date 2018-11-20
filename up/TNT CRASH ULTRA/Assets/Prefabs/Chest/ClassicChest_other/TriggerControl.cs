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
            Debug.Log("Вы подобрали сундук!");
            switch (typeOfThisChest)
            {
                case Chest.Diamond:
                    DiamondSpawn.doubleDiamonds = true;

                    break;
                case Chest.Rainbow:
                    MoveAndJump.Invisible = true;

                    break;
                case Chest.Glitch:
                    GlitchTNTBehaviour.spawntnt = true;

                    break;
                default: // Chest.Classic
                    MoveAndJump.speedboost = true;
                    break;
            }
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
