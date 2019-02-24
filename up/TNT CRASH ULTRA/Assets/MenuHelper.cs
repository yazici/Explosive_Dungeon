using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MenuHelper : MonoBehaviour {
    public  Camera[] Cameras;
    public  Canvas[] Canvases;
    public static TextMeshProUGUI FindTextByName(string name) { TextMeshProUGUI founded_text = null; if(GameObject.Find(name) != null){founded_text = GameObject.Find(name).GetComponent<TextMeshProUGUI>();} return founded_text;}
    void Start()
    {
        ChangeMenuTab(0);
    }
    public  void ChangeMenuTab(int index) { for (int i = 0; i < Cameras.Length; i++) {
            if (i != index) {
                Cameras[i].enabled = false;
                Canvases[i].enabled = false;
            }
            Cameras[index].enabled = true;
            Canvases[index].enabled = true;
        }
        print("sbfsh");
    }
}
