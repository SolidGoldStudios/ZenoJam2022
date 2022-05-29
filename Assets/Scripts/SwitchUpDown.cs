using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchUpDown : MonoBehaviour
{
    public float speed = 2.5f;

    void Start()
    {

    }

    void Update()
    {
        transform.position = new Vector3 (Mathf.PingPong (Time.time * speed, 5),
            transform.position.y, transform.position.x);
}
}