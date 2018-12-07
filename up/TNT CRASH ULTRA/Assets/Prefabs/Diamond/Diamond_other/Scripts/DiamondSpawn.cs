using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondSpawn : MonoBehaviour {
    public Effects_Mechanics Effects_Mechanics;
    private static GameObject DiamondCloned;
    public GameObject DiamondPrefab;
    public static bool doubleDiamonds;
    public Transform DiamondSpawner;
    public static int CurrentValueOfDiamonds;
    private static AudioSource diamond_wav;
    private void Start()
    {
        diamond_wav = GameObject.Find("Audio_Diamond").GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (doubleDiamonds) { StartCoroutine(SetDoubleDiamonds()); }
        if (DiamondCloned == null)
        {
            DiamondSpawner.position = new Vector2(Random.Range(-8.0f, 8.0f), DiamondSpawner.position.y);
            DiamondCloned = Instantiate(DiamondPrefab, DiamondSpawner.transform.position, Quaternion.identity) as GameObject;
        Debug.Log(CurrentValueOfDiamonds);
        }
    }
    public static void DestroyDiamond() { Destroy(DiamondCloned); diamond_wav.Play(); }
    public static void TakeDiamond() { if (doubleDiamonds) { CurrentValueOfDiamonds = CurrentValueOfDiamonds + 2; } else { CurrentValueOfDiamonds++; } }
    public IEnumerator SetDoubleDiamonds()
    {
        Effects_Mechanics.ChangeStateOfEffectIcon(Effects_Mechanics.Effects.Double_Diamonds, true);
        yield return new WaitForSeconds(15);
        doubleDiamonds = false;
        Effects_Mechanics.ChangeStateOfEffectIcon(Effects_Mechanics.Effects.Double_Diamonds, false);
    }
}

