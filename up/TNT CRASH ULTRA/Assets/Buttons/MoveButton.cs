using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MoveButton : MonoBehaviour
{
       public Sprite[] button_sprites;
       public Image[] buttons;
       public void ChangeStateOfButton(int bs) // 11 12 21 22
       {
              switch(bs)
              {
                     case 11: buttons[0].sprite = button_sprites[0]; break;
                     case 12: buttons[0].sprite = button_sprites[1]; break;
                     case 21: buttons[1].sprite = button_sprites[2]; break;
                     case 22: buttons[1].sprite = button_sprites[3]; break;
              }
       }
}


