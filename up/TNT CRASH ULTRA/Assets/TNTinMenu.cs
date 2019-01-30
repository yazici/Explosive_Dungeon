using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTinMenu : MonoBehaviour
{
    public GameObject tnt_prefab;
    private GameObject tnt_object;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("StartLifeCycle", 2f, 3f);
    }
    void StartLifeCycle() { StartCoroutine(MenuTNT()); }
    public IEnumerator MenuTNT()
    {
        gameObject.transform.position = new Vector3(Random.Range(-8.0f, 8.0f), gameObject.transform.position.y, gameObject.transform.position.z);
        tnt_object = Instantiate(tnt_prefab, gameObject.transform.position, Quaternion.identity) as GameObject;
        tnt_object.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
        tnt_object.GetComponent<SpriteRenderer>().sortingOrder = -1;
        tnt_object.GetComponent<Rigidbody2D>().gravityScale = 0.45f;
        yield return new WaitForSeconds(2.01f);
        Destroy(tnt_object);
    }
}
