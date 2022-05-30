using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoRotate : MonoBehaviour
{

    public float speed = 10f;
    public float levitate = 10f;

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
        
        if (startPosition.y - levitate > transform.position.y) 
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (5 * Time.deltaTime), transform.position.z);
        }

        if (startPosition.y + levitate < transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - (5 * Time.deltaTime), transform.position.z);
        }

        
    }
}
