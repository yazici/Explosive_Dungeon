/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InLevelUI : MonoBehaviour
{
    Vector3 startPosition;
    void Start()
    {
        startPosition = Camera.main.transform.localPosition;
        InvokeRepeating("ShakeCamera", 3f, 999);
    }
    public void ShakeCamera()
    {
        // Тряска камерой :)
        print("hello");
        float duration = 1.0f;
        float slow_down_amount = 1.0f;
        float shake_power = 0.7f;
        bool Should_Shake = true;
        while(Should_Shake)
        {
            if (duration > 0)
            {
                Camera.main.transform.localPosition = startPosition + Random.insideUnitSphere * shake_power;
                duration -= Time.unscaledDeltaTime * slow_down_amount;
            }
            else
            {
                Should_Shake = false;
                Camera.main.transform.localPosition = startPosition;
            }
        }
    }
}
*/