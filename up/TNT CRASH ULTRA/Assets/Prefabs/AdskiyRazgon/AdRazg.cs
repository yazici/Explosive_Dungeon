using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdRazg : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroydd", 0.5f);
    }

    // Update is called once per frame
    void Destroydd()
    {
        Destroy(this.gameObject);
    }
}
