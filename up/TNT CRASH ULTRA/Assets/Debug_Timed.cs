using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Debug_Timed : MonoBehaviour
{
    public TextMeshProUGUI DiamondsCounter;
    // Start is called before the first frame update
    public void AddDiamonds()
    {
        if (PlayerPrefs.HasKey("DiamondsCount"))
        {
            PlayerPrefs.SetInt("DiamondsCount", PlayerPrefs.GetInt("DiamondsCount") + 100);
            DiamondsCounter.text = "" + PlayerPrefs.GetInt("DiamondsCount");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
