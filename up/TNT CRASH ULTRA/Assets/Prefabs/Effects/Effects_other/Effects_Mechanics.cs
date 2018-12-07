using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Effects_Mechanics : MonoBehaviour
{   
    public GameObject[] Effects_GameObjects;
    public enum Effects { Speed, Double_Diamonds, Armor };
    void Start()
    {
        foreach (GameObject elem in Effects_GameObjects) { elem.SetActive(false); }
    }
    public void ChangeStateOfEffectIcon(Effects effect, bool enable)
    {
        switch (effect)
        {
            case Effects.Speed: Effects_GameObjects[0].SetActive(enable); break;
            case Effects.Double_Diamonds: Effects_GameObjects[1].SetActive(enable); break;
            case Effects.Armor: Effects_GameObjects[2].SetActive(enable); break;
        }
    }
}