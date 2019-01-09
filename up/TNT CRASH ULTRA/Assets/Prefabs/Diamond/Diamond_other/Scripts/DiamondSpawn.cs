using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondSpawn : MonoBehaviour {
    public Effects_Mechanics Effects_Mechanics;
    public static DiamondSpawn Instance;
    private static GameObject DiamondCloned;
    public GameObject DiamondPrefab;
    
    public Transform DiamondSpawner;
    public static int CurrentValueOfDiamonds;
    private static AudioSource diamond_wav;
    public static bool doubleDiamonds;

    private void Start()
    {
        Instance = this;
        diamond_wav = GameObject.Find("Audio_Diamond").GetComponent<AudioSource>();
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
    public static void DestroyDiamond() { Destroy(DiamondCloned); diamond_wav.Play(); }
    public static void TakeDiamond() { if (doubleDiamonds) { CurrentValueOfDiamonds = CurrentValueOfDiamonds + 2; } else { CurrentValueOfDiamonds++; } }
    IEnumerator SetDoubleDiamonds()
    {
        doubleDiamonds = true;
        Effects_Mechanics.ChangeStateOfEffectIcon(Effects_Mechanics.Effects.Double_Diamonds, true);
        yield return new WaitForSeconds(15f);
        doubleDiamonds = false;
        Effects_Mechanics.ChangeStateOfEffectIcon(Effects_Mechanics.Effects.Double_Diamonds, false);
    }
}

