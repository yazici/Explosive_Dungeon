using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerControl : MonoBehaviour {
    public enum Chest { Classic, Diamond, Rainbow, Glitch }
    
    public Chest typeOfThisChest;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.Instance.SoundPlay(8);
            switch (typeOfThisChest)
            {
                case Chest.Diamond:
                if(ClassicChest.DiamondCoroutine != null)
                        ClassicChest.Instance.StopCoroutine(ClassicChest.DiamondCoroutine);
                    ClassicChest.DiamondCoroutine = DiamondSpawn.Instance.StartCoroutine("SetDoubleDiamonds");
                    Debug.Log("Вы подобрали алмазный сундук!");
                    Achievements.AchievementSave.DiamondChestsOpened++;
                    break;
                case Chest.Rainbow:
                    if(ClassicChest.InvisibleCoroutine != null)
                        ClassicChest.Instance.StopCoroutine(ClassicChest.InvisibleCoroutine);
                    ClassicChest.InvisibleCoroutine = Player.Instance.StartCoroutine("SetInvisible");
                    Debug.Log("Вы подобрали радужный сундук!");
                    break;
                case Chest.Glitch:
                    Glitch_Main.Instance.SpawnGlitchTNTs();
                    Achievements.AchievementSave.GlitchChestsOpened++;
                    Debug.Log("Вы подобрали глитч-сундук!");
                    break;
                default: // Chest.Classic
                if(ClassicChest.SpeedBoostCoroutine != null){
                    ClassicChest.Instance.StopCoroutine(ClassicChest.SpeedBoostCoroutine);
                    }
                    Debug.Log("Вы подобрали классический сундук!");
                    ClassicChest.SpeedBoostCoroutine = Player.Instance.StartCoroutine("OffSpeedboost");
                    break;
            }
            if(Player.Instance.Invisible){ Achievements.AchievementSave.OpenedChestUnderInvisibility++;}
            if (gameObject.transform.parent.gameObject != null) { Destroy(gameObject.transform.parent.gameObject); }
        }
    }
}
