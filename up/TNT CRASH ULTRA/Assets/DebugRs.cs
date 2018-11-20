using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DebugRs : MonoBehaviour {

    public void restart() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
}
