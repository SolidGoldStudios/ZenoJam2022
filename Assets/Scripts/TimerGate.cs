using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerGate : MonoBehaviour
{
    public Vector3 targetPosition;
    public float duration;

    private Vector3 startPosition;
    private float startTime;
    private bool moving;

    void Start()
    {
        startPosition = transform.position;
        moving = true;
    }

    void Update()
    {
        if (moving) {
            float t = (Time.time - startTime) / duration;
            Debug.Log("t = " + t);
            if (t >= 0 && t <= 1) {
                transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            } else {
                transform.position = targetPosition;
                moving = false;
            }
        }
    }

    public void Move()
    {
        startTime = Time.time;
        moving = true;
    }
}
