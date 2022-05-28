using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    public bool isBattery;
    public bool isGlowStick;
    public int numItems;
    

    void Start() {
        Debug.Log("started!");
    }

    void OnCollisionEnter(Collision collision) {
        Debug.Log("Entered the collision");
    }

    void OnCollisionExit(Collision collision) {
        Debug.Log("Exited the collision");
    }
}
