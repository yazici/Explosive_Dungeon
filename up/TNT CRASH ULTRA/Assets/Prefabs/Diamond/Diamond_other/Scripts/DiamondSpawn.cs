﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DiamondSpawn : MonoBehaviour {
    public Effects_Mechanics Effects_Mechanics;
    public static DiamondSpawn Instance;
    private static GameObject DiamondCloned;
    public GameObject DiamondPrefab;
    
    public Transform DiamondSpawner;
    public static int CurrentValueOfDiamonds;
    public static bool doubleDiamonds;
    private void Start()
    {
        Instance = this;
        
    }
    
    private void Update()
    {
        if (DiamondCloned == null)
                {
            DiamondSpawner.position = new Vector2(Random.Range(-8.0f, 8.0f), DiamondSpawner.position.y);
            DiamondCloned = Instantiate(DiamondPrefab, DiamondSpawner.transform.position, Quaternion.identity) as GameObject;
        Debug.Log(CurrentValueOfDiamonds);
        }
    }
    public static void DestroyDiamond() { Destroy(DiamondCloned); AudioManager.Instance.SoundPlay(4); }
    public void TakeDiamondLocal(){if(!Player.Instance.Died){TakeDiamond(); }}
    public static void TakeDiamond() { if (doubleDiamonds) { CurrentValueOfDiamonds = CurrentValueOfDiamonds + 2; Achievements.AchievementSave.DiamondsCollectedInOneGame = Achievements.AchievementSave.DiamondsCollectedInOneGame + 2; } else { CurrentValueOfDiamonds++; Achievements.AchievementSave.DiamondsCollectedInOneGame++;} }
    IEnumerator SetDoubleDiamonds()
    {
        doubleDiamonds = true;
        Effects_Mechanics.ChangeStateOfEffectIcon(Effects_Mechanics.Effects.Double_Diamonds, true);
        yield return new WaitForSeconds(7.5f);
        doubleDiamonds = false;
        Effects_Mechanics.ChangeStateOfEffectIcon(Effects_Mechanics.Effects.Double_Diamonds, false);
    }
}

