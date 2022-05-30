using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoRotate : MonoBehaviour
{

    public float speed = 10f;
    public float levitate = 10f;
    public float amplitude = 10f;
    
    private Vector3 startPosition;

    private bool atStart = true;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, speed * Time.deltaTime, 0f);

        transform.position = new Vector3(transform.position.x, startPosition.y + amplitude * Mathf.Sin(levitate*Time.time), transform.position.z);


    }
}
