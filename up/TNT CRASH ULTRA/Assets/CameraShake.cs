using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {
    public float shake_power = 0.7f;
    public float duration = 1.0f;
    public Transform camera;
    public float slow_down_amount = 1.0f;
    public static bool Should_Shake = false;

    Vector3 startPosition;
    float initial_Duration;

    private void Start()
    {
        camera = Camera.main.transform;
        startPosition = camera.localPosition;
        initial_Duration = duration;
    }
    private void Update()
    {
        if (Should_Shake)
        {
            if (duration > 0)
            {
                camera.localPosition = startPosition + Random.insideUnitSphere * shake_power;
                duration -= Time.deltaTime * slow_down_amount;
            }
            else
            {
                Should_Shake = false;
                duration = initial_Duration;
                camera.localPosition = startPosition;
            }
        }
    }
}
