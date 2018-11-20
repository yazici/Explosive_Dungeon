using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadingScreen2 : MonoBehaviour {
    void Start()
    {
        StartCoroutine(LoadAsync());
    }
    IEnumerator LoadAsync()
    {
        yield return new WaitForSeconds(3f);
        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync("LoadingScreen");
        if (sceneLoad.isDone)
        {
            Debug.Log("Scene was loaded!");
        }
        while (!sceneLoad.isDone)
        {
            yield return null;
        }
    }
}
