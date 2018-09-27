using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
public class MenuResponse : MonoBehaviour
{
    public float CHANGE_COLOR_TIME = 0.7f;

    public void Start() { InvokeRepeating("ChangePlay", 0, CHANGE_COLOR_TIME); }

    public void ChangeScene() { SceneManager.LoadScene(1); }

    public void OpenTwitter() { Application.OpenURL("https://twitter.com/8ilverStudio"); }

    private void ChangePlay()
    {
        Text PlayButtonText = MenuHelper.FindTextByName("PlayText");
        if (PlayButtonText.color == new Color32(0, 0, 0, 255)){PlayButtonText.color = new Color32(255, 255, 255, 255);}else{PlayButtonText.color = new Color32(0, 0, 0, 255);}
    }
}
// if (MenuHelper.FindTextByName("PlayText").color == new Color32(0, 0, 0, 255)) { MenuHelper.FindTextByName("PlayText").color = new Color32(255, 255, 255, 255); } else { MenuHelper.FindTextByName("PlayText").color = new Color32(0, 0, 0, 255);