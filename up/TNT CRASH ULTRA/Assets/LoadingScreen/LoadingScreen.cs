using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadingScreen : MonoBehaviour {
	void Start () {
        StartCoroutine(LoadAsync());
	}
    IEnumerator LoadAsync(){
        yield return new WaitForSeconds(3.5f);
        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync("GameMenu");
        if (sceneLoad.isDone){
            Debug.Log("Scene was loaded!");
        }
        while (!sceneLoad.isDone){
            yield return null;
        }
    }
}
