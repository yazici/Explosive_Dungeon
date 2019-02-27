using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemScr : MonoBehaviour
{
    public static TotemScr Instance;
    public GameObject Totem;
    public SpriteRenderer TotemSpriteRenderer;
    public Sprite[] totem_sprite;
    public int TotemHealth = 2;
    
    void PlayerKill(){Player.Instance.StartCoroutine(Player.Instance.TryToKillPlayer());}
    void Start()
    {
        Instance = this;
    }
    public void DamageTotem()
    {
        TotemHealth--;
        if(TotemHealth == 1)
        {
            TotemSpriteRenderer.sprite = totem_sprite[1];
        }else if(TotemHealth == 0)
        {
            TotemSpriteRenderer.sprite = totem_sprite[2];
            PlayerKill();
        }
    }
    void Update()
    {
        
    }
}
