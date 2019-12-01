using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Limits : MonoBehaviour
{
    public Vector2 screenResolution;
    public Vector3 limits;

    void Start()
    {
        screenResolution = Camera.main.ScreenToWorldPoint(new Vector3
            (Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    void Update()
    {
        limits = transform.position;
        limits.x = Mathf.Clamp(limits.x, screenResolution.x, screenResolution.x * -1);
        limits.y = Mathf.Clamp(limits.y, screenResolution.y, screenResolution.y * -1);
        transform.position = limits;
    }
}
